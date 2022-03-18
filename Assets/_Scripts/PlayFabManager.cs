using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages communication with PlayFab server.
/// 
/// Provides methods for logging in and registering through PlayFab
/// </summary>
public class PlayFabManager : MonoBehaviour
{
    [Header("UI")]
    public InputField emailInput;
    public InputField passwordInput;

    public void RegisterButton()
    {
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        SceneManager.LoadScene(1);
    }

    public void LoginButton()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        SceneManager.LoadScene(1); // loads the main scene of the application
    }

    void OnError(PlayFabError error)
    {
        // TODO: display PlayFab response though a text element in the UI
    }
}
