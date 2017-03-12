# A2BBServer

This project is part of a bigger one, namely Angular 2 Beyond Browser (A2BB), and it contains the Identity Server and API REST server of the A2BB system, written in ASP.NET Core.

The projects which are part of A2BB are:
* [A2BBServer](https://github.com/marcuson/A2BBServer)
* [A2BBAdminApp](https://github.com/marcuson/A2BBAdminApp)
* [A2BBUserSPA](https://github.com/marcuson/A2BBUserSPA)
* [A2BBMobileApp](https://github.com/marcuson/A2BBMobileApp)
* [A2BBGranter](https://github.com/marcuson/A2BBGranter)

and they have been developed as a demo to show the potentials of Angular2 - ASP.NET Core - Arduino technologies during a TechItalian meetup
(see [event here](https://www.meetup.com/TechItaliaTuscany/events/237721715)).

For an A2BB system description, please see [whole project description](#a2bb-description).

If you want to know more about TechItalians, see [TechItalia Tuscany MeetUp page](https://www.meetup.com/it-IT/TechItaliaTuscany/).

This project was generated with [Visual Studio 2017 RC](https://www.visualstudio.com/it/vs/visual-studio-2017-rc/).

## A2BB description

**NOTE**: This is only a system designed to show Angular 2 - ASP.NET Core - Arduino capabilities over a possible realistic problem. As such, it might have security issues/might be improved in both software and hardware aspects/might be done in other ways/etc. Also please note that this README will try to be as clear as possible, but for a full understanding of the system you need to double-check it with the source code.

Angular 2 Beyond Browser (A2BB for short) is a system where some users, who are responsible of restricted areas inside a building, are capable to grant/revoke access to such areas using mobile devices as "keys".

Let's make an example to clarify the statement above. Imagine an University (the *Building* from now on) where there are some IT administrators (the *Administrators* or *Admins*) and some professors (the *Users*), each of whom is responsible of the access to its lab (the *Restricted Area* or *Area* for short). The *Users* might want to grant access to their labs to some students via their mobile phones (the *Devices*) which will be linked to the *User* profile. In front of an *Area*'s door there is an hardware device (the *Granter*) which will recognize the trusted *Devices* for such *Area* and will let the student in or not. *Admins* are in charge of *Users* and *Granters* registration and link between them; *Users* must link the *Devices* of authorized students to their profile and can revoke a *Device* authorization anytime.

More clear? Good. To fulfill such requirements, we can build an architecture with the following elements:

* **A2BB Server**: a server where *Admins* and *Users* profiles and all necessary info are stored on DB, exposing a REST API to manipulate such data.
* **A2BB Admin app**: a desktop app used by *Admins* to create new *Users*, delete old ones, register *Granters* via their serial and link *Granters* to their respective *Users*.
* **A2BB User browser SPA**: a Single Page Application (SPA) accessible from browser where *Users* can link *Devices* to their accounts and grant/revoke access to them.
* **A2BB Mobile app**: the mobile application to install on a student's *Device*, which will be used to complete *Device*-*User* link and will communicate with *Granter* to gain access to an *Area*.
* **A2BB Granter**: an hardware system which will forward a *Device* access request to the A2BB Server API, and will or will not grant access to ita *Area* according to server response.

Let's dive deep into some details for each element of this system.

### The server

This element is actually divided into 2 parts, which are the **Identity Server** and the **REST API Server**; potentially, these servers could run on different machines. They are based on [Identity Server 4](http://docs.identityserver.io/en/release/), [Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity) and [ASP.NET Core MVC](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api).  
The Identity Server is an OAuth/OpenId compatible server which will manage authentication and authorization. The REST API server will provide web services (JSON is used to transfer data between server and client) to manage *Users* and *Granters*, link *Devices*, grant and register access to *Areas* and so on. Some of the REST endpoints will require the user to be authenticated through a bearer token, which can be obtained from the Identity Server via valid *User* or *Admin* credentials.

### The admin app

This is a desktop app used by *Admins* to create new *Users*, register *Granters* via their serials and link *Granters* to their respective *Users*. All these operations are made via REST calls to the Identity Server and REST API Server, and all of them need to be authenticated with *Admin* credentials; a login page is used to manage this aspect. The app is developed using [Electron](https://electron.atom.io/) and [Angular 2](https://angular.io/).

### The user browser SPA

After an *Admin* has registered a new *User*, this one can access its profile area through a web browser at a specific URL. Using its credentials on the login page, it can make authenticated REST calls to the REST API server, using the interface built with [Angular 2](https://angular.io/). So the *User* can change its password, link a student's *Device* to its account using QR codes, grant/revoke *Area* access to a previously linked *Device* and look at which the *Area* access logs.

### The mobile app

The mobile app is responsible to complete link process between the host *Device* and an *User* profile, scanning a QR code visible on the user SPA which holds a temporary registration token and making a suitable link request to the REST API with this token. Once the link is complete, the mobile app will store a unique device id, which will be used in conjunction with a *Granter* to gain access to *Restricted Areas* where the linked *User* is in charge. The app is developed using [Ionic 2](http://ionic.io/2) and [Angular 2](https://angular.io/).

### The granter

The *Granter* is an Arduino-based hardware with TFT LCD, Bluetooth and WiFi modules. The LCD shows a QR code holding *Granter*'s Bluetooth MAC address, name and an OTP generated randomly by Arduino and refreshed each time a *Device* tries to make an access request. An access request is started by a *Device*, which scans the QR code on the *Granter* LCD, connects to *Granter* Bluetooth via its MAC and sends back the OTP along with its *Device* id. The *Granter* will check if the OTP sent matches the internal one; if so, a REST call is made via te WiFi module to the API endpoint that grant access to an *Area*, specifying both the *Device* id and the *Granter* serial. The server will answer with a positive response only if the *Device* has been authorized to enter the *Granter*'s *Area* by the respective *User* who "owns" such *Area*. The *Granter* will report the response outcome to the *Device* through an ACK or NACK via Bluetooth and prepare for a new access request.

### System overview

The whole system overview is represented here through an image for a simpler understanding.

![A2BB Overview](/Docs/overview.png)

**User registration**

1. The *Admin* login using its credentials and gains an access token to make authenticated REST calls.
2. The *Admin* creates a new *User* with username and password.
3. The *Admin* registers a new *Granter* via its serial.
4. The *Admin* links the *Granter* to its *User*.

**Device link**

5. The *User* login to its profile page to make authenticated REST calls.
6. (Optional) The *User* changes its password.
7. The *User* starts a link process. A QR code with link info is displayed on the SPA interface. The user SPA checks periodically for link completion.
8. The *Device* scans the QR code.
9. The *Device* sends link info from QR code to the server.
10. The server replies back with a positive/negative response. If positive, the *Device* stores the unique device id received from server. 
11. After *Device* has acquired its id, the next check call from user SPA will inform that the link has been completed. The GUI can then been refreshed and the newly added *Device* will be visible.

**Access request**

12. The *Device* scans the QR code on the *Granter*.
13. The *Device* connects to the *Granter* via Bluetooth and send the OTP from QR and its unique device id.
14. The *Granter* checks the OTP: if wrong, sends immediatly a NACK back and prepares for a new access request.
15. If the OTP was ok, the *Granter* sends an access request with unique device id and its own serial to the server.
16. The server answers with a positive/negative response according to *User* defined policies.
17. The granter forwards the server response to the *Device* and prepares for a new access request.
18. (Optional) The *User* checks the access of the linked *Devices*.
19. (Optional) The *User* revoke access to the *Device*.

## Prerequisites

You have to install [Visual Studio 2017 RC](https://www.visualstudio.com/it/vs/visual-studio-2017-rc/).
You need to have PostgreSQL installed on the machine where the server runs, listening on default port `5432` with 2 DBs: `a2bb_idsrv` and `a2bb_api`. Before the first server run, make sure to initialize these DBs via the scripts in the `Scripts` folder. Also, make sure that the DB string connection in the `appsettings.json` is correct for your PostgreSQL configuration (if you use the defaults during PostgreSQL installation, the connection string should be ok).

Note that the whole system expects to find the server at address `192.168.1.138`. Obviously, you can expose the server wherever you want, using an IP address or an URL, but you should make sure that:

1. The configuration files/classes in all projects are updated to point to the right IP/URL.
2. The server is accessible from all the elements in the A2BB systems.

## Build

Open the main solution file with Visual Studio, then on the Solution Explorer view right click on root solution and choose `Build`.

## Start server

After you have built the projects with Visual Studio, go to the project root folder, then under `Starter` directory you can find 2 files, `start.bat` and `stop.bat` which you can use to start/stop the Identity Server and API REST server proxied via [Nginx](https://www.nginx.com/resources/wiki/). The files are meant to be used under Windows based OS, but a porting to Linux and OSX based systems should be straightforward.

**NOTE**: if you have not changed the scripts from `Scripts` folder, there is a default administrator account with username "Admin" and password "Admin.123".
