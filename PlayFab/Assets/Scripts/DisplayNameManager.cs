using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class DisplayNameManager : MonoBehaviour
{
    /// <summary>
    /// Requests a name change for the player.
    /// </summary>
    /// <param name="displayName">The new display name for the player.</param>
    public void RequestNameChange(string displayName)
    {
        //Cretes a request to change name.
        var displayrequest = new UpdateUserTitleDisplayNameRequest() {
            DisplayName = displayName
        };

        //Request the database to change the display name.
        PlayFabClientAPI.UpdateUserTitleDisplayName(displayrequest, UpdateUserResult, OnLoginFailure);
    }

    /// <summary>
    /// On successful change of a display name.
    /// </summary>
    /// <param name="request">The request that was sent to the database.</param>
    void UpdateUserResult(UpdateUserTitleDisplayNameResult request)
    {
        print("User display name successfully changed.");
    }

    /// <summary>
    /// On failure of change of a display name.
    /// </summary>
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

}
