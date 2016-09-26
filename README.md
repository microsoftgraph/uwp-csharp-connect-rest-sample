# Microsoft Graph Connect Sample for UWP (REST)

**Table of contents**

* [Introduction](#introduction)
* [Prerequisites](#prerequisites)
* [Find the system-assigned redirect URI](#redirect)
* [Register and configure the app](#register)
* [Build and debug](#build)
* [Questions and comments](#questions)
* [Additional resources](#additional-resources)

<a name="introduction"></a>
##Introduction

This sample shows how to connect your Windows 10 Universal app to Office 365 using the Microsoft Graph API (previously called Office 365 unified API) to send an email. It also uses the new Windows 10 [WebAccountManager API](http://blogs.technet.com/b/ad/archive/2015/08/03/develop-windows-universal-apps-with-azure-ad-and-the-windows-10-identity-api.aspx) to authenticate users in your tenant.

<a name="prerequisites"></a>
## Prerequisites ##

This sample requires the following:  

  * Visual Studio 2015 
  * Windows 10 ([development mode enabled](https://msdn.microsoft.com/library/windows/apps/xaml/dn706236.aspx))
  * An Office 365 for business account. You can sign up for [an Office 365 Developer subscription](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account) that includes the resources that you need to start building Office 365 apps.
  * A Microsoft Azure tenant to register your application. Azure Active Directory (AD) provides identity services that applications use for authentication and authorization. A trial subscription can be acquired here: [Microsoft Azure](http://aka.ms/jjm0q7).

**Important**: You will also need to ensure your Azure subscription is bound to your Office 365 tenant. To do this, see [Associate your Office 365 account with Azure AD to create and manage apps](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_CreateAzureSubscription) for more information.

<a name="redirect"></a>
## Find the system-assigned redirect URI for the app

Before you can register the application in the Azure portal, you need to find out the application's redirect URI.  Windows 10 provides each application with a unique URI and ensures that messages sent to that URI are only sent to that application.  To determine the redirect URI for your project:

1. Open the solution in Visual Studio 2015. 
2. Make sure that your Platform Target is set to x86 or x64.
3. Press F5.
4. After the app launches, choose the **Copy** button ![alt text](/readme-images/copy_icon.png) located in the menu on the top left of the app. This will copy the redirect URI for the app to the clipboard. 
5. Store this value. You will use it when registering the app, as described in the following section. 


The redirect URI value will look something like this:
```
ms-appx-web://Microsoft.AAD.BrokerPlugIn/S-1-15-2-694665007-945573255-503870805-3898041910-4166806349-50292026-2305040851
```


<a name="register"></a>
##Register and configure the app

1. Sign in to the [Azure portal](https://portal.azure.com/).
2. On the top bar, click on your account and under the **Directory** list, choose the Active Directory tenant where you wish to register your application.
3. Click on **More Services** in the left hand nav, and choose **Azure Active Directory**.
4. Click on **App registrations** and choose **Add**.
5. Enter a friendly name for the application, select **Native** as the Application Type. For the **Redirect URI**, enter the redirect URI value that you obtained during the previous step. Click on **Create** to create the application.
6. While still in the Azure portal, choose your application, click on **Settings** and choose **Properties**.
7. Find the **Application ID** value and copy it to the clipboard. This is the client ID value we'll add to the project later.
8. Configure **Permissions** for your application - in the **Settings** menu, choose the **Required permissions** section, click on **Add**, then **Select an API**, and type "Microsoft Graph" in the text box. Then, click on **Select Permissions** and select:
   * Send mail as user
   * Sign in and read user profile
 
9. Click **Select**.


<a name="build"></a>
## Build and debug ##

**Note:** If you see any errors while installing packages during step 2, make sure the local path where you placed the solution is not too long/deep. Moving the solution closer to the root of your drive resolves this issue.

1. After you've loaded the solution in Visual Studio, configure the sample to use the application (client) id that you registered in Azure Active Directory and the domain of your tenant by adding the corresponding values for these keys in the Application.Resources node of the App.xaml file.
![Office 365 UWP Microsoft Graph connect sample](/readme-images/ClientTenant.png "Client ID value in App.xaml file")`

2. Press F5 to build and debug. Run the solution and sign in to Office 365 with your organizational account.

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
Copyright (c) 2015 Microsoft. All rights reserved.


