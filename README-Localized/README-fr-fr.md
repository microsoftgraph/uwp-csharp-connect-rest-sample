# Exemple de connexion d’UWP à Office 365 avec Microsoft Graph

**Sommaire**  

* [Introduction](#introduction)  
* [Conditions préalables](#prerequisites)  
* [Recherche de l’URI de redirection attribué par le système](#redirect)  
* [Enregistrement et configuration de l’application](#register)  
* [Création et débogage](#build)
* [Questions et commentaires](#questions)
* [Ressources supplémentaires](#additional-resources)

<a name="introduction"></a>
##Introduction

Cet exemple explique comment connecter votre application Windows 10 universelle à Office 365 avec Microsoft Graph (anciennement appelé API unifiée Office 365) pour envoyer un courrier électronique. Il utilise également la nouvelle [API WebAccountManager](http://blogs.technet.com/b/ad/archive/2015/08/03/develop-windows-universal-apps-with-azure-ad-and-the-windows-10-identity-api.aspx) de Windows 10 pour authentifier les utilisateurs de votre client.

> Remarque : pour mieux comprendre le code d’appel de Microsoft Graph dans une application UWP, consultez la rubrique relative à l’[appel de Microsoft Graph dans une application Windows 10 universelle](https://graph.microsoft.io/docs/platform/uwp).


<a name="prerequisites"></a>
## Conditions préalables ##

**Remarque :** consultez la page relative à la [prise en main des API Office 365](http://dev.office.com/getting-started/office365apis?platform=option-windowsuniversal#setup) pour enregistrer plus facilement votre application et exécuter plus rapidement cet exemple.

Cet exemple nécessite les éléments suivants :

  * Visual Studio 2015 
  * Windows 10 ([avec mode de développement](https://msdn.microsoft.com/fr-fr/library/windows/apps/xaml/dn706236.aspx))
  * Un compte professionnel Office 365. Vous pouvez vous inscrire à [Office 365 Developer](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account) pour accéder aux ressources dont vous avez besoin pour commencer à créer des applications Office 365.
  * Un client Microsoft Azure pour enregistrer votre application. Azure Active Directory (AD) fournit des services d’identité que les applications utilisent à des fins d’authentification et d’autorisation. Un abonnement d’évaluation peut être demandé ici : [Microsoft Azure](http://aka.ms/jjm0q7).

**Important** : vous devrez également vous assurer que votre abonnement Azure est lié à votre client Office 365. Pour cela, consultez [Association de votre compte Office 365 à Azure AD pour créer et gérer des applications](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_CreateAzureSubscription) pour obtenir plus d’informations.

<a name="redirect"></a>
## Recherche de l’URI de redirection attribué par le système

Avant d’enregistrer l’application dans le portail Azure, vous devez connaître l’URI de redirection de l’application. Windows 10 fournit un URI unique à chaque application et vérifie que les messages envoyés à cet URI sont envoyés uniquement à cette application. Pour déterminer l’URI de redirection pour votre projet :

1. Ouvrez la solution dans Visual Studio 2015. 
2. Assurez-vous que votre plateforme cible est définie sur x86 ou x64.
3. Appuyez sur F5.
4. Une fois l’application démarrée, cliquez sur le bouton **Copier**![Texte de remplacement](../readme-images/copy_icon.png) situé dans le menu, en haut à gauche de l’application. L’URI de redirection pour l’application est alors copié dans le presse-papiers. 
5. Conservez cette valeur. Vous en aurez besoin pour enregistrer l’application, comme décrit dans la rubrique suivante. 


La valeur de l’URI de redirection doit ressembler à ceci : 
```
ms-appx-web://Microsoft.AAD.BrokerPlugIn/S-1-15-2-694665007-945573255-503870805-3898041910-4166806349-50292026-2305040851
```


<a name="register"></a>
##Enregistrement et configuration de l’application

1.	Connectez-vous au [Portail de gestion Azure](http://aka.ms/i5b8dz) à l’aide de vos informations d’identification Azure AD.
2.	Cliquez sur **Active Directory** dans le menu de gauche, puis sélectionnez le répertoire sur votre site de développeur Office 365.
3.	Dans le menu supérieur, cliquez sur **Applications**.
4.	Cliquez sur **Ajouter** dans le menu inférieur.
5.	Sur la page **Que souhaitez-vous faire ?**, cliquez sur **Ajouter une application développée par mon entreprise**.
6.	Sur la page **Parlez-nous de votre application**, sélectionnez ** APPLICATION CLIENTE NATIVE** pour indiquer le nom de l’application, par exemple **O365-UWP-Connect**.
7.	Cliquez sur l’icône de flèche en bas à droite de la page.
8.	Sur la page **Informations sur l’application**, entrez la valeur de l’URI de redirection que vous avez obtenue pendant l’étape précédente.
9.	Une fois l’application ajoutée, vous serez redirigé vers la page **Démarrage rapide** de l’application. Maintenant, vous pouvez sélectionner **Configurer** dans le menu supérieur.
10.	Sous **Autorisations accordées à d’autres applications**, sélectionnez **Ajouter une application**. Dans la boîte de dialogue, sélectionnez l’application **Microsoft Graph**. Une fois revenu sur la page de configuration de l’application, sélectionnez les autorisations **Envoyer un courrier électronique en tant qu’utilisateur** et **Se connecter et lire le profil utilisateur**.
11.	Copiez la valeur **ClientID** sur la page **Configurer**.
12.	Cliquez sur **Enregistrer** dans le menu inférieur.

<a name="build"></a>
## Création et débogage ##

**Remarque :** si vous constatez des erreurs pendant l’installation des packages à l’étape 2, vérifiez que le chemin d’accès local où vous avez sauvegardé la solution n’est pas trop long/profond. Pour résoudre ce problème, il vous suffit de déplacer la solution dans un dossier plus près du répertoire racine de votre lecteur.

1. Une fois la solution chargée dans Visual Studio, configurez l’exemple pour utiliser l’ID client que vous avez enregistré dans Azure Active Directory et le domaine de votre client en ajoutant les valeurs correspondantes de ces clés dans le nœud Application.Resources du fichier App.xaml.
![Exemple de connexion d’UWP à Office 365 avec Microsoft Graph](../readme-images/ClientTenant.png "Valeur de l’ID client dans le fichier App.xaml")`

2. Appuyez sur F5 pour créer et déboguer l’application. Exécutez la solution et connectez-vous à Office 365 avec votre compte professionnel.


<a name="questions"></a>
## Questions et commentaires

Nous serions ravis de connaître votre opinion sur le projet de connexion d’UWP avec Microsoft Graph. Vous pouvez nous faire part de vos questions et suggestions dans la rubrique [Problèmes](https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Connect/issues) de ce référentiel.

Votre avis compte beaucoup pour nous. Communiquez avec nous sur [Stack Overflow](http://stackoverflow.com/questions/tagged/office365+or+microsoftgraph). Posez vos questions avec les tags [MicrosoftGraph] et [Office 365].

<a name="additional-resources"></a>
## Ressources supplémentaires ##

- [Autres exemples de connexion à Office 365](https://github.com/OfficeDev?utf8=%E2%9C%93&query=-Connect)
- [Présentation de Microsoft Graph](http://graph.microsoft.io)
- [Présentation de la plateforme des API Office 365](https://msdn.microsoft.com/office/office365/howto/platform-development-overview)
- [Vidéos et exemples de code d’API Office 365](https://msdn.microsoft.com/office/office365/howto/starter-projects-and-code-samples)
- [Exemples de code du développeur Office](http://dev.office.com/code-samples)
- [Centre de développement Office](http://dev.office.com/)


## Copyright
Copyright (c) 2015 Microsoft. Tous droits réservés.


