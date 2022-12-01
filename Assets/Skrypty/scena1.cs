using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class scena1 : MonoBehaviour
{
    public Text odpowiedz;
    public string scene;
    public string wynik;
    public Button[] przyciski;
    public Image correntImage;
    public Image badImage;
    public Text correctText;
    public float duration;
    public float timeToNextScene;
    public bool dobraOdp = false;
    public bool zlaOdp = false;
    public int playerPoints;
    public int mistakes;
    public Slider pasek;
    public float czasTrwania;
    public float czasWartosc = 7;
    public bool stopTime;
    public bool noAnswer;
    public bool lastround;
    public SummaryMathf summaryMathf;

    private void Awake()
    {
        Time.timeScale = 1;
        playerPoints = PlayerPrefs.GetInt("punktyDzialanie");
        mistakes = PlayerPrefs.GetInt("punktyDzialanieBlad");
        noAnswer = false;
        pasek.value = 0;
        pasek.maxValue = czasWartosc;
        stopTime = false;
    }
    void Start()
    {
        if (summaryMathf == null)
            return;
        GetValues();
    }
    void GetValues()
    {
        odpowiedz = this.GetComponent<Text>();
        correctText.gameObject.SetActive(false);
        correntImage.fillAmount = 0;
        badImage.fillAmount = 0;
        dobraOdp = false;
        Time.timeScale = 1;
        zlaOdp = false;
        Debug.Log(PlayerPrefs.GetInt("punktyDzialanie", 0));
    }
 
    // Update is called once per frame
    void Update()
    {
        if(dobraOdp)
        {
            GoodAnswer();
        }
        if (zlaOdp)
        {
            BadAnswer();
        }
        if (noAnswer)
        {
            NoAnswer();
        }

        czasTrwania += Time.deltaTime * 1;
        pasek.value = czasTrwania;
        if (czasTrwania >= czasWartosc)
        {
            noAnswer = true;
        }
    }
    void GoodAnswer()
    {
        Time.timeScale = 1;
        correntImage.fillAmount += duration * Time.unscaledDeltaTime;
        if (correntImage.fillAmount >= 0.5f)
        {
            correctText.text = "BRAWO";
            correctText.gameObject.SetActive(true);
            if (correntImage.fillAmount >= 1)
            {
                timeToNextScene -= Time.unscaledDeltaTime * 1;
                if (timeToNextScene <= 0)
                {
                    if (lastround)
                        summaryMathf.ShowSummary();
                    else
                        SceneManager.LoadScene(scene);
                }
            }
        }
    }
    void BadAnswer()
    {
        badImage.fillAmount += duration * Time.deltaTime;
        if (badImage.fillAmount >= 0.5f)
        {
            correctText.text = "NIESTETY";
            correctText.gameObject.SetActive(true);
            if (badImage.fillAmount >= 1)
            {
                timeToNextScene -= Time.deltaTime * 1;
                if (timeToNextScene <= 0)
                {
                    if (lastround)
                        summaryMathf.ShowSummary();
                    else
                        SceneManager.LoadScene(scene);
                }
            }
        }
    }
    void NoAnswer()
    {
            badImage.fillAmount += duration * 0.5f * Time.deltaTime;
            if (badImage.fillAmount >= 0.5f)
            {
                correctText.text = "NIESTETY";
                correctText.gameObject.SetActive(true);
                if (badImage.fillAmount >= 1)
                {
                    timeToNextScene -= Time.deltaTime * 1;
                    if (timeToNextScene <= 0)
                    {
                        if (lastround)
                            summaryMathf.ShowSummary();
                        else
                            SceneManager.LoadScene(scene);
                    }
                }
            }
    }
    public void sprawdzanieOdp1()
    {
        if(odpowiedz.text == wynik)
        {
            Debug.Log("DOBRAODPOWEDZ");
            dobraOdp = true;
            playerPoints++;
            PlayerPrefs.SetInt("punktyDzialanie", playerPoints);
            PlayerPrefs.Save();


        }
        else
        {
            Debug.Log("ZLAODPOWEIDZ");
            zlaOdp = true;
            mistakes++;
            PlayerPrefs.SetInt("punktyDzialanieBlad", mistakes);
            PlayerPrefs.Save();
            
        }
    }


  
}
