using UnityEngine;
using Photon.Pun;

/// <summary>
/// <c>ConnectToServer</c> connects to the Photon Network when the scene starts
/// and exposes the required menu buttons upon successfully connecting
/// </summary>
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    public GameObject hostBtn;
    public GameObject joinBtn;
    public GameObject lblLoad;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(); // connect to master lobby
    }

    public override void OnJoinedLobby()
    {
        lblLoad.SetActive(false);
        hostBtn.SetActive(true);
        joinBtn.SetActive(true);
    }


}
