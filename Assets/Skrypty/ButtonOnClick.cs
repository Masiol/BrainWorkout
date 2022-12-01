using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//Biblioteki w tym zarz¹dzanie scenami, User Interface oraz komponentem Text Mesh Pro

public class ButtonOnClick : MonoBehaviour
{
    //Zmienne

    //Kontener dla elementów UI
    public Canvas canvas;
    //Guzik"
    public Button button;
    // Odwo³anie do skrpytu "SpawnObject"
    public SpawnObject spawn;
    // Zliczanie punktów dobrych i z³ych
    public int pointsGood;
    public int pointsWrong;
    // Nazwa Sceny która zostanie za³adowana po obecnej
    [SerializeField] private string scene;

    //Booleany z dobr¹ i z³¹ odpowiedzi¹.
    private bool dobraOdp;
    private bool zlaOdp;
    //Zdjêcia, w tym wypadku zielone oraz czerwone t³o
    public Image correctImage;
    public Image badImage;
    // Czas po jakim nastêpuje zakrycie sceny powy¿szymi zdjêciami.
    private float duration = 1.75f;
    //Komponet Tekstu
    public Text correctText;
    //Czas do za³adowania kolejnej sceny
    public float timeToNextScene = 2f;
    //Odwo³anie do skryptu z podsumowaniem aktualnego poziomu
    SummaryTestThree summaryTestThree;

    //Sprawdza czy to ostatnia runda w danej grze.
    public bool lastround;

    //Funkcja wywo³uj¹ca siê w pierwszej klatce po starcie gry - natychmiastowo.
    void Start()
    {
        // Wyszukuje Objektu SummaryManager po nazwie, jeœli jest to ostatnia runda
       if(lastround)
        {
            summaryTestThree = GameObject.Find("SummaryManager").GetComponent<SummaryTestThree>();
        }
       //Ustawia prêdkoœæ czasu na wartoœæ domyœln¹.
        Time.timeScale = 1;

        //Pobiera komponenty i odwo³ania do innych skryptów
        canvas = gameObject.GetComponent<Canvas>();
        spawn = FindObjectOfType<SpawnObject>();
        button = this.GetComponent<Button>();
        pointsGood = PlayerPrefs.GetInt("koncentracjaDobre");
        pointsWrong = PlayerPrefs.GetInt("koncentracjaZle");

        //Wyszukanie kompnetów po dzieciach obiektu
        correctText = GameObject.Find("TextHolder").GetComponentInChildren<Text>();
        correctImage = GameObject.Find("Good").GetComponentInChildren<Image>();
        badImage = GameObject.Find("Wrong").GetComponentInChildren<Image>();


        //Przypisanie ka¿demu guziki funkcji ClickOnButton - umo¿liwiaj¹c¹ wykonanie akcji po jego klikniêciu.
        button.GetComponent<Button>().onClick.AddListener(() => PlayerCanClickButton(button));

        

    }

    // Update wykonuje siê raz na klatkê.
    void Update()
    {
        //Jeœli bool dobraOdp = true
        if (dobraOdp)
        {
            //Zacznij wype³niaæ obraz
            correctImage.fillAmount += duration * Time.unscaledDeltaTime;
            //Jeœli obraz wypelniony w ponad po³owie
            if (correctImage.fillAmount >= 0.5f)
            {
                //Aktywuj komponent Text i zmieñ jego treœæ na BRAWO
                correctText.text = "BRAWO";
                correctText.gameObject.SetActive(true);
                // Jesli zdjêcie wype³nione w ca³oœci
                if (correctImage.fillAmount >= 1)
                {
                    //Odpal zegar odliczaj¹cy do \/
                    timeToNextScene -= Time.unscaledDeltaTime * 1;
                    if (timeToNextScene <= 0)
                    {
                        //G³ównego podsumowania jeœli to ostatnia runda
                        if(lastround)
                        {
                            summaryTestThree.ShowSummary();
                        }
                        //Lub do kolejnej sceny z gr¹.
                        else
                        SceneManager.LoadScene(scene);
                    }
                }
            }
        }

        //Identyczna sytuacja jak z powy¿szym przyk³adem.
        if (zlaOdp)
        {
            {
                badImage.fillAmount += duration * Time.unscaledDeltaTime;
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
                            {
                                summaryTestThree.ShowSummary();
                            }
                            else
                                SceneManager.LoadScene(scene);
                        }
                    }
                }
            }
        }
    }
    // Funkcja wywo³ana i przypisana ka¿demu guzikowi na starcie.
    public void PlayerCanClickButton(Button button)
    {
        //Jeœli skrypt "SpawnObject" ma bool ustawiony na canClick.
        if (spawn.canClick)
        {
            //Klikniêty przeze mnie guzik na ustawiony tag "Target" -> Tag ustawia siê w skrypcie "SpawnObject" w metodzie "RandomObject()"
            if (button.tag == "Target")
            {
                //Podlicza punkty zapisywane w skrypcie jak i w pamiêci silnika jako PlayerPrefs.
                pointsGood++;
                PlayerPrefs.SetInt("koncentracjaDobre", pointsGood);
                PlayerPrefs.Save();
                //Ustawia bool na true, co pozwoli wywo³aæ wype³nianie siê zdjêcia z metody "Update()"
                dobraOdp = true;

                //BliŸniaczo pododbna sytuacja.
            }
            else
            {
                pointsWrong++;
                PlayerPrefs.SetInt("koncentracjaZle", pointsWrong);
                PlayerPrefs.Save();
                zlaOdp = true;
            }
        }
    }

}
