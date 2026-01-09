# Authentication Overview

When you start a new project with any Photon SDK, you might notice that users (a.k.a. players) are anonymous. There is no need to login as user and you may notice that each new session gets a random `userId` assigned.

This is great to get projects started but there are serious disadvantages:
- No security against impersonation, anyone can claim any `userId`
- Any inventory or stats are provided client side and easy to manipulate
- Users can not identify others or find friends
- Everyone has unconditional access to the online component of your app
- Malicious users can not be banned

Due to this, we strongly recommend to add authentication to all apps before a general release. While Photon itself does not provide user accounts, third party services can easily be integrated. Once setup, Photon uses a server-to-server REST API to authenticate users. These services can grant or deny access to Photon.

---

## Anonymous Users

Even if your Photon App does not require server-side authentication, clients always have to send an authentication operation. The default authentication request uses `CustomAuthenticationType.None` and happens behind the scenes when a client connects.

Unless your client code sets any credentials, the server assigns a new GUID as `userId` which lasts until the session ends. Clients are by default allowed to identify themselves by sending a `userId` but this is not checked in any way. Storing and re-using a `userId` client side is a very simple way to "identify" users but also very vulnerable to identity theft. This should be replaced with proper server side authentication.

### Rejecting Anonymous Users

Even after setting up Authentication Provider(s) for your App, clients can still try to use `AuthenticationValues.CustomAuthenticationType = CustomAuthenticationType.None`. To reject these clients, make sure to uncheck **"Allow anonymous clients to connect"** per App in the Dashboard.

---

## Authentication Setup

Authentication requires some coordination between the server side and your clients.

1.  **Dashboard Setup**: Per application (and corresponding AppId), the setup is done in the Photon Dashboard first. Services can be predefined or added as a "Custom Server".
2.  **Client-Side Setup**: Clients always send an authentication operation when they connect. To request authentication with a specific provider, clients must set their `AuthenticationValues` before they connect.

The client's `AuthenticationValues.CustomAuthenticationType` value defines which service to use.

### Predefined Providers

Photon implements several popular user account services directly (e.g., Facebook, Steam, Oculus, etc.). For each service, the Photon Dashboard will ask for a set of values to make the association.

### Custom Authentication

Used to integrate any service not covered by predefined providers. A web service must be setup to answer Photon's requests for authorization via a REST API.

### PlayFab Integration

While PlayFab is a popular "user backend service", its authentication is implemented as Custom Authentication.

---

## Adding Voice and Chat

The Voice and Chat SDKs are separate solutions and have their own Authentication configurations. We recommend configuring each AppId to offer the same Authentication options as Fusion to ensure consistency. The Voice SDK for Unity even comes with an option to re-use the `AuthenticationValues` of Fusion.
