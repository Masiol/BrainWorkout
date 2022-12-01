using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Podsumowanie : MonoBehaviour
{
    public int dzialaniaPunktyDobre;
    public int dzialaniaPunktyZle;
    public TextMeshProUGUI dzialaniaPunktyDobreTxt;
    public TextMeshProUGUI dzialaniaPunktyZleTxt;
    [Space]
    public int pamiecPunktyDobre;
    public int pamiecPunktyZle;
    public TextMeshProUGUI pamiecPunktyDobreTxt;
    public TextMeshProUGUI pamiecPunktyZleTxt;
    [Space]
    public int koncentracjaPunktyDobre;
    public int koncentracjaPunktyZle;
    public TextMeshProUGUI koncentracjaPunktyDobreTxt;
    public TextMeshProUGUI koncentracjaPunktyZleTxt;
    [Space]
    public int szybkoscPunktyDobre;
    public int szybkoscPunktyZle;
    public TextMeshProUGUI szybkoscPunktyDobreTxt;
    public TextMeshProUGUI szybkoscPunktyZleTxt;

    void Start()
    {
        GetValues();
        SetTxt();
    }
    void GetValues()
    {
        dzialaniaPunktyDobre = PlayerPrefs.GetInt("punktyDzialanie");
        dzialaniaPunktyZle = PlayerPrefs.GetInt("punktyDzialanieBlad");

        pamiecPunktyZle = PlayerPrefs.GetInt("WszystkieZleOdpowiedzi");
        pamiecPunktyDobre = PlayerPrefs.GetInt("WszystkiePoprawneOdpowiedzi");

        koncentracjaPunktyZle = PlayerPrefs.GetInt("koncentracjaZle");
        koncentracjaPunktyDobre = PlayerPrefs.GetInt("koncentracjaDobre");

        szybkoscPunktyDobre = PlayerPrefs.GetInt("szybkoscDobre");
        szybkoscPunktyZle = PlayerPrefs.GetInt("szybkoscZle");
    }
    void SetTxt()
    {
        dzialaniaPunktyDobreTxt.text = dzialaniaPunktyDobre.ToString();
        dzialaniaPunktyZleTxt.text = dzialaniaPunktyZle.ToString();
        pamiecPunktyDobreTxt.text = pamiecPunktyDobre.ToString();
        pamiecPunktyZleTxt.text = pamiecPunktyZle.ToString();
        koncentracjaPunktyDobreTxt.text = koncentracjaPunktyDobre.ToString();
        koncentracjaPunktyZleTxt.text = koncentracjaPunktyZle.ToString();
        szybkoscPunktyDobreTxt.text = szybkoscPunktyDobre.ToString();
        szybkoscPunktyZleTxt.text = szybkoscPunktyZle.ToString();
    }
}
