using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro;

public class LeaderBoardManager : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public string statisticName = "Testboard";
    public int fetchDelay = 1;

    /// <summary>
    /// Gets the score from the input field and checks if it's a value.
    /// </summary>
    /// <param name="scoreField">The field the score was taken from.</param>
    public void GetScoreFromField(TMP_InputField scoreField)
    {
        int score;

        //Checks if the score from the input field is a number.
        bool success = int.TryParse(scoreField.text, out score);
        if (success)
        {
            PostScore(score);
        }
        else
        {
            Debug.LogError("The score from the input field isn't a number!");
        }

    }

    /// <summary>
    /// Post the score of the player to the database.
    /// </summary>
    /// <param name="score"></param>
    public void PostScore(int score)
    {
        PlayFabClientAPI.UpdatePlayerStatistics(new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate> {
                //Posts the specific statistic with the score to the database.
                new StatisticUpdate { StatisticName = "Testboard", Value = score},
            }
        },
        //If successful, fetch the new leaderboard data with a delay to try to circumvent lag.
        result => { Invoke("GetLeaderBoard", fetchDelay); },
        //Else cast an error.
        error => { Debug.LogError(error.GenerateErrorReport()); });
    }

    /// <summary>
    /// Fetch leaderboard data. 
    /// </summary>
    public void GetLeaderBoard()
    {
        PlayFabClientAPI.GetLeaderboard(
            new GetLeaderboardRequest()
            {
                //Fetch a specific statistic from the leaderboard.
                StatisticName = statisticName,
            },
            //Save the leaderboard if the fetch was successful
            result => SaveLeaderBoard(result.Leaderboard),
            //Throw an error if fetch wasn't successful
            error => Debug.Log(error.GenerateErrorReport())
        );
    }

    /// <summary>
    /// Saves the leaderboard.
    /// </summary>
    /// <param name="playerLeaderboardEntries">The list of the entries in the leaderboard to be shown.</param>
    void SaveLeaderBoard(List<PlayerLeaderboardEntry> playerLeaderboardEntries) {
        string leaderBoard = "";

        for(int i = 0; i < playerLeaderboardEntries.Count; i++)
        {
            //Adds the display name and the score to a string for each entry in the leaderboard.
            leaderBoard += playerLeaderboardEntries[i].DisplayName + " : "  + playerLeaderboardEntries[i].StatValue.ToString() + "\n"; 
        }

        //Updates the leaderboard textfield when done.
        SetLeaderBoard(leaderBoard);
        
    }

    /// <summary>
    /// Sets the text of the leaderboard textfield to the data from the database.
    /// </summary>
    void SetLeaderBoard(string leaderBoardText)
    {
        if (leaderBoardText != null) textField.text = leaderBoardText;
    }
}
