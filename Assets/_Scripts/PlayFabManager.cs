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
    [Header("Login Form")]
    [SerializeField] private GameObject loginPage;
    [SerializeField] private InputField emailInput;
    [SerializeField] private InputField passwordInput;

    [Header("Register Form")]
    [SerializeField] private GameObject registerPage;
    [SerializeField] private InputField emailInputReg;
    [SerializeField] private InputField passwordInputReg;
    [SerializeField] private InputField confirmPassword;
    [SerializeField] private Dropdown userTypeDropdown;

    [Header("Error Display")]
    [SerializeField] private GameObject errorPanel;
    [SerializeField] private Text errorText;

    // the following 2 methods toggle between the login and registration forms
    public void OpenLoginForm()
    {
        emailInput.text = "";
        passwordInput.text = "";
        errorPanel.SetActive(false);
        registerPage.SetActive(false);
        loginPage.SetActive(true);
    }

    public void OpenRegisterForm()
    {
        emailInputReg.text = "";
        passwordInputReg.text = "";
        confirmPassword.text = "";
        errorPanel.SetActive(false);
        loginPage.SetActive(false);
        registerPage.SetActive(true);
    }

    /// <summary>
    /// Register a new user to PlayFab
    /// </summary>
    public void OnRegister()
    {
        // first check that the password fields match
        if (passwordInputReg.text != confirmPassword.text)
        {
            OnError("passwords must match");
            return;
        }

        // send request to register new user
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInputReg.text,
            Password = passwordInputReg.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    /// <summary>
    /// Login to PlayFab using the provided email and password
    /// </summary>
    public void OnLogin()
    {
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    /// <summary>
    /// Save the user type to the user's PlayFab data
    /// </summary>
    void SendUserData()
    {
        string userType = userTypeDropdown.options[userTypeDropdown.value].text;
        var userTypeRequest = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "userType", userType }
            }
        };
        PlayFabClientAPI.UpdateUserData(userTypeRequest,
            (UpdateUserDataResult result) => {
                Debug.Log("Sucessfully registered user type " + userType);
                PlayFabDataManager.LoadUserData(); },
            OnError);
    }

    /// <summary>
    /// Retrieve the user's PlayFab data
    /// </summary>
    public void FetchUserData()
    {
        // fetch user data from PlayFab
        Debug.Log("fetching user data");
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    // below are OnSuccess and OnError callbacks for all of the above PlayFab API calls

    /// <summary>
    /// Callback method for handling the user data received from PlayFab
    /// </summary>
    /// <param name="result"></param>
    void OnDataReceived(GetUserDataResult result)
    {
        Debug.Log("received user data");
        if (result.Data != null && result.Data.ContainsKey("userType"))
        {
            Debug.Log(result.Data["userType"].Value + " userType received!");
        }
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Register successful!");
        SendUserData();
        SceneManager.LoadScene(1);
    }

    void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login successful!");
        PlayFabDataManager.LoadUserData();
        SceneManager.LoadScene(1); // loads the main scene of the application
    }

    void OnError(PlayFabError error)
    {
        errorPanel.SetActive(true);
        errorText.text = error.GenerateErrorReport();
    }

    void OnError(string error)
    {
        errorPanel.SetActive(true);
        errorText.text = error;
    }
}
