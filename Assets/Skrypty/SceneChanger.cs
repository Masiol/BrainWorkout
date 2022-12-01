using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public float time;
    public bool changeImmediatelyOnStart;
    public bool chaneSceneOnButton;
    public string scena;

    void Start()
    {
        if(changeImmediatelyOnStart)
            SceneManager.LoadScene(scena);
    }

    // Update is called once per frame
    void Update()
    {
        if (!chaneSceneOnButton && !changeImmediatelyOnStart)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                SceneManager.LoadScene(scena);
            }
        }
    }
    public void ChangeSceneOnButton()
    {
        if(chaneSceneOnButton)
            SceneManager.LoadScene(scena);
    }
}
