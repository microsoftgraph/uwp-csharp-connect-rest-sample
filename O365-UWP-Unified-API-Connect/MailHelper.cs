// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Windows.Storage;

namespace O365_UWP_Unified_API_Connect
{
    class MailHelper
    {

        /// <summary>
        /// Compose and send a new email.
        /// </summary>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="bodyContent">The body of the email.</param>
        /// <param name="recipients">A semicolon-separated list of email addresses.</param>
        /// <returns></returns>
        internal async Task ComposeAndSendMailAsync(string subject,
                                                            string bodyContent,
                                                            string recipients)
        {

            // Get current user photo
            Stream photoStream = await GetCurrentUserPhotoStreamAsync();


            // If the user doesn't have a photo, or if the user account is MSA, we use a default photo

            if (photoStream == null)
            {
                StorageFile file = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFileAsync("test.jpg");
                photoStream = (await file.OpenReadAsync()).AsStreamForRead();
            }

            MemoryStream photoStreamMS = new MemoryStream();
            // Copy stream to MemoryStream object so that it can be converted to byte array.
            photoStream.CopyTo(photoStreamMS);
            byte[] photoStreamBytes = photoStreamMS.ToArray();
            string photoStreamBase64 = Convert.ToBase64String(photoStreamBytes);

            string photoFileId = await UploadFileToOneDriveAsync(photoStreamMS.ToArray());

            string attachments = "{'contentBytes':'" + photoStreamBase64 + "',"
                                + "'@odata.type' : '#microsoft.graph.fileAttachment',"
                                + "'contentType':'image/png',"
                                + "'name':'me.png'}";

            // Get the sharing link and insert it into the message body.
            string sharingLinkUrl = await GetSharingLinkAsync(photoFileId);
            string bodyContentWithSharingLink = String.Format(bodyContent, sharingLinkUrl);

            // Prepare the recipient list
            string[] splitter = { ";" };
            var splitRecipientsString = recipients.Split(splitter, StringSplitOptions.RemoveEmptyEntries);
            string recipientsJSON = null;

            int n = 0;
            foreach (string recipient in splitRecipientsString)
            {
                if ( n==0)
                recipientsJSON += "{'EmailAddress':{'Address':'" + recipient.Trim() + "'}}";
                else
                {
                    recipientsJSON += ", {'EmailAddress':{'Address':'" + recipient.Trim() + "'}}";
                }
                n++;
            }

            try
            {

                HttpClient client = new HttpClient();
                var token = await AuthenticationHelper.GetTokenForUserAsync();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                
                // Build contents of post body and convert to StringContent object.
                // Using line breaks for readability.
                string postBody = "{'Message':{" 
                    +  "'Body':{ " 
                    + "'Content': '" + bodyContentWithSharingLink + "'," 
                    + "'ContentType':'HTML'}," 
                    + "'Subject':'" + subject + "'," 
                    + "'ToRecipients':[" + recipientsJSON +  "],"
                    + "'Attachments':[" + attachments + "]},"
                    + "'SaveToSentItems':true}";

                var emailBody = new StringContent(postBody, System.Text.Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await client.PostAsync(new Uri("https://graph.microsoft.com/v1.0/me/microsoft.graph.SendMail"), emailBody);

                if ( !response.IsSuccessStatusCode)
                {

                    throw new Exception("We could not send the message: " + response.StatusCode.ToString());
                }


            }

            catch (Exception e)
            {
                throw new Exception("We could not send the message: " + e.Message);
            }
        }

        // Gets the stream content of the signed-in user's photo. 
        // This snippet doesn't work with consumer accounts.
        public async Task<Stream> GetCurrentUserPhotoStreamAsync()
        {
            Stream currentUserPhotoStream = null;

            try
            {
                HttpClient client = new HttpClient();
                var token = await AuthenticationHelper.GetTokenForUserAsync();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                // Endpoint for the current user's photo
                Uri photoEndpoint = new Uri("https://graph.microsoft.com/v1.0/me/photo/$value");

                HttpResponseMessage response = await client.GetAsync(photoEndpoint);

                if (response.IsSuccessStatusCode)
                {
                    currentUserPhotoStream = await response.Content.ReadAsStreamAsync();
                }

                else
                {
                    return null;
                }

            }


            catch (Exception e)
            {
                return null;

            }

            return currentUserPhotoStream;

        }

        // Uploads the specified file to the user's root OneDrive directory.
        public async Task<string> UploadFileToOneDriveAsync(byte[] file)
        {
            string uploadedFileId = null;
            JObject jResult = null;

            try
            {

                HttpClient client = new HttpClient();
                var token = await AuthenticationHelper.GetTokenForUserAsync();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                MemoryStream fileStream = new MemoryStream(file);
                var fileContentPostBody = new StreamContent(fileStream);

                // Endpoint for content in an existing file.
                Uri fileEndpoint = new Uri("https://graph.microsoft.com/v1.0/me/drive/root/children/me.png/content");

                var requestMessage = new HttpRequestMessage(HttpMethod.Put, fileEndpoint)
                {
                    Content = fileContentPostBody
                };


                HttpResponseMessage response = await client.SendAsync(requestMessage);

                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    jResult = JObject.Parse(responseContent);
                    uploadedFileId = (string)jResult["id"];


                }

                return uploadedFileId;
            }

            catch (Exception e)
            {
                throw new Exception("We could not create the file. The request returned this status code: " + e.Message);
            }
        }

        public static async Task<string> GetSharingLinkAsync(string Id)
        {
            string sharingLinkUrl = null;
            JObject jResult = null;

            try
            {
                HttpClient client = new HttpClient();
                var token = await AuthenticationHelper.GetTokenForUserAsync();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                string postContent = "{'type': 'view','scope': 'anonymous'}";
                var createBody = new StringContent(postContent, System.Text.Encoding.UTF8, "application/json");
                Uri photoEndpoint = new Uri("https://graph.microsoft.com/v1.0/me/drive/items/" + Id + "/createLink");
                HttpResponseMessage response = await client.PostAsync(photoEndpoint, createBody);
                if (response.IsSuccessStatusCode)
                {
                    string responseContent = await response.Content.ReadAsStringAsync();
                    jResult = JObject.Parse(responseContent);
                    JToken sharingLink = jResult["link"];
                    sharingLinkUrl = (string)sharingLink["webUrl"];

                }
                else
                {
                    return null;
                }

            }

            catch (Exception e)
            {
                throw new Exception("We could not get the sharing link. The request returned this status code: " + e.Message);
            }

            return sharingLinkUrl;
        }
    }
}
//********************************************************* 
// 
//O365-UWP-Microsoft-Graph-Connect, https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Connect
//
//Copyright (c) Microsoft Corporation
//All rights reserved. 
//
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// ""Software""), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:

// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 
//********************************************************* 