# 使用 Microsoft Graph 的适于 UWP 的 Office 365 Connect 示例

**目录**  

* [简介](#introduction)  
* [先决条件](#prerequisites)  
* [查找系统分配的重定向 URI](#redirect)  
* [注册和配置应用](#register)  
* [构建和调试](#build)  
* [问题和意见](#questions)  
* [其他资源](#additional-resources)  

<a name="introduction"></a>
##简介

此示例演示如何使用 Microsoft Graph API（旧称 Office 365 统一 API）连接 Windows 10 通用应用以发送电子邮件。该示例还使用新的 Windows 10 [WebAccountManager API](http://blogs.technet.com/b/ad/archive/2015/08/03/develop-windows-universal-apps-with-azure-ad-and-the-windows-10-identity-api.aspx) 对租户中的用户进行身份验证。

> 注意： 要了解在 UWP 应用中调用 Microsoft Graph 的代码，请参阅[在通用 Windows 10 应用中调用 Microsoft Graph](https://graph.microsoft.io/docs/platform/uwp)。


<a name="prerequisites"></a>
## 先决条件 ##

**注意：** 尝试 [Office 365 API 入门](http://dev.office.com/getting-started/office365apis?platform=option-windowsuniversal#setup)页面，其简化了注册，使您可以更快地运行该示例。

该示例需要以下各项：

  * Visual Studio 2015 
  * Windows 10（[已启用开发模式](https://msdn.microsoft.com/zh-cn/library/windows/apps/xaml/dn706236.aspx)）
  * Office 365 商业版帐户。您可以注册 [Office 365 开发人员订阅](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account)，其中包含您开始构建 Office 365 应用所需的资源。
  * 用于注册您的应用程序的 Microsoft Azure 租户。Azure Active Directory (AD) 为应用程序提供了用于进行身份验证和授权的标识服务。您还可在此处获得试用订阅： [Microsoft Azure](http://aka.ms/jjm0q7)。

**重要说明**： 您还需要确保您的 Azure 订阅已绑定到 Office 365 租户。要执行这一操作，请参阅[关联您的 Office 365 帐户和 Azure AD 以创建并管理应用](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_CreateAzureSubscription)获取详细信息。

<a name="redirect"></a>
## 查找应用中系统分配的重定向 URI

在 Azure 门户中注册应用程序之前，您需要查找应用程序的重定向 URI。Windows 10 为每个应用程序提供了唯一的 URI，确保发送到此 URI 的邮件只发送到该应用程序。要确定您的项目的重定向 URI：

1. 打开 Visual Studio 2015 中的解决方案。 
2. 确保您的平台目标已设置为 x86 或 x64。
3. 按 F5。
4. 应用启动后，选择菜单中位于应用左上方的**复制**按钮![替换文本](../readme-images/copy_icon.png)。这会将该应用程序的重定向 URI 复制到剪贴板。 
5. 存储该值。您将在注册应用时使用该值，如下一节中所述。 


重定向 URI 值将如下所示：
```
ms-appx-web://Microsoft.AAD.BrokerPlugIn/S-1-15-2-694665007-945573255-503870805-3898041910-4166806349-50292026-2305040851
```


<a name="register"></a>
##注册和配置应用

1.	使用您的 Azure AD 凭据登录到 [Azure 管理门户](http://aka.ms/i5b8dz)。
2.	单击左侧菜单中的 **Active Directory**，然后选择 Office 365 开发人员网站的目录。
3.	单击顶部菜单中的**应用程序**。
4.	单击底部菜单中的**添加**。
5.	在**希望执行何种操作页面**上单击**添加组织正在开发的应用程序**。
6.	在**告诉我们您的应用程序信息页面**上选择**本机客户端应用程序**类型，并指定该应用的名称，例如 **O365-UWP-Connect**。
7.	单击页面右下角的箭头图标。
8.	在**应用程序信息**页面上输入您在上一步获得的重定向 URI 值。
9.	成功添加应用程序后，您将被带到应用程序的**快速启动**页面。在该页面的顶部菜单中选择**配置**。
10.	在**其他应用程序的权限**下选择**添加应用程序**。在对话框中选择 **Microsoft Graph** 应用程序。返回应用程序配置页面后，选择**以用户身份发送邮件**和**登录和读取用户配置文件**权限。
11.	在**配置**页面上复制指定给**客户端 ID** 的值。
12.	单击底部菜单中的**保存**。

<a name="build"></a>
## 构建和调试 ##

**注意：** 如果在步骤 2 安装程序包时出现任何错误，请确保您放置该解决方案的本地路径并未太长/太深。将解决方案移动到更接近驱动器根目录的位置可以解决此问题。

1. 在 Visual Studio 中加载解决方案后，通过在 App.xaml 文件的 Application.Resources 节点中添加这些键的相应值配置示例以使用您在 Azure Active Directory 中注册的客户端 id 和您的租户域。
![Office 365 UWP Microsoft Graph 连接示例](../readme-images/ClientTenant.png "App.xaml 文件中的客户端 ID 值")`

2. 按 F5 进行构建和调试。运行解决方案，并使用机构帐户登录 Office 365。


<a name="questions"></a>
## 问题和意见

我们乐意倾听您有关 UWP Microsoft Graph Connect 项目的反馈。您可以在该存储库中的[问题](https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Connect/issues)部分将问题和建议发送给我们。

您的反馈对我们意义重大。请在[堆栈溢出](http://stackoverflow.com/questions/tagged/office365+or+microsoftgraph)上与我们联系。使用 [MicrosoftGraph] 和 [office365] 标记出您的问题。

<a name="additional-resources"></a>
## 其他资源 ##

- [其他 Office 365 Connect 示例](https://github.com/OfficeDev?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph 概述](http://graph.microsoft.io)
- [Office 365 API 平台概述](https://msdn.microsoft.com/office/office365/howto/platform-development-overview)
- [Office 365 API 代码示例和视频](https://msdn.microsoft.com/office/office365/howto/starter-projects-and-code-samples)
- [Office 开发人员代码示例](http://dev.office.com/code-samples)
- [Office 开发人员中心](http://dev.office.com/)


## 版权所有
版权所有 (c) 2015 Microsoft。保留所有权利。


