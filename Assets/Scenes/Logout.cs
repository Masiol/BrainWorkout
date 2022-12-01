using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logout : MonoBehaviour
{
    public AuthManager authManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LogoutUser()
    {
        authManager.Logout();
        StartCoroutine(ExitApp());
    }
    IEnumerator ExitApp()
    {
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }
}
