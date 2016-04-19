# Exemplo de conexão com o Office 365 para UWP usando o Microsoft Graph

**Sumário**  

* [Introdução](#introduction)  
*[Pré-requisitos](#prerequisites)  
*[Localizar o URI de redirecionamento atribuído pelo sistema](#redirect)  
*[Registrar e configurar o aplicativo](#register)  
*[Criar e depurar](#build)  
*[Perguntas e comentários](#questions)  
*[Recursos adicionais](#additional-resources)  

<a name="introduction"></a>
##Introdução

Este exemplo mostra como conectar o aplicativo Universal do Windows 10 ao Office 365 usando a API do Microsoft Graph (antiga API unificada do Office 365) para enviar emails. O exemplo usa também a nova [API WebAccountManager](http://blogs.technet.com/b/ad/archive/2015/08/03/develop-windows-universal-apps-with-azure-ad-and-the-windows-10-identity-api.aspx) do Windows 10 para autenticar os usuários no locatário.

> Observação: para compreender o código de chamada do Microsoft Graph em um aplicativo da UWP, confira o artigo [Chamar o Microsoft Graph em um aplicativo Universal do Windows 10](https://graph.microsoft.io/docs/platform/uwp).


<a name="prerequisites"></a>
## Pré-requisitos ##

**Observação:** experimente a página [Introdução às APIs do Office 365](http://dev.office.com/getting-started/office365apis?platform=option-windowsuniversal#setup), que simplifica o registro para que você possa executar esse exemplo com mais rapidez.

Esse exemplo requer o seguinte:

  * Visual Studio 2015 
  * Windows 10 ([habilitado para o modo de desenvolvimento](https://msdn.microsoft.com/pt-br/library/windows/apps/xaml/dn706236.aspx))
  * Uma conta do Office 365 para empresas. Inscreva-se para uma [Assinatura de Desenvolvedor do Office 365](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account), que inclui os recursos necessários para começar a criação de aplicativos do Office 365.
  * Um locatário do Microsoft Azure para registrar o seu aplicativo. O Active Directory (AD) do Azure fornece serviços de identidade que os aplicativos usam para autenticação e autorização. Você pode adquirir uma assinatura de avaliação aqui: [Microsoft Azure](http://aka.ms/jjm0q7).

**Importante**: você também deve assegurar que a assinatura do Azure esteja vinculada ao locatário do Office 365. Para saber como fazer isso, confira o artigo [Associar sua conta do Office 365 ao AD do Azure para criar e gerenciar aplicativos](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_CreateAzureSubscription).

<a name="redirect"></a>
## Localizar o URI de redirecionamento atribuído pelo sistema para o aplicativo

Antes de registrar o aplicativo no Portal do Azure, você precisa localizar o URI de redirecionamento do aplicativo. O Windows 10 fornece a cada aplicativo um URI exclusivo e garante que as mensagens enviadas para esse URI sejam enviadas somente para esse aplicativo. Para determinar o URI de redirecionamento do projeto:

1. Abra a solução no Visual Studio 2015. 
2. Verifique se Destino da Plataforma está definido como x86 ou x64.
3. Pressione F5.
4. Depois inicializar o aplicativo, escolha o botão **Copiar**![texto Alt](../readme-images/copy_icon.png) localizado no menu no canto superior esquerdo do aplicativo. Dessa forma, você copia o URI de redirecionamento do aplicativo para a área de transferência. 
5. Armazene esse valor. Você vai usá-lo para registrar o aplicativo, conforme descrito na seção anterior. 


O valor do URI de redirecionamento terá a seguinte aparência: 
```
ms-appx-web://Microsoft.AAD.BrokerPlugIn/S-1-15-2-694665007-945573255-503870805-3898041910-4166806349-50292026-2305040851
```


<a name="register"></a>
##Registrar e configurar o aplicativo

1.	Entre no [Portal de Gerenciamento do Azure](http://aka.ms/i5b8dz) usando as credenciais do AD do Azure.
2.	Clique em **Active Directory** no menu à esquerda e escolha o diretório para o site do desenvolvedor do Office 365.
3.	No menu superior, clique em **Aplicativos**.
4.	Clique em **Adicionar** no menu inferior.
5.	Na página **O que você deseja fazer?**, clique em **Adicionar um aplicativo que minha organização esteja desenvolvendo**.
6.	Na página **Conte-nos sobre o seu aplicativo**, escolha o tipo **APLICATIVO CLIENTE NATIVO** e especifique um nome para o aplicativo, por exemplo, **O365-UWP-Connect**.
7.	Clique no ícone de seta no canto inferior direito da página.
8.	Na página **Informações de Aplicativos**, insira o valor do URI de redirecionamento obtido na etapa anterior.
9.	Após adicionar o aplicativo com êxito, você será direcionado para a página **Início Rápido** do aplicativo. A partir desse local, escolha **Configurar** no menu superior.
10.	Em **Permissões para outros aplicativos**, escolha **Adicionar aplicativo**. Escolha o aplicativo **Microsoft Graph** na caixa de diálogo. Quando retornar à página de configuração do aplicativo, escolha as permissões **Enviar email como usuário** e **Perfil de entrada e de leitura de usuário**.
11.	Copie o valor especificado da **ID do cliente** na página **Configurar**.
12.	Clique em **Salvar** no menu inferior.

<a name="build"></a>
## Criar e depurar ##

**Observação:** caso receba mensagens de erro durante a instalação de pacotes na etapa 2, verifique se o caminho para o local onde você colocou a solução não é muito longo ou extenso. Para resolver esse problema, coloque a solução junto à raiz da unidade.

1. Depois de carregar a solução no Visual Studio, configure o exemplo para usar a ID do cliente registrada no Active Directory do Azure e o domínio do locatário adicionando os valores correspondentes para essas chaves no nó Application.Resources do arquivo App.xaml. 
![Exemplo de conexão com o Office 365 para UWP usando o Microsoft Graph](../readme-images/ClientTenant.png "Valor da ID do cliente no arquivo App.xaml")`

2. Pressione F5 para criar e depurar. Execute a solução e entre no Office 365 com uma conta corporativa.


<a name="questions"></a>
## Perguntas e comentários

Gostaríamos de saber sua opinião sobre o projeto de conexão para UWP usando o Microsoft Graph Você pode enviar perguntas e sugestões na seção [Problemas](https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Connect/issues) deste repositório.

Os seus comentários são importantes para nós. Junte-se a nós na página do [Stack Overflow](http://stackoverflow.com/questions/tagged/office365+or+microsoftgraph). Marque as suas perguntas com [MicrosoftGraph] e [office365].

<a name="additional-resources"></a>
## Recursos adicionais ##

-[Outros exemplos de conexão com o Office 365](https://github.com/OfficeDev?utf8=%E2%9C%93&query=-Connect)
-[Visão geral do Microsoft Graph](http://graph.microsoft.io)
-[Visão geral da plataforma de APIs do Office 365](https://msdn.microsoft.com/office/office365/howto/platform-development-overview)
-[Exemplos de código e vídeos sobre APIs do Office 365](https://msdn.microsoft.com/office/office365/howto/starter-projects-and-code-samples)
-[Exemplos de código para desenvolvedores do Office](http://dev.office.com/code-samples)
-[Centro de Desenvolvimento do Office](http://dev.office.com/)


## Direitos autorais
Copyright © 2015 Microsoft. Todos os direitos reservados.


