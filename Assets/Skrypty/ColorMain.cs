using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ColorMain : MonoBehaviour
{
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

}

//{


   // Image r = gameObject.GetComponent<Image>();
    //LeanTween.value(gameObject, 0, 1, 1).setLoopPingPong().setOnUpdate((float val) =>
    //{
        //Color c = r.color;
        //c.a = val;
       // r.color = c;
    //});
//}