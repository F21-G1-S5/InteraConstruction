using UnityEngine;
using Photon.Pun;

/// <summary>
/// <c>SpawnPlayers</c> instantiates a player object over PhotonNetwork when the scene is loaded.
/// </summary>
public class SpawnPlayers : MonoBehaviour
{
    public GameObject playerPrefab;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minZ;
    public float maxZ;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);


    }

    private void LateUpdate()
    {
        transform.position = playerPrefab.transform.position;
    }
}
