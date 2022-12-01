using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public int scene;
    void Start()
    {
        //scene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    public void Click()
    {
        SceneManager.LoadScene(scene + 1);
    }
}
