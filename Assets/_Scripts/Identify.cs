using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Identify : MonoBehaviour
{
    private string password = "create";
    public InputField userInput;
    public string input;
    private string correct = "Scenes/Menus/HostServer";
    private string wrong = "Scenes/Menus/MainMenu";

    public void submitButton()
    {
        input = userInput.text;
        
        if(input == password)
        {
            SceneManager.LoadScene(correct);
        }
        else
        {
            SceneManager.LoadScene(wrong);
        }
    }
}
