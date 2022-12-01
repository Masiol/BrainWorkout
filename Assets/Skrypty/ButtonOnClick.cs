using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

//Biblioteki w tym zarz�dzanie scenami, User Interface oraz komponentem Text Mesh Pro

public class ButtonOnClick : MonoBehaviour
{
    //Zmienne

    //Kontener dla element�w UI
    public Canvas canvas;
    //Guzik"
    public Button button;
    // Odwo�anie do skrpytu "SpawnObject"
    public SpawnObject spawn;
    // Zliczanie punkt�w dobrych i z�ych
    public int pointsGood;
    public int pointsWrong;
    // Nazwa Sceny kt�ra zostanie za�adowana po obecnej
    [SerializeField] private string scene;

    //Booleany z dobr� i z�� odpowiedzi�.
    private bool dobraOdp;
    private bool zlaOdp;
    //Zdj�cia, w tym wypadku zielone oraz czerwone t�o
    public Image correctImage;
    public Image badImage;
    // Czas po jakim nast�puje zakrycie sceny powy�szymi zdj�ciami.
    private float duration = 1.75f;
    //Komponet Tekstu
    public Text correctText;
    //Czas do za�adowania kolejnej sceny
    public float timeToNextScene = 2f;
    //Odwo�anie do skryptu z podsumowaniem aktualnego poziomu
    SummaryTestThree summaryTestThree;

    //Sprawdza czy to ostatnia runda w danej grze.
    public bool lastround;

    //Funkcja wywo�uj�ca si� w pierwszej klatce po starcie gry - natychmiastowo.
    void Start()
    {
        // Wyszukuje Objektu SummaryManager po nazwie, je�li jest to ostatnia runda
       if(lastround)
        {
            summaryTestThree = GameObject.Find("SummaryManager").GetComponent<SummaryTestThree>();
        }
       //Ustawia pr�dko�� czasu na warto�� domy�ln�.
        Time.timeScale = 1;

        //Pobiera komponenty i odwo�ania do innych skrypt�w
        canvas = gameObject.GetComponent<Canvas>();
        spawn = FindObjectOfType<SpawnObject>();
        button = this.GetComponent<Button>();
        pointsGood = PlayerPrefs.GetInt("koncentracjaDobre");
        pointsWrong = PlayerPrefs.GetInt("koncentracjaZle");

        //Wyszukanie kompnet�w po dzieciach obiektu
        correctText = GameObject.Find("TextHolder").GetComponentInChildren<Text>();
        correctImage = GameObject.Find("Good").GetComponentInChildren<Image>();
        badImage = GameObject.Find("Wrong").GetComponentInChildren<Image>();


        //Przypisanie ka�demu guziki funkcji ClickOnButton - umo�liwiaj�c� wykonanie akcji po jego klikni�ciu.
        button.GetComponent<Button>().onClick.AddListener(() => PlayerCanClickButton(button));

        

    }

    // Update wykonuje si� raz na klatk�.
    void Update()
    {
        //Je�li bool dobraOdp = true
        if (dobraOdp)
        {
            //Zacznij wype�nia� obraz
            correctImage.fillAmount += duration * Time.unscaledDeltaTime;
            //Je�li obraz wypelniony w ponad po�owie
            if (correctImage.fillAmount >= 0.5f)
            {
                //Aktywuj komponent Text i zmie� jego tre�� na BRAWO
                correctText.text = "BRAWO";
                correctText.gameObject.SetActive(true);
                // Jesli zdj�cie wype�nione w ca�o�ci
                if (correctImage.fillAmount >= 1)
                {
                    //Odpal zegar odliczaj�cy do \/
                    timeToNextScene -= Time.unscaledDeltaTime * 1;
                    if (timeToNextScene <= 0)
                    {
                        //G��wnego podsumowania je�li to ostatnia runda
                        if(lastround)
                        {
                            summaryTestThree.ShowSummary();
                        }
                        //Lub do kolejnej sceny z gr�.
                        else
                        SceneManager.LoadScene(scene);
                    }
                }
            }
        }

        //Identyczna sytuacja jak z powy�szym przyk�adem.
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
    // Funkcja wywo�ana i przypisana ka�demu guzikowi na starcie.
    public void PlayerCanClickButton(Button button)
    {
        //Je�li skrypt "SpawnObject" ma bool ustawiony na canClick.
        if (spawn.canClick)
        {
            //Klikni�ty przeze mnie guzik na ustawiony tag "Target" -> Tag ustawia si� w skrypcie "SpawnObject" w metodzie "RandomObject()"
            if (button.tag == "Target")
            {
                //Podlicza punkty zapisywane w skrypcie jak i w pami�ci silnika jako PlayerPrefs.
                pointsGood++;
                PlayerPrefs.SetInt("koncentracjaDobre", pointsGood);
                PlayerPrefs.Save();
                //Ustawia bool na true, co pozwoli wywo�a� wype�nianie si� zdj�cia z metody "Update()"
                dobraOdp = true;

                //Bli�niaczo pododbna sytuacja.
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
