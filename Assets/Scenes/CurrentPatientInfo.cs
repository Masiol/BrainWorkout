using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentPatientInfo : MonoBehaviour
{
    public string PatientID;
    public int Level;
    public int Area;

    //Area = 1 - Logika
    //Area = 2 - Pamiêæ
    //Area = 3 - Koncentracja
    //Area = 4 - Szybkoœæ
    [Header("Logika")]
    public int LogikaPoprawne;
    public int LogikaZle;
    
    [Header("Pamieæ")]
    public int PamiecaPoprawne;
    public int PamiecZle;

    [Header("Koncentracja")]
    public int KoncentracjaPoprawne;
    public int KoncentracjaZle;

    [Header("Szybkosc")]
    public int SzybkoscPoprawne;
    public int SzybkoscZle;

    public static CurrentPatientInfo instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PatientID = PlayerPrefs.GetString("NumerPacjenta");
        Level = PlayerPrefs.GetInt("CurrentLevel");
        Area = PlayerPrefs.GetInt("CurrentArea");
        GetData();
    }
    public void SetLevelNumber(int i)
    {
        Level = i;
    }
    public void SetAreaNumber(int i)
    {
        Area = i;
    }

    public void SaveLogic(int LP, int LZ)
    {
        PlayerPrefs.SetInt("LogikaPoprawne", LP);
        PlayerPrefs.SetInt("LogikaZle", LZ);
        GetData();
        SendData(Level, Area, LP, LZ);
    }
    public void SaveMemeory(int PP, int PZ)
    {
        PlayerPrefs.SetInt("PamiecaPoprawne", PP);
        PlayerPrefs.SetInt("PamiecZle", PZ);
        GetData();
        SendData(Level, Area, PP, PZ);
    }
    public void SaveKoncentracja(int KP, int KZ)
    {
        PlayerPrefs.SetInt("KoncentracjaPoprawne", KP);
        PlayerPrefs.SetInt("KoncentracjaZle", KZ);
        GetData();
        SendData(Level, Area, KP, KZ);
    }
    public void SaveSpeed(int SP, int SZ)
    {
        PlayerPrefs.SetInt("SzybkoscPoprawne", SP);
        PlayerPrefs.SetInt("SzybkoscZle", SZ);
        GetData();
        SendData(Level, Area, SP, SZ);
    }

    public void SetData()
    {
        PlayerPrefs.SetInt("CurrentLevel", Level);
        PlayerPrefs.SetInt("CurrentArea", Area);
    }
    private void GetData()
    {
        LogikaPoprawne = PlayerPrefs.GetInt("LogikaPoprawne");
        LogikaZle = PlayerPrefs.GetInt("LogikaZle");

        PamiecaPoprawne = PlayerPrefs.GetInt("PamiecaPoprawne");
        PamiecZle =  PlayerPrefs.GetInt("PamiecZle");

        KoncentracjaPoprawne = PlayerPrefs.GetInt("KoncentracjaPoprawne");
        KoncentracjaZle = PlayerPrefs.GetInt("KoncentracjaZle");

        SzybkoscPoprawne = PlayerPrefs.GetInt("SzybkoscPoprawne");
        SzybkoscZle = PlayerPrefs.GetInt("SzybkoscZle");
    }
    void SendData(int aktualnyPoziom, int aktulanaStrefa, int dobreOdpowiedzi, int ZleOdpowiedzi)
    {
        Debug.Log(aktualnyPoziom);
        Debug.Log(aktulanaStrefa);
        Debug.Log(dobreOdpowiedzi);
        Debug.Log(ZleOdpowiedzi);
    }
}
