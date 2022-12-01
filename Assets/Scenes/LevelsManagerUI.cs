using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsManagerUI : MonoBehaviour
{
    public static LevelsManagerUI instance;

    //Screen object variables
    public GameObject _Obszary;
    public GameObject _Poziomy;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void Obszary() 
    {
        _Obszary.SetActive(true);
        _Poziomy.SetActive(false);
    }
    public void Poziomy() 
    {
        _Obszary.SetActive(false);
        _Poziomy.SetActive(true);
    }
}