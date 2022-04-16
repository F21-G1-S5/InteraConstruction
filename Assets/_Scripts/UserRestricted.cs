using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Hides the gameobject if the user is of a certain type
/// </summary>
public class UserRestricted : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayFabDataManager.GetUserType() == "Trainee")
        {
            gameObject.SetActive(false);
        }
    }
}
