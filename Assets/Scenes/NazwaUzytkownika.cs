using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NazwaUzytkownika : MonoBehaviour
{
    public string imie;
    public Text imiepacjenta;
    void Start()
    {
        imie = PlayerPrefs.GetString("ImiePacjenta");
        imiepacjenta.text = imie;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
