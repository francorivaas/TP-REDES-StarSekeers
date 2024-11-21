using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ScoreManager : MonoBehaviourPun
{
    private Dictionary<string, int> pointValues;

    private int player1Score;
    private int player2Score;
    public Text player1ScoreText;
    public Text player2ScoreText;
    private static ScoreManager instance;
    void Awake() 
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        pointValues = new Dictionary<string, int>
        {
            { "Player1", 100 }, 
            { "Player2", 100 }, 
            { "Obstacle", 50 },
            { "OtherObject", 25 } 
        };

        player1Score = 0;
        player2Score = 0;
        UpdateScoreUI();
    }

    public void AddScore(string objectTag, int playerNumber)
    {
        if (pointValues.ContainsKey(objectTag))
        {
            int points = pointValues[objectTag];
            if (playerNumber == 1)
            {
                player1Score += points;
            }
            else if (playerNumber == 2)
            {
                player2Score += points;
            }
            photonView.RPC("SyncScores", RpcTarget.All, player1Score, player2Score);
            UpdateScoreUI();

            Debug.Log("Player " + playerNumber + " scored " + points + " points!");
        }
        else
        {
            Debug.LogWarning("No point value defined for object tag: " + objectTag);
        }
    }

    public int GetScore(int playerNumber)
    {
        if (playerNumber == 1)
            return player1Score;
        else if (playerNumber == 2)
            return player2Score;

        return 0;
    }
    private void UpdateScoreUI()
    {
        if (player1ScoreText != null)
        {
            player1ScoreText.text = "Player 1: " + player1Score.ToString();
        }

        if (player2ScoreText != null)
        {
            player2ScoreText.text = "Player 2: " + player2Score.ToString();
        }
    }

    [PunRPC]
    private void SyncScores(int newPlayer1Score, int newPlayer2Score)
    {
        player1Score = newPlayer1Score;
        player2Score = newPlayer2Score;
        UpdateScoreUI();
    }
}