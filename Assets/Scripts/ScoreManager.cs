using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con UI

public class ScoreManager : MonoBehaviour
{
    // Diccionario para almacenar los puntos asociados a diferentes objetos
    private Dictionary<string, int> pointValues;

    // Puntuación de los jugadores
    private int player1Score;
    private int player2Score;

    // Referencias al texto en la UI
    public Text player1ScoreText;
    public Text player2ScoreText;

    void Start()
    {
        // Inicializa los valores de puntos
        pointValues = new Dictionary<string, int>
        {
            { "Player1", 100 }, // Puntos que obtiene el jugador 2 al golpear al jugador 1
            { "Player2", 100 }, // Puntos que obtiene el jugador 1 al golpear al jugador 2
            { "Obstacle", 50 },  // Puntos por destruir obstáculos
            { "OtherObject", 25 } // Otros elementos de juego
        };

        player1Score = 0;
        player2Score = 0;

        // Actualiza los textos inicialmente
        UpdateScoreUI();
    }

    // Método para añadir puntos según el objeto colisionado
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

            // Actualiza los textos en la UI
            UpdateScoreUI();

            Debug.Log("Player " + playerNumber + " scored " + points + " points!");
        }
        else
        {
            Debug.LogWarning("No point value defined for object tag: " + objectTag);
        }
    }

    // Método para obtener la puntuación actual de un jugador
    public int GetScore(int playerNumber)
    {
        if (playerNumber == 1)
            return player1Score;
        else if (playerNumber == 2)
            return player2Score;

        return 0;
    }

    // Método para actualizar la UI con los puntajes actuales
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
}