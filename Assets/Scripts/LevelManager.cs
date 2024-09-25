using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void OnPlayerLeftRoom(Player otherPlayer) 
    {
        try
        {
            Debug.Log($"El jugador {otherPlayer.NickName} ha abandonado la sala");
            if (otherPlayer.IsInactive)
            {
                Debug.Log($"El jugador {otherPlayer.NickName} ha abandonado la sala");
                if (otherPlayer.IsInactive)
                {
                    Debug.Log("El jugador est� inactivo y puede volver");
                    //Inicia corrutina para esperar 10 segundos
                    StartCoroutine(CheckPlayerRejoin(otherPlayer));
                }
                else
                {
                    Debug.Log("El jugador fue removido permanentemente de la sala");
                    OnConnectedToMaster();
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Se produjo una excepci�n al manejar OnPlayerLeftRoom: {ex.Message}");
        } 
    }
    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private IEnumerator CheckPlayerRejoin(Player otherPlayer)
    {
        //Espera 10 segundos
        yield return new WaitForSeconds(10f);
        //Verifica si el jugador sigue inactivo despu�s de 10 segundos
        if (otherPlayer.IsInactive)
        {
            Debug.Log("El jugador no volvi� despu�s de 10 segundos. Cargando el men� principal...");
            OnConnectedToMaster();
        }
        else
        {
            Debug.Log($"El juagdor {otherPlayer.NickName} ha vuelto.");
        }
    }
    //Sobrescribir el m�todo OnDisconnected para manejar la desconexi�n
    public override void OnDisconnected(DisconnectCause cause)
    {
        try
        {
            Debug.Log($"Desconectado del servidor. Raz�n: {cause}");
            //L�gica para manejar la desconexi�n, por ejemplo, cargar una escena de reconexi�n o el men� principal
            if (cause == DisconnectCause.Exception || cause == DisconnectCause.ServerTimeout)
            {
                Debug.LogWarning("La desconexi�n fue inesperada. Cargando men� principal...");
                SceneManager.LoadScene("MainMenu");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Se produjo una excepci�n al manejar OnDisconnected: {ex.Message}");
        }
    }
}

