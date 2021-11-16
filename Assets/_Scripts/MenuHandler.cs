using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }

    public void LOAD_SCENE(string SceneName)
    {

        SceneManager.LoadScene(SceneName);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
