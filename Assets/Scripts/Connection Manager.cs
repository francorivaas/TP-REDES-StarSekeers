using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    private bool HasToCheckForSecondPlayer = false;
    [SerializeField] private float TimeToConnect = 10f;
    private float CurrentTimeWaiting = 0f;
    [SerializeField] private GameObject ErrorPopUp;
    [SerializeField] private Text ErrorTxt;

    [SerializeField] private GameObject PopUpButton;

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        ErrorPopUp.SetActive(true);
 //       ErrorTxt.text = "Tu oponente se ha desconectado, espera un momento";
        PopUpButton.SetActive(true);
        PopUpButton.SetActive(false);
        ErrorTxt.text = "Tu oponente se ha desconectado";
        Time.timeScale = 0;
//        StartCoroutine(WaitSecondPlayer());
    }

    private IEnumerator WaitSecondPlayer()
    {
        yield return new WaitForSeconds(TimeToConnect);

        if (PhotonNetwork.PlayerList.Length == 2)
        {
            HasToCheckForSecondPlayer = false;
            CurrentTimeWaiting = 0;
            ErrorPopUp.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            PopUpButton.SetActive(true);
            ErrorTxt.text = "No se pudo reconectar tu partida";
        }
    }
}
