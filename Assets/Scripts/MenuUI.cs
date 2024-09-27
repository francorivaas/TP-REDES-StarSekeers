using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMPro.TMP_InputField createInput;
    [SerializeField] private TMPro.TMP_InputField joinInput;
    [SerializeField] private TMPro.TMP_Text feedbackText; // Para mostrar mensajes de error

    private void Awake()
    {
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    private void OnDestroy()
    {
        createButton.onClick.RemoveAllListeners();
        joinButton.onClick.RemoveAllListeners();
    }

    private void CreateRoom()
    {
        RoomOptions roomConfiguration = new RoomOptions();
        roomConfiguration.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(createInput.text, roomConfiguration);
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Gameplay");
    }
    //Sobrescribir OnJoinRoomFailed para manejar los errores al unirse a una sala
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        string errorMessage;
        switch (returnCode)
        {
            case ErrorCode.GameFull:
                errorMessage = "La sala está llena. Por favor, intenta unirte a otra sala.";
                break;
            case ErrorCode.GameDoesNotExist:
                errorMessage = "La sala no existe. Verifica el nombre de la sala o crea una nueva.";
                break;
            default:
                errorMessage = $"Error al unirse a la sala: {message} (Código de error: {returnCode})";
                break;
        }
        Debug.LogError(errorMessage);
        if (feedbackText != null) feedbackText.text = errorMessage;
    }
}
