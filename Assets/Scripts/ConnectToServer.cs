using Photon.Pun;
using UnityEngine.SceneManagement;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        //nos empezamos a conectar
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        //una vez que nos conectamos cargamos main menu
        SceneManager.LoadScene("MainMenu");
    }
}
