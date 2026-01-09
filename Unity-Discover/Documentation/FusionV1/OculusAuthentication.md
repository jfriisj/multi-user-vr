# Oculus Authentication

Oculus verifies users based on their Oculus ID and a client-provided nonce. In cryptography, a nonce is an arbitrary number that can only be used once.

---

## Application Setup

Adding Oculus as an authentication provider is done via the [Photon Applications' Dashboard](https://dashboard.photonengine.com/).

1.  Go to the **"Manage"** page of your application.
2.  Scroll down to the **"Authentication"** section.
3.  Add or edit the Oculus provider with the following mandatory settings:
    - **appid**: ID of your Oculus App.
    - **appsecret**: Secret for your Oculus App.

---

## Client Code

### Get Credentials

The client needs to log in to Oculus then generate a nonce, which serves as proof of a valid Oculus user.

#### Unity Instructions

1.  Download the **Oculus Platform SDK for Unity** and import it into your project.
2.  In the Editor's menu bar, go to **"Oculus Platform" -> "Edit Settings"** and enter your Oculus AppId.
3.  Use the following code to initialize the platform, check entitlement, and retrieve the user's ID and nonce:

```csharp
using UnityEngine;
using Oculus.Platform;
using Oculus.Platform.Models;

public class OculusAuth : MonoBehaviour
{
    private string oculusId;

    private void Start()
    {
        Core.AsyncInitialize().OnComplete(OnInitializationCallback);
    }

    private void OnInitializationCallback(Message<PlatformInitialize> msg)
    {
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error during initialization. Error Message: {0}",
                msg.GetError().Message);
        }
        else
        {
            Entitlements.IsUserEntitledToApplication().OnComplete(OnIsEntitledCallback);
        }
    }

    private void OnIsEntitledCallback(Message msg)
    {
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error verifying the user is entitled to the application. Error Message: {0}",
                msg.GetError().Message);
        }
        else
        {
            GetLoggedInUser();
        }
    }

    private void GetLoggedInUser()
    {
        Users.GetLoggedInUser().OnComplete(OnLoggedInUserCallback);
    }

    private void OnLoggedInUserCallback(Message<User> msg)
    {
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error getting logged in user. Error Message: {0}",
                msg.GetError().Message);
        }
        else
        {
            // Use msg.Data.ID, NOT msg.Data.OculusID
            oculusId = msg.Data.ID.ToString();
            GetUserProof();
        }
    }

    private void GetUserProof()
    {
        Users.GetUserProof().OnComplete(OnUserProofCallback);
    }

    private void OnUserProofCallback(Message<UserProof> msg)
    {
        if (msg.IsError)
        {
            Debug.LogErrorFormat("Oculus: Error getting user proof. Error Message: {0}",
                msg.GetError().Message);
        }
        else
        {
            string oculusNonce = msg.Data.Value;
            // You can now use oculusId and oculusNonce for Photon Authentication
        }
    }
}
```

### Authenticate

The client needs to send the **Oculus ID** and the **generated nonce** as query string parameters with the respective keys `"userid"` and `"nonce"`.

In Fusion, this is typically done by setting the `AuthenticationValues` before starting the `NetworkRunner`:

```csharp
var authValues = new AuthenticationValues();
authValues.AuthType = CustomAuthenticationType.Oculus;
authValues.AddAuthParameter("userid", oculusId);
authValues.AddAuthParameter("nonce", oculusNonce);

// Pass authValues to NetworkRunner.StartGame()
```
