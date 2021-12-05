using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptTutDes : MonoBehaviour
{
    public GameObject uiObject;
    public AudioSource forkliftAudio;
    // Start is called before the first frame update
    void Start()
    {
        uiObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.tag == "Player")
        {
            uiObject.SetActive(true);
            forkliftAudio.Play();
            StartCoroutine("WaitForSec");
        }
    }
    IEnumerator WaitForSec()
    {
        yield return new WaitForSeconds(4);
        forkliftAudio.Stop();
        uiObject.SetActive(false);
        //Destroy(gameObject);
    }
}
