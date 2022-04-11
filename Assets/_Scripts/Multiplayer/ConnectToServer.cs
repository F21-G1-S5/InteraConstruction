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

    public string[] allowedHosts;

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
        joinBtn.SetActive(true);

        string userType = PlayFabDataManager.GetUserType();
        foreach (string role in allowedHosts)
        {
            if (role == userType)
            {
                hostBtn.SetActive(true);
                break;
            }
        }
    }


}
