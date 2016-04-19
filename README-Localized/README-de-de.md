# Office 365 Connect-Beispiel für UWP unter Verwendung von Microsoft Graph

**Inhalt**  

* [Einführung](#introduction)  
* [Voraussetzungen](#prerequisites)  
* [Suchen des dem System zugewiesenen Umleitungs-URIs](#redirect)  
* [Registrieren und Konfigurieren der App](#register)  
* [Erstellen und Debuggen](#build)  
* [Fragen und Kommentare](#questions)  
* [Zusätzliche Ressourcen](#additional-resources)  

<a name="introduction"></a>
##Einführung

In diesem Beispiel wird das Verbinden Ihrer universellen Windows 10-App zu Office 365 mithilfe der Microsoft Graph-API (wurde zuvor als vereinheitlichte Office 365-API bezeichnet) für das Senden einer E-Mail gezeigt. Darin wird zudem die neue [WebAccountManager-API](http://blogs.technet.com/b/ad/archive/2015/08/03/develop-windows-universal-apps-with-azure-ad-and-the-windows-10-identity-api.aspx) von Windows 10 verwendet, um Benutzer bei Ihrem Mandanten zu authentifizieren.

> Hinweis: Informationen über das Verstehen des Codes für das Aufrufen von Microsoft Graph in einer UWP-App finden Sie unter [Aufrufen von Microsoft Graph in einer universellen Windows 10-App](https://graph.microsoft.io/docs/platform/uwp).


<a name="prerequisites"></a>
## Voraussetzungen ##

**Hinweis: ** Rufen Sie die Seite [Erste Schritte mit Office 365-APIs](http://dev.office.com/getting-started/office365apis?platform=option-windowsuniversal#setup) auf. Auf dieser wird die Registrierung vereinfacht, damit Sie dieses Beispiel schneller ausführen können.

Für dieses Beispiel ist Folgendes erforderlich:

  * Visual Studio 2015 
  * Windows 10 (mit [aktiviertem Entwicklungsmodus](https://msdn.microsoft.com/de-de/library/windows/apps/xaml/dn706236.aspx))
  * Ein Office 365 for Business-Konto Sie können sich für [ein Office 365-Entwicklerabonnement](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account) registrieren. Dieses umfasst die Ressourcen, die Sie zum Erstellen von Office 365-Apps benötigen.
  * Ein Microsoft Azure-Mandant zum Registrieren Ihrer Anwendung. Von Azure Active Directory (AD) werden Identitätsdienste bereitgestellt, die durch Anwendungen für die Authentifizierung und Autorisierung verwendet werden. Hier kann ein Testabonnement erworben werden: [Microsoft Azure](http://aka.ms/jjm0q7).

**Wichtig**: Sie müssen zudem sicherstellen, dass Ihr Azure-Abonnement an Ihren Office 365-Mandanten gebunden ist. Weitere Informationen dafür finden Sie unter [Zuordnen des Office 365-Kontos zu Azure AD zum Erstellen und Verwalten von Apps](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_CreateAzureSubscription).

<a name="redirect"></a>
## Suchen des dem System zugewiesenen Umleitungs-URIs für die App

Bevor Sie die Anwendung im Azure-Portal registrieren können, müssen Sie den Umleitungs-URI der Anwendung herausfinden. Windows-10 stellt für jede Anwendung einen eindeutigen URI bereit und stellt sicher, dass an diesen URI gesendete Nachrichten nur an diese Anwendung gesendet werden. So ermitteln Sie den Umleitungs-URI für Ihr Projekt:

1. Öffnen Sie die Projektmappe in Visual Studio 2015. 
2. Stellen Sie sicher, dass Ihr Plattformziel auf x86 oder x64 festgelegt ist.
3. Drücken Sie F5.
4. Wählen Sie nach dem Start der App die Schaltfläche **Kopieren**![Alternativtext](../readme-images/copy_icon.png) aus, die sich im Menü ganz links der App befindet. Dadurch wird der Umleitungs-URI für die App in die Zwischenablage kopiert. 
5. Speichern Sie diesen Wert. Sie verwenden ihn beim Registrieren der App, wie dies im folgenden Abschnitt beschrieben wird. 


Der Umleitungs-URI-Wert sieht in etwa wie folgt aus: 
```
ms-appx-web://Microsoft.AAD.BrokerPlugIn/S-1-15-2-694665007-945573255-503870805-3898041910-4166806349-50292026-2305040851
```


<a name="register"></a>
##Registrieren und Konfigurieren der App

1.	Melden Sie sich mithilfe Ihrer Azure AD-Anmeldeinformationen beim [Azure-Verwaltungsportal](http://aka.ms/i5b8dz) an.
2.	Klicken Sie im linken Menü auf **Active Directory**, und wählen Sie dann das Verzeichnis für Ihre Office 365-Entwicklerwebsite aus.
3.	Klicken Sie im oberen Menü auf **Anwendungen**.
4.	Klicken Sie im Menü unten auf **Hinzufügen**.
5.	Klicken Sie auf der Seite für die Auswahl der Aktionen auf **Eine von meinem Unternehmen entwickelte Anwendung hinzufügen**
6.	Wählen Sie auf der Seite **Erzählen Sie uns von Ihrer Anwendung** die Option **Systemeigene Clientanwendung** für den Typ aus, und geben Sie einen Namen für die App an, beispielsweise **O365-UWP-Connect**.
7.	Klicken Sie unten rechts auf der Seite auf das Pfeilsymbol.
8.	Geben Sie auf der Seite **Anwendungsinformationen** den während des vorherigen Schritts abgerufenen Umleitungs-URI-Wert ein.
9.	Nachdem die Anwendung erfolgreich hinzugefügt wurde, gelangen Sie zur Seite **Schnellstart** für die Anwendung. Klicken Sie dort im oberen Menü auf **Konfigurieren**
10.	Wählen Sie unter **Berechtigungen für andere Anwendungen** die Option **Anwendung hinzufügen** aus. Wählen Sie im Dialogfeld die Anwendung **Microsoft Graph** aus. Wählen Sie, nachdem Sie zur Anwendungskonfigurationsseite zurückgekehrt sind, die Berechtigungen **E-Mails als Benutzer senden** und **Anmelden und Lesen des Benutzerprofils aktivieren** aus.
11.	Kopieren Sie den auf der Seite **Konfigurieren** angegebenen Wert für **Client-ID**.
12.	Klicken Sie im Menü unten auf **Speichern**

<a name="build"></a>
## Erstellen und Debuggen ##

  
**Hinweis: ** Wenn beim Installieren der Pakete während des Schritts 2 Fehler angezeigt werden, müssen Sie sicherstellen, dass der lokale Pfad, unter dem Sie die Projektmappe abgelegt haben, weder zu lang noch zu tief ist. Dieses Problem lässt sich beheben, indem Sie den Pfad auf Ihrem Laufwerk verkürzen.

1. Konfigurieren Sie nach dem Laden der Projektmappe in Visual Studio das Beispiel für die Verwendung der in der Azure Active Directory registrierten Client-ID und der Domäne Ihres Mandanten, indem Sie die entsprechenden Werte für diese Schlüssel im Knoten „Application.Resources“ der Datei „App.xaml“ hinzufügen. ![Office 365 UWP Microsoft Graph Connect-Beispiel](../readme-images/ClientTenant.png "Client-ID-Wert in der Datei „App.xaml“")

2. Drücken Sie zum Erstellen und Debuggen F5. Führen Sie die Projektmappe aus, und melden Sie sich mithilfe Ihres Organisationskontos bei Office 365 an.


<a name="questions"></a>
## Fragen und Kommentare

Wir schätzen Ihr Feedback hinsichtlich des UWP Microsoft Graph Connect-Projekts. Sie können uns Ihre Fragen und Vorschläge über den Abschnitt [Probleme](https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Connect/issues) dieses Repositorys senden.

Ihr Feedback ist uns wichtig. Nehmen Sie unter [Stack Overflow](http://stackoverflow.com/questions/tagged/office365+or+microsoftgraph) Kontakt mit uns auf. Taggen Sie Ihre Fragen mit [MicrosoftGraph] und [office365].

<a name="additional-resources"></a>
## Zusätzliche Ressourcen ##

- [Weitere Office 365 Connect-Beispiele](https://github.com/OfficeDev?utf8=%E2%9C%93&query=-Connect)
- [Microsoft Graph-Übersicht](http://graph.microsoft.io)
- [Office 365 APIs-Plattformübersicht](https://msdn.microsoft.com/office/office365/howto/platform-development-overview)
- [Office 365-API-Codebeispiele und -Videos](https://msdn.microsoft.com/office/office365/howto/starter-projects-and-code-samples)
- [Office-Entwicklercodebeispiele](http://dev.office.com/code-samples)
- [Office Dev Center](http://dev.office.com/)


## Copyright
Copyright (c) 2015 Microsoft. Alle Rechte vorbehalten.


