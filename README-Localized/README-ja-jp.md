# Microsoft Graph を使った UWP 用 Office 365 Connect サンプル

**目次**  

* [はじめに](#introduction)  
* [前提条件](#prerequisites)  
* [システムによって割り当てられたリダイレクト URI を検索する](#redirect)  
* [アプリを登録して構成する](#register)  
* [ビルドとデバッグ](#build)  
* [質問とコメント](#questions)  
* [その他の技術情報](#additional-resources)  

<a name="introduction"></a>
##はじめに

このサンプルでは、Microsoft Graph API (以前は Office 365 統合 API と呼ばれていた) を使い、Windows 10 ユニバーサル アプリを Office 365 に接続して電子メールを送信する方法を示します。また、新しい Windows 10 [WebAccountManager API](http://blogs.technet.com/b/ad/archive/2015/08/03/develop-windows-universal-apps-with-azure-ad-and-the-windows-10-identity-api.aspx) を使って、テナントのユーザーの認証も行います。

> メモ: UWP アプリケーションで Microsoft Graph を呼び出すためのコードを理解するには、「[ユニバーサル Windows 10 アプリで Microsoft Graph を呼び出す](https://graph.microsoft.io/docs/platform/uwp)」をご覧ください。


<a name="prerequisites"></a>
## 前提条件 ##

**メモ:** このサンプルをより迅速に実行するため、「[Office 365 API を使う](http://dev.office.com/getting-started/office365apis?platform=option-windowsuniversal#setup)」ページに記載された登録の簡略化をお試しください。

このサンプルの実行には次が必要です。

  * Visual Studio 2015 
  * Windows 10 ([開発モードが有効](https://msdn.microsoft.com/ja-jp/library/windows/apps/xaml/dn706236.aspx))
  * ビジネス向けの Office 365 アカウント。Office 365 アプリのビルドを開始するために必要なリソースを含む [Office 365 Developer サブスクリプション](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account)にサインアップできます。
  * アプリケーションを登録する Microsoft Azure テナント。Azure Active Directory (AD) は、アプリケーションでの認証と承認に使う ID サービスを提供します。ここでは、試用版サブスクリプションを取得できます。 [Microsoft Azure](http://aka.ms/jjm0q7)。

**重要**: Azure サブスクリプションが Office 365 テナントにバインドされていることを確認する必要もあります。確認する方法については、「[Office 365 アカウントを Azure AD と関連付けて、アプリを作成して管理する](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_CreateAzureSubscription)」で詳細をご覧ください。

<a name="redirect"></a>
## アプリのシステムによって割り当てられたリダイレクト URI を検索する

Azure ポータルでアプリケーションを登録する前に、アプリケーションのリダイレクト URI を確認する必要があります。Windows 10 では、各アプリケーションに一意の URI があり、その URI に送信されたメッセージだけがそのアプリケーションに送信されます。プロジェクト用のリダイレクト URI を特定するには、次の操作を行います。

1. Visual Studio 2015 でソリューションを開きます。 
2. プラットフォーム ターゲットが x86 や x64 に設定されていることを確認します。
3. F5 キーを押します。
4. アプリケーションが起動したら、アプリの左上のメニューにある **[コピー]** ボタン ![代替テキスト](../readme-images/copy_icon.png) をクリックします。これにより、アプリのリダイレクト URI がクリップボードにコピーされます。 
5. この値を保存します。次のセクションで説明するように、この値はアプリを登録する際に使います。 


リダイレクト URI 値は、次のようになります。
```
ms-appx-web://Microsoft.AAD.BrokerPlugIn/S-1-15-2-694665007-945573255-503870805-3898041910-4166806349-50292026-2305040851
```


<a name="register"></a>
##アプリを登録して構成する

1. Azure AD 資格情報を使って、[Azure 管理ポータル](http://aka.ms/i5b8dz)にサインインします。
2. 左側のメニューで **[Active Directory]** をクリックしてから、Office 365 開発者向けサイトのディレクトリを選択します。
3. 上部のメニューで、**[アプリケーション]** をクリックします。
4. 下部のメニューから、**[追加]** をクリックします。
5. **[実行内容] ページ**で、**[所属組織が開発しているアプリケーションの追加]** をクリックします。
6. **[アプリケーション情報の指定] のページ**で、種類に **[ネイティブ クライアント アプリケーション]** を選択し、アプリ名に「**O365-UWP-Connect**」などを指定します。
7.	ページの右下隅にある矢印アイコンをクリックします。
8. **[アプリケーション情報]** ページで、前の手順で取得したリダイレクト URI の値を入力します。
9. アプリケーションが正常に追加されたら、アプリケーションの **[クイック スタート]** ページに移動します。ここから、上部のメニューにある **[構成]** を選択します。
10. **[他のアプリケーションに対するアクセス許可]** で、**[アプリケーションの追加]** を選択します。ダイアログ ボックスで、**Microsoft Graph** アプリケーションを選択します。アプリケーションの構成ページに戻り、**[ユーザーとしてメールを送信する]** と **[サインインとユーザー プロファイルの読み取り]** のアクセス許可を選択します。
11. **[構成]** ページで、**[クライアント ID]** に指定された値をコピーします。
12. 下部のメニューで、**[保存]** をクリックします。

<a name="build"></a>
## ビルドとデバッグ ##

**メモ:** 手順 2 でパッケージのインストール中にエラーが発生した場合は、ソリューションを保存したローカル パスが長すぎたり深すぎたりしていないかご確認ください。デバイスのルート近くにソリューションを移動すると問題が解決します。

1. Visual Studio にソリューションを読み込んだ後、サンプルを構成して、Azure Active Directory に登録したクライアント ID とテナントのドメインを使います。この構成は、App.xaml ファイルの Application.Resources ノードにあるこれらのキーに対応する値を追加して行います。
![Office 365 UWP Microsoft Graph Connect サンプル](../readme-images/ClientTenant.png "App.xaml ファイル内のクライアント ID 値")`

2. F5 キーを押して、ビルドとデバッグを実行します。　ソリューションを実行し、所属組織のアカウントで Office 365 にサインインします。


<a name="questions"></a>
## 質問とコメント

UWP Microsoft Graph Connect プロジェクトに関するフィードバックをお寄せください。質問や提案につきましては、このリポジトリの「[問題](https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Connect/issues)」セクションで送信できます。

お客様からのフィードバックを重視しています。[スタック オーバーフロー](http://stackoverflow.com/questions/tagged/office365+or+microsoftgraph)でご連絡ください。質問には [MicrosoftGraph] と [office365] でタグ付けしてください。

<a name="additional-resources"></a>
## その他の技術情報 ##

- [Office 365 Connect のサンプル](https://github.com/OfficeDev?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph の概要](http://graph.microsoft.io)
- [Office 365 API プラットフォームの概要](https://msdn.microsoft.com/office/office365/howto/platform-development-overview)
- [Office 365 API のコード サンプルとビデオ](https://msdn.microsoft.com/office/office365/howto/starter-projects-and-code-samples)
- [Office 開発者向けコード サンプル](http://dev.office.com/code-samples)
- [Office デベロッパー センター](http://dev.office.com/)


## 著作権
Copyright (c) 2015 Microsoft.All rights reserved.


