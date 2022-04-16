using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// WIP
public class Teleportation : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private GameObject player;

    // workaround for a bug with the tutorial prompts not disappearing
    [SerializeField] private TutorialManager tManager;

    public void Teleport()
    {
        if (player)
        {
            //tManager.Clear();
            CharacterController cc = player.GetComponent<CharacterController>();
            if (cc != null)
            {
                cc.enabled = false;
            }

            player.transform.position = destination.position;

            if (cc != null)
            {
                cc.enabled = true;
            }

        }
    }

    private void Awake()
    {
        if (player == null)
        {
            // if a player object wasn't provided in the editor, we try to register it on awake
            PlayerMovement[] players = FindObjectsOfType<PlayerMovement>();
            foreach (PlayerMovement p in players)
            {
                if (p.isLocalPlayer())
                {
                    player = p.gameObject;
                    break;
                }
            }
        }
    }
}
