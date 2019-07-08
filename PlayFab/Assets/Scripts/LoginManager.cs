using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class LoginManager : MonoBehaviour
{

    string loginName;
    public DisplayNameManager displayNameManager;


    /// <summary>
    /// Request a PlayFab login with an associated playerName.
    /// </summary>
    /// <param name="playerName">The associated playerName for the login.</param>
    public void RequestLogin(string playerName)
    {
        //Saves the loginname.
        loginName = playerName;
        //Creates a new login request.
        var request = new LoginWithCustomIDRequest { CustomId = playerName, CreateAccount = true };
        //Requests the database to login.
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
    }


    /// <summary>
    /// Calls when the RequestLogin for the player has been submitted succesfully. 
    /// </summary>
    /// <param name="result">The data from the login.</param>
    private void OnLoginSuccess(LoginResult result)
    {
        //If the account is newly created, change the displayname of the account to the same as the loginname.
        if (result.NewlyCreated) displayNameManager.RequestNameChange(loginName);
    }


    /// <summary>
    /// Calls when the RequestLogin for the player has failed. 
    /// </summary>
    /// <param name="error">The error associated with the failed login.</param>
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }


}
