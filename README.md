# Twitter Backup

**Live demo**: [http://twitter-backup.azurewebsites.net/](http://twitter-backup.azurewebsites.net)

## Wiki pages
  1. [DataModel.md](Wiki/DataModel.md)
  1. [SSL.md](Wiki/SSL.md)

### Table of Contents
  1. [Twitter](#twitter)
  1. [Security](#security)

### Twitter

##### Microsoft Owin

> Middleware that enables an application to support Twitter's OAuth 2.0 authentication workflow.

We use version: 3.0.1

```bash
PM> Install-Package Microsoft.Owin.Security.Twitter
```

#### TweetSharp

> TweetSharp v2 is a fast, clean wrapper around the Twitter API. It uses T4 templates to make adding new endpoints easy.

```bash
PM> Install-Package TweetSharp
```

### Sources:
  1. [Microsoft Owin Twitter](https://www.nuget.org/packages/Microsoft.Owin.Security.Twitter/)
  1. [Code! MVC 5 App with Facebook, Twitter, LinkedIn and Google OAuth2 Sign-on (C#) | The ASP.NET Site](http://www.asp.net/mvc/overview/security/create-an-aspnet-mvc-5-app-with-facebook-and-google-oauth2-and-openid-sign-on)
  
**[⬆ back to top](#table-of-contents)**

### Security 

We will store all configuration in external files.

Our need to have `Security/AppSettingsSecrets.config`.

```xml
<appSettings> 
	
	<!-- Twitter -->
	<add key="TwitterConsumerKey" value="AppTwitterConsumerKey"/>
	<add key="TwitterConsumerSecret" value="AppTwitterConsumerSecret" />
	<!-- Twitter -->
	  
</appSettings>
```

### Sources:
  1. [Best practices for deploying passwords and other sensitive data to ASP.NET and Azure App Service](http://www.asp.net/identity/overview/features-api/best-practices-for-deploying-passwords-and-other-sensitive-data-to-aspnet-and-azure)
  
**[⬆ back to top](#table-of-contents)**
