# Office 365 Profile sample for Windows

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

This sample shows how to connect your Windows 10 Universal app to Office 365 using the preview unified API to send an email. It also uses the new Windows 10 [WebAccountManager API](http://blogs.technet.com/b/ad/archive/2015/08/03/develop-windows-universal-apps-with-azure-ad-and-the-windows-10-identity-api.aspx) to authenticate users in your tenant.

<a name="prerequisites"></a>
## Prerequisites ##

This sample requires the following:  
  * Visual Studio 2015.  
  * Windows 10 Tools for Visual Studio
  * Windows 10 (development mode enabled)
  * An Office 365 account. You can sign up for [an Office 365 Developer subscription](http://aka.ms/ro9c62) that includes the resources that you need to start building Office 365 apps.
  * A Microsoft Azure tenant to register your application. Azure Active Directory provides identity services that applications use for authentication and authorization. A trial subscription can be acquired here: [Microsoft Azure](http://aka.ms/jjm0q7).

**Important**: You will also need to ensure your Azure subscription is bound to your Office 365 tenant. To do this see the Active Directory team's blog post, [Creating and Managing Multiple Windows Azure Active Directories](http://blogs.technet.com/b/ad/archive/2013/11/08/creating-and-managing-multiple-windows-azure-active-directories.aspx). In this post, the *Adding a new directory* section will explain how to do this. You can also read [Set up your Office 365 development environment](https://msdn.microsoft.com/office/office365/howto/setup-development-environment#bk_CreateAzureSubscription) for more information.

<a name="redirect"></a>
## Find the system-assigned redirect URI for the app

Before you can register the application in the Azure portal, you need to find out the application's redirect URI.  Windows 10 provides each application with a unique URI and ensures that messages sent to that URI are only sent to that application.  To determine the redirect URI for your project:

1. Open the solution in Visual Studio 2015.
2. Open the `AuthorizationHelper.cs` file.
3. Find this line of code, uncomment it, and set a breakpoint on it.

```C#
string URI = string.Format("ms-appx-web://Microsoft.AAD.BrokerPlugIn/{0}", WebAuthenticationBroker.GetCurrentApplicationCallbackUri().Host.ToUpper());
```

4. Press F5.
5. When the breakpoint is hit, use the debugger to determine the value of redirectURI, and copy it aside for the next step.
6. Stop debugging, and clear the breakpoint.

The redirectURI value will look something like this:

```
ms-appx-web://Microsoft.AAD.BrokerPlugIn/S-1-15-2-694665007-945573255-503870805-3898041910-4166806349-50292026-2305040851
```


<a name="register"></a>
##Register and configure the app

1.	Sign in to the [Azure Management Portal](http://aka.ms/i5b8dz), using your Azure AD credentials.
2.	Click **Active Directory** on the left menu, then select the directory for your Office 365 developer site.
3.	On the top menu, click **Applications**.
4.	Click **Add** from the bottom menu.
5.	On the **What do you want to do page**, click **Add an application my organization is developing**.
6.	On the **Tell us about your application page**, specify **O365-Win-Profile** for the application name and select **NATIVE CLIENT APPLICATION** for type.
7.	Click the arrow icon on the bottom-right corner of the page.
8.	On the **Application information** page, enter the redirect URI value that you obtained during the previous step.
9.	Once the application has been successfully added, you will be taken to the **Quick Start** page for the application. From here, select **Configure** in the top menu.
10.	Under **permissions to other applications**, select **Add application**. In the dialog box, select the **Office 365 unified API (preview)** application. After you return to the application configuration page, select the **Read items in all site collections**, **Read users' files**, and **Access directory as the signed in user** permissions.
11.	Copy the value specified for **Client ID** on the **Configure** page, and be sure to remember the value you specified for the redirect URI.
12.	Click **Save** in the bottom menu.

<a name="build"></a>
## Build and debug ##

**Note:** If you see any errors while installing packages during step 3 (for example, *Unable to find "Microsoft.IdentityModel.Clients.ActiveDirectory"*) make sure the local path where you placed the solution is not too long/deep. Moving the solution closer to the root of your drive resolves this issue.

1. After you've loaded the solution in Visual Studio, configure the sample to use the client id that you registered in Azure Active directory and the domain of your tenant by adding the corresponding values for these keys in the Application.Resources node of the App.xaml file.
![Office 365 UWP unified API connect sample](/readme-images/ClientTenant.png "Client ID value in App.xaml file")`

2. Press F5 to build and debug. Run the solution and sign in with your organizational account to Office 365.


<a name="questions"></a>
## Questions and comments

We'd love to get your feedback on the O365 UWP unified APIConnect project. You can send your questions and suggestions to us in the [Issues](https://github.com/OfficeDev/O365-UWP-Unified-API-Connect/issues) section of this repository.

Questions about Office 365 development in general should be posted to [Stack Overflow](http://stackoverflow.com/questions/tagged/Office365+API). Make sure that your questions or comments are tagged with [Office365] and [API].

<a name="additional-resources"></a>
## Additional resources ##

- [Other Office 365 Connect samples](https://github.com/OfficeDev?utf8=%E2%9C%93&query=-Connect)
- [Office 365 unified API overview (preview)](https://msdn.microsoft.com/en-us/office/office365/howto/office-365-unified-api-overview)
- [Office 365 APIs platform overview](https://msdn.microsoft.com/office/office365/howto/platform-development-overview)
- [Office 365 API code samples and videos](https://msdn.microsoft.com/office/office365/howto/starter-projects-and-code-samples)
- [Office developer code samples](http://dev.office.com/code-samples)
- [Office dev center](http://dev.office.com/)


## Copyright
Copyright (c) 2015 Microsoft. All rights reserved.

