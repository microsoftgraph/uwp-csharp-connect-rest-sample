# Ejemplo Connect de Office 365 para UWP con Microsoft Graph

**Tabla de contenido**  

* [Introducción](#introduction)  
* [Requisitos previos](#prerequisites)  
* [Buscar el URI de redireccionamiento asignado por el sistema](#redirect)  
* [Registrar y configurar la aplicación](#register)  
* [Compilar y depurar](#build)  
* [Preguntas y comentarios](#questions)  
* [Recursos adicionales](#additional-resources)  
  
<a name="introduction"></a>
##Introducción

Este ejemplo muestra cómo conectar su aplicación universal de Windows 10 a Office 365 con la API de Microsoft Graph (anteriormente denominada API unificada de Office 365) para enviar un correo electrónico. También usa la nueva [API de WebAccountManager](http://blogs.technet.com/b/ad/archive/2015/08/03/develop-windows-universal-apps-with-azure-ad-and-the-windows-10-identity-api.aspx) de Windows 10 para autenticar usuarios en el inquilino.

> Nota: Para entender el código para llamar a Microsoft Graph en una aplicación UWP, consulte [Llamar a Microsoft Graph en una aplicación universal de Windows 10](https://graph.microsoft.io/docs/platform/uwp).


<a name="prerequisites"></a>
## Requisitos previos ##

**Nota:** Consulte [Introducción a las API de Office 365](http://dev.office.com/getting-started/office365apis?platform=option-windowsuniversal#setup), que simplifica el registro para que este ejemplo se ejecute más rápidamente.

Este ejemplo requiere lo siguiente:

  * Visual Studio 2015 
  * Windows 10 ([modo de desarrollo habilitado](https://msdn.microsoft.com/es-es/library/windows/apps/xaml/dn706236.aspx))
  * Una cuenta de Office 365 para empresas. Puede registrarse en [una suscripción a Office 365 Developer](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_Office365Account), que incluye los recursos que necesita para comenzar a crear aplicaciones de Office 365.
  * Un inquilino de Microsoft Azure para registrar la aplicación. Azure Active Directory (AD) proporciona servicios de identidad que las aplicaciones usan para autenticación y autorización. Puede adquirir una suscripción de prueba aquí: [Microsoft Azure](http://aka.ms/jjm0q7).

**Importante**: También necesitará asegurarse de que su suscripción a Azure esté enlazada a su inquilino de Office 365. Para ello, consulte [Asociar su cuenta de Office 365 con Azure AD para crear y administrar aplicaciones](https://msdn.microsoft.com/en-us/office/office365/howto/setup-development-environment#bk_CreateAzureSubscription) para obtener más información.

<a name="redirect"></a>
## Buscar el URI de redireccionamiento asignado por el sistema para la aplicación

Para poder registrar la aplicación en el Portal de Azure, necesitará conocer el URI de redireccionamiento de la aplicación. Windows 10 proporciona a cada aplicación un URI único y se asegura de que los mensajes enviados a ese URI solo se envíen a esa aplicación. Para determinar el URI de redireccionamiento del proyecto:

1. Abra la solución en Visual Studio 2015. 
2. Asegúrese de que el destino de la plataforma esté configurado en x86 o x64.
3. Presione F5.
4. Una vez que se inicie la aplicación, elija el botón **Copiar**![Texto alternativo](../readme-images/copy_icon.png) situado en el menú de la esquina superior izquierda de la aplicación. Esto hará que el URI de redireccionamiento de la aplicación se copie en el Portapapeles. 
5. Guarde este valor. Lo usará al registrar la aplicación tal y como se describe en la sección siguiente. 


El valor del URI de redireccionamiento será similar al siguiente:
```
ms-appx-web://Microsoft.AAD.BrokerPlugIn/S-1-15-2-694665007-945573255-503870805-3898041910-4166806349-50292026-2305040851
```


<a name="register"></a>
##Registrar y configurar la aplicación

1.	Inicie sesión en el [Portal de administración de Azure](http://aka.ms/i5b8dz) usando sus credenciales de Azure AD.
2.	Haga clic en **Active Directory** en el menú de la izquierda y, a continuación, seleccione el directorio para el sitio de desarrolladores de Office 365.
3.	En el menú superior, haga clic en **Aplicaciones**.
4.	Haga clic en **Agregar** desde el menú inferior.
5.	En la página **Qué desea hacer**, haga clic en **Agregar una aplicación que mi organización está desarrollando**.
6.	En la página **Cuéntenos acerca de su aplicación**, seleccione **Aplicación de cliente nativa** para escribir y especificar un nombre para la aplicación, por ejemplo **O365-UWP-Connect**.
7.	Haga clic en el icono de flecha en la esquina inferior derecha de la página.
8.	En la página **Información de la aplicación**, escriba el valor del URI de redireccionamiento que obtuvo en el paso anterior.
9.	Una vez que la aplicación se agregó correctamente, se le dirigirá a la página **Inicio rápido** de la aplicación. Allí, seleccione **Configurar** en el menú superior.
10.	En **Permisos de otras aplicaciones**, seleccione **Agregar aplicación**. En el cuadro de diálogo, seleccione la aplicación de **Microsoft Graph**. Después de volver a la página de configuración de la aplicación, seleccione los permisos **Enviar correo como usuario** e **Iniciar sesión y leer el perfil de usuario**.
11.	Copie el valor especificado para el **Id. de cliente** en la página **Configurar**.
12.	Haga clic en **Guardar** en el menú inferior.

<a name="build"></a>  
## Compilar y depurar ##

**Nota:** Si observa algún error durante la instalación de los paquetes en el paso 2, asegúrese de que la ruta de acceso local donde colocó la solución no es demasiado larga o profunda. Para resolver este problema, mueva la solución más cerca de la raíz de la unidad.

1. Después de cargar la solución en Visual Studio, configure el ejemplo para usar el identificador de cliente que registró en Azure Active Directory y el dominio de su inquilino agregando los valores correspondientes de estas claves en el nodo Application.Resources del archivo App.xaml.
![Ejemplo Connect de Office 365 para UWP con Microsoft Graph](../readme-images/ClientTenant.png "Valor del identificador de cliente en el archivo App.xaml")`

2. Presione F5 para compilar y depurar. Ejecute la solución e inicie sesión en Office 365 con su cuenta de la organización.


<a name="questions"></a>
## Preguntas y comentarios

Nos encantaría recibir sus comentarios acerca del proyecto UWP Microsoft Graph Connect. Puede enviarnos sus preguntas y sugerencias a través de la sección [Problemas](https://github.com/OfficeDev/O365-UWP-Microsoft-Graph-Connect/issues) de este repositorio.

Su opinión es importante para nosotros. Conecte con nosotros en [Stack Overflow](http://stackoverflow.com/questions/tagged/office365+or+microsoftgraph). Etiquete sus preguntas con [MicrosoftGraph] y [office365].

<a name="additional-resources"></a>
## Recursos adicionales ##

- [Otros ejemplos Connect para Office 365](https://github.com/OfficeDev?utf8=%E2%9C%93&query=-Connect)
- [Información general de Microsoft Graph](http://graph.microsoft.io)
- [Información general sobre la plataforma de las API de Office 365](https://msdn.microsoft.com/office/office365/howto/platform-development-overview)
- [Vídeos y ejemplos de código de la API de Office 365](https://msdn.microsoft.com/office/office365/howto/starter-projects-and-code-samples)
- [Ejemplos de código de Office Developer](http://dev.office.com/code-samples)
- [Centro para desarrolladores de Office](http://dev.office.com/)


## Copyright
Copyright (c) 2015 Microsoft. Todos los derechos reservados.


