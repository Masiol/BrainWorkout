using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wyniki : MonoBehaviour
{
    public int koncentracjaPunktyDobre;
    public int szybkoscPunktyDobre;
    public int pamiecPunktyDobre;
    public int dzialaniaPunktyDobre;
    public int suma;
    public Text wyniki;
    public int sumaPrezentow;
    public Nagrody nagrody;

    // Start is called before the first frame update
    void Start()
    {
        
        dzialaniaPunktyDobre = PlayerPrefs.GetInt("punktyDzialanie");
        pamiecPunktyDobre = PlayerPrefs.GetInt("WszystkiePoprawneOdpowiedzi");
        koncentracjaPunktyDobre = PlayerPrefs.GetInt("koncentracjaDobre");
        szybkoscPunktyDobre = PlayerPrefs.GetInt("szybkoscDobre");
        
        suma = dzialaniaPunktyDobre + pamiecPunktyDobre + koncentracjaPunktyDobre + szybkoscPunktyDobre + sumaPrezentow;
        PlayerPrefs.SetInt("sumaWszystkich",suma);
        wyniki.text = suma.ToString();

        //SetTxt();
        if (nagrody == null)
            return;
    }

    // Update is called once per frame
    void Update()
    {
        sumaPrezentow = PlayerPrefs.GetInt("sumaNagrod");
        suma = dzialaniaPunktyDobre + pamiecPunktyDobre + koncentracjaPunktyDobre + szybkoscPunktyDobre + sumaPrezentow;
        wyniki.text = suma.ToString();
    }
}
