using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rejestracja : MonoBehaviour
{
    public InputField nameR;
    public InputField lastnameR;
    public InputField password;
    public InputField birthdayYear;
    public InputField email;
    public Dropdown plec;
    public Button rejestracjaR;

    public AuthManager authManager;

    public string[] pacjent;
    public string[] zapisanypacjent;
    void Start()
    {
        rejestracjaR.onClick.AddListener(Register);
        if(zapisanypacjent != null)
        {
            zapisanypacjent[0] = PlayerPrefs.GetString("ImiePacjenta");
            zapisanypacjent[1] = PlayerPrefs.GetString("NazwiskoPacjenta");
            zapisanypacjent[2] = PlayerPrefs.GetString("HasloPacjenta");
            zapisanypacjent[3] = PlayerPrefs.GetString("RokUrodzeniaPacjenta");
            zapisanypacjent[4] = PlayerPrefs.GetString("PlecPacjenta");
            zapisanypacjent[5] = PlayerPrefs.GetString("emailPacjenta");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Register()
    {
        if(nameR.text != "" && lastnameR.text != "" && password.text != "" && password.text.Length >= 6 && birthdayYear.text != "" && email.text != "" )
        {
            SaveData();
        }
        if(nameR.text == "")
        {
            Debug.Log("brak imienia");
        }
        if (lastnameR.text == "")
        {
            Debug.Log("brak nazwiska");
        }
        if (password.text == "")
        {
            Debug.Log("brak hasla");
        }
        if (birthdayYear.text == "")
        {
            Debug.Log("brak roku");
        }
        if(password.text.Length < 6)
        {
            Debug.Log("za krotkie haslo");
        }
        if (email.text == "")
        {
            Debug.Log("nie podano mejla");
        }

    }
    public void SaveData()
    {
        pacjent[0] = nameR.text;
        pacjent[1] = lastnameR.text; 
        pacjent[2] = password.text;
        pacjent[3] = birthdayYear.text;
        pacjent[4] = plec.options[plec.value].text;
        pacjent[5] = email.text;
        SaveToPrefs();
    }
    public void SaveToPrefs()
    {
        PlayerPrefs.SetString("ImiePacjenta", pacjent[0]);
        PlayerPrefs.SetString("NazwiskoPacjenta", pacjent[1]);
        PlayerPrefs.SetString("HasloPacjenta", pacjent[2]);
        PlayerPrefs.SetString("RokUrodzeniaPacjenta", pacjent[3]);
        PlayerPrefs.SetString("PlecPacjenta", pacjent[4]);
        PlayerPrefs.SetString("emailPacjenta", pacjent[5]);
    }
}
