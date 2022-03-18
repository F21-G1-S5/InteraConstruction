using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

/// <summary>
/// <c>CreateAndJoinRooms</c> provides event handlers to the scene for both hosting
/// and joining a room through Photon
/// </summary>
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public InputField createRoomInput;
    public InputField joinRoomInput;

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createRoomInput.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinRoomInput.text);
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        PhotonNetwork.LoadLevel("MultiPlayerScene");
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveLobby();
        PhotonNetwork.Disconnect();

        SceneManager.LoadScene("MainMenu");
    }

}
