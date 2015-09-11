// Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. See full license at the bottom of this file.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace O365_UWP_Unified_API_Connect
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string _mailAddress;
        private string _displayName = null;
        private MailHelper _mailHelper = new MailHelper();
        private bool _userLoggedIn = false;
        public static ApplicationDataContainer _settings = ApplicationData.Current.RoamingSettings;

        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Developer code - if you haven't registered the app yet, we warn you. 
            if (!App.Current.Resources.ContainsKey("ida:ClientID"))
            {
                WelcomeText.Text = "Oops - App not registered with Office 365. To run this sample, you must register it with Office 365. You can do that through the 'Add | Connected services' dialog in Visual Studio. See Readme for more info";
                ConnectButton.IsEnabled = false;
            }
        }

        /// <summary>
        /// Signs in the current user.
        /// </summary>
        /// <returns></returns>
        public async Task SignInCurrentUserAsync()
        {
            var token = await AuthenticationHelper.GetTokenHelperAsync();

            if (token != null)
            {
                _userLoggedIn = true;
                string userId = (string)_settings.Values["userID"];
                _mailAddress = (string)_settings.Values["userEmail"];
                _displayName = (string)_settings.Values["userName"];
            }

        }


        //Toggle button for logging user in and out.
        private async void ConnectButton_Click(object sender, RoutedEventArgs e)
        {
            var rl = ResourceLoader.GetForCurrentView();
            if (!_userLoggedIn)
            {
                WelcomeText.Text = "acquiring token...";
                ProgressBar.Visibility = Visibility.Visible;
                await SignInCurrentUserAsync();
                if (!String.IsNullOrEmpty(_displayName))
                {
                    WelcomeText.Text = "Hi " + _displayName + "," + Environment.NewLine + rl.GetString("WelcomeMessage");
                    MailButton.IsEnabled = true;
                    EmailAddressBox.IsEnabled = true;
                    _userLoggedIn = true;
                    ConnectButton.Content = "disconnect";
                    EmailAddressBox.Text = _mailAddress;
                }
                else
                {
                    WelcomeText.Text = rl.GetString("AuthenticationErrorMessage");
                }
            }
            else
            {
                ProgressBar.Visibility = Visibility.Visible;
                AuthenticationHelper.SignOut();
                WelcomeText.Text = "";
                ProgressBar.Visibility = Visibility.Collapsed;
                MailButton.IsEnabled = false;
                EmailAddressBox.IsEnabled = false;
                _userLoggedIn = false;
                ConnectButton.Content = "connect";
                this._displayName = null;
                this._mailAddress = null;
            }

            ProgressBar.Visibility = Visibility.Collapsed;
        }

        private async void MailButton_Click(object sender, RoutedEventArgs e)
        {
            var rl = ResourceLoader.GetForCurrentView();
            _mailAddress = EmailAddressBox.Text;
            WelcomeText.Text = "sending mail...";
            ProgressBar.Visibility = Visibility.Visible;
            try
            {
                await _mailHelper.ComposeAndSendMailAsync(rl.GetString("MailSubject"), ComposePersonalizedMail(_displayName), _mailAddress);
                WelcomeText.Text = "mail sent";
            }
            catch (Exception)
            {
                WelcomeText.Text = rl.GetString("MailErrorMessage");
            }
            ProgressBar.Visibility = Visibility.Collapsed;
        }

        // <summary>
        // Personalizes the e-mail.
        // </summary>
        public static string ComposePersonalizedMail(string userName)
        {
            var rl = ResourceLoader.GetForCurrentView();
            return String.Format(rl.GetString("MailContents"), userName);
        }
    }
}

//********************************************************* 
// 
//O365-UWP-Unified-API-Connect, https://github.com/OfficeDev/O365-UWP-Unified-API-Connect
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