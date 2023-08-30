using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

public static class AuthenticationHandler
{
    public static AuthState AuthState { get; private set; } = AuthState.NotAuthenticated;

    //Authenticate and give back an AuthState
    public static async Task<AuthState> DoAuth(int maxRetries = 5)
    {
        //Check if we are already authenticated
        if(AuthState == AuthState.Authenticated)
        {
            return AuthState;
        }

        if(AuthState == AuthState.Authenticating)
        {
            Debug.LogWarning("Already authenticating");
            await AuthenticatingAsync();
            return AuthState;
        }

        //Try to sign in anonymously
        await SignInAnonymouslyAsync(maxRetries);

        return AuthState;
    }

    private static async Task<AuthState> AuthenticatingAsync()
    {
        while (AuthState == AuthState.Authenticating || AuthState == AuthState.NotAuthenticated)
        {
            await Task.Delay(200);
        }

        return AuthState;
    }

    private static async Task SignInAnonymouslyAsync(int maxRetries)
    {
        //Go to the authenticating phase if not authenticated yet
        AuthState = AuthState.Authenticating;

        int retries = 0;

        //Try to authenticate a few times until we're successful
        while (AuthState == AuthState.Authenticating && retries < maxRetries)
        {
            try
            {
                //Sign in with a service. AnonymouslyAsynch mean you don't have to enter anything.
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                //Check if the authentication succeeded
                if (AuthenticationService.Instance.IsSignedIn && AuthenticationService.Instance.IsAuthorized)
                {
                    AuthState = AuthState.Authenticated;
                    break;
                }
            }
            catch(AuthenticationException authException)
            {
                Debug.LogError(authException);
                AuthState = AuthState.Error;
            }
            catch(RequestFailedException reqException)
            {
                Debug.LogError(reqException);
                AuthState = AuthState.Error;
            }

            retries++;
            await Task.Delay(1000);
        }

        if(AuthState != AuthState.Authenticated)
        {
            Debug.LogWarning($"Player was not signed in successfully after {retries} tries.");
            AuthState = AuthState.TimeOut;
        }
    }
}

public enum AuthState
{
    NotAuthenticated,
    Authenticating,
    Authenticated,
    Error,
    TimeOut
}
