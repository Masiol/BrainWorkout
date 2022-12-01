using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Test : MonoBehaviour
{
    private GameObject Answers;
    public List<String> wyniki = new List<string>();
    public string dzialanie;
    public string dobryWynik;
    private TextMeshProUGUI mathfOperationScreen;
    public List<Button> buttonAnswers = new List<Button>();
    void Start()
    {
        mathfOperationScreen = GameObject.Find("Dzialanie").GetComponent<TextMeshProUGUI>();
        mathfOperationScreen.text = dzialanie.ToString();
        Answers = GameObject.Find("Answers");
        foreach (Button child in Answers.transform.GetComponentsInChildren<Button>())
        {
            buttonAnswers.Add(child);
            
        }
        foreach (Button child in Answers.transform.GetComponentsInChildren<Button>())
        { 
        buttonAnswers[0].GetComponentInChildren<TextMeshProUGUI>().text = wyniki[0];
        buttonAnswers[1].GetComponentInChildren<TextMeshProUGUI>().text = wyniki[1];
        buttonAnswers[2].GetComponentInChildren<TextMeshProUGUI>().text = wyniki[2];
        buttonAnswers[3].GetComponentInChildren<TextMeshProUGUI>().text = wyniki[3];
        child.transform.SetSiblingIndex(UnityEngine.Random.Range(0, buttonAnswers.Count));

            if(child.GetComponentInChildren<TextMeshProUGUI>().text == dobryWynik)
            {
                child.transform.name = "Good";
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(Button button)
    {
        if(button.name == "Good")
        {
            Debug.Log("gituwa");
        }
        if(button.name != "Good")
        {
            Debug.Log("dupa");
        }
    }
}
