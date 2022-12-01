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
    //Area = 2 - Pami��
    //Area = 3 - Koncentracja
    //Area = 4 - Szybko��

    private void Start()
    {
        PatientID = PlayerPrefs.GetString("NumerPacjenta");
        Level = PlayerPrefs.GetInt("CurrentLevel");
        Area = PlayerPrefs.GetInt("CurrentArea");
    }

   

    public void SetLevelNumber(int i)
    {
        Level = i;
    }
    public void SetAreaNumber(int i)
    {
        Area = i;
    }

    public void SetData()
    {
        PlayerPrefs.SetInt("CurrentLevel", Level);
        PlayerPrefs.SetInt("CurrentArea", Area);
    }
}
