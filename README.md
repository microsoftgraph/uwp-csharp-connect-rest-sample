# Microsoft Graph Connect Sample for UWP (REST)

> **Building apps for enterprise customers?** Your app may not work if your enterprise customer turns on enterprise mobility security features like <a href="https://azure.microsoft.com/en-us/documentation/articles/active-directory-conditional-access-device-policies/" target="_newtab">conditional device access</a>. In this case, you may not know and your customers may experience errors. 

> To support **all enterprise customers** across **all enterprise scenarios**, you must use the Azure AD endpoint and manage your apps using the [Azure Management Portal](https://aka.ms/aadapplist). For more information, see [Deciding between the Azure AD and Azure AD v2.0 endpoints](https://graph.microsoft.io/docs/authorization/auth_overview#deciding-between-azure-ad-and-the-v2-authentication-endpoint).

**Table of contents**

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Register and configure the app](#register)
* [Build and debug](#build)
* [Questions and comments](#questions)
* [Additional resources](#additional-resources)

<a name="introduction"></a>
## Introduction

This sample shows how to connect your Windows 10 Universal app to Office 365 using the Microsoft Graph API to retrieve a user's profile picture, upload the picture to OneDrive, and send an email that contains the photo as an attachment and the sharing link in its text. The sample uses the Azure AD v2.0 endpoint, which enables users to sign in with either their personal or work or school Microsoft accounts.

> Note: To understand the code for calling Microsoft Graph in a UWP app, see [Call Microsoft Graph in a universal Windows 10 app] (https://graph.microsoft.io/docs/platform/uwp).


<a name="prerequisites"></a>
## Prerequisites ##

**Note:** Try out the [Get started with Office 365 APIs](http://dev.office.com/getting-started/office365apis?platform=option-windowsuniversal#setup) page, which simplifies registration so you can get this sample running faster.

This sample requires the following:  

  * [Visual Studio 2015](https://www.visualstudio.com/en-us/downloads) 
  * Windows 10 ([development mode enabled](https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx))
  * Either a [Microsoft](www.outlook.com) or [Office 365 for business account](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account).

<a name="register"></a>
## Register and configure the app

1. Sign into the [App Registration Portal](https://apps.dev.microsoft.com/) using either your personal or work or school account.
2. Select **Add an app**.
3. Enter a name for the app, and select **Create application**.
	
	The registration page displays, listing the properties of your app.
 
4. Under **Platforms**, select **Add platform**.
5. Select **Mobile platform**.
6. Copy both the Application Id and Redirect URI values to the clipboard. You'll need to enter these values into the sample app.

	The app id is a unique identifier for your app. The redirect URI is a unique URI provided by Windows 10 for each application to ensure that messages sent to that URI are only sent to that application. 

7. Select **Save**.


<a name="build"></a>
## Build and debug ##

**Note:** If you see any errors while installing packages during step 2, make sure the local path where you placed the solution is not too long/deep. Moving the solution closer to the root of your drive resolves this issue.


1. After you've loaded the solution in Visual Studio, configure the sample to use the application (client) id and redirectURI that you registered by adding the corresponding values for these keys in the Application.Resources node of the App.xaml file.
![Office 365 UWP Microsoft Graph connect sample](/readme-images/appId_and_redirectURI.png "Client ID value in App.xaml file")`

2. Press F5 to build and debug. Run the solution and sign in with either your personal or work or school account.

<a name="contributing"></a>
## Contributing ##

If you'd like to contribute to this sample, see [CONTRIBUTING.MD](/CONTRIBUTING.md).

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.

<a name="questions"></a>
## Questions and comments

We'd love to get your feedback about the UWP Microsoft Graph Connect project. You can send your questions and suggestions to us in the [Issues](https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Connect/issues) section of this repository.

Your feedback is important to us. Connect with us on [Stack Overflow](http://stackoverflow.com/questions/tagged/office365+or+microsoftgraph). Tag your questions with [MicrosoftGraph] and [office365].

<a name="additional-resources"></a>
## Additional resources ##

- [Other Office 365 Connect samples](https://github.com/OfficeDev?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph overview](http://graph.microsoft.io)
- [Office 365 APIs platform overview](https://msdn.microsoft.com/office/office365/howto/platform-development-overview)
- [Office 365 API code samples and videos](https://msdn.microsoft.com/office/office365/howto/starter-projects-and-code-samples)
- [Office developer code samples](http://dev.office.com/code-samples)
- [Office dev center](http://dev.office.com/)

## Copyright
Copyright (c) 2016 Microsoft. All rights reserved.


