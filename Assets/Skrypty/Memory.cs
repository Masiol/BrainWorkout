using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Memory : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;
    [Tooltip("Odstêpy miêdzy kwadracikami - uzale¿nione od iloœci")]
    public Vector2[] spacing;
    [Tooltip("Tutaj masz wielkoœæ kwadracików - uzale¿nione jest to od ich iloœci")]
    public Vector2[] cellSizes;
    [Tooltip("Tu nie ruszaj")]
    public int[] columntCount;
    [Tooltip("Tutaj jak nie ma, to podpinasz podstawowy guzik jaki ma sie kopiowaæ")]
    public Button button;
    [Tooltip("Tu sie samo dodaje")]
    public List<Button> MemoryButtons = new List<Button>();
    [Tooltip("Przesun sobie paskiem ile chcesz guziczków")]
    [Range(1,9)]
    public int MemoryButtonCount;
    private GameObject fruitCon;
    public GameObject checkFruitButton;
    public List<string> fruits = new List<string>();
    public List<Sprite> picturesFruit = new List<Sprite>();
    private int i;
    [Space]
    public float Timer;
    public float CzasNaOdpowiedz;
    private float czasNaOdpowiedzTimer;
    private int TimerInt;
    public TextMeshProUGUI timeText;
    [Space]
    public int numberOfAttempts;
    private int currentAttempt = 1;
    public TextMeshProUGUI CurrentAttempsText;
    private bool goodClick = false;
    private bool endRound;
    private List<string> trueFruitList = new List<string>();
    private List<Sprite> trueFruitImages = new List<Sprite>();
    private bool firstBool = true;
    private bool answerTime;
    [Space]
    private Slider slider;
    public Summary summary;
    [HideInInspector] public int GoodAnswers;
    [HideInInspector] public int WrongAnswers;
    [HideInInspector] public int totalGoodAnswers;
    [HideInInspector] public int totalWrongAnswers;

    void Start()
    {
        Time.timeScale = 1;
        GetValues();
        
        #region ROZMIARWIELKOSCKAFELKOW
        if (MemoryButtonCount < 3)
        {
            gridLayoutGroup.constraintCount = columntCount[0];
            gridLayoutGroup.cellSize = cellSizes[0];
            gridLayoutGroup.spacing = spacing[0];
        }
        else if (MemoryButtonCount == 3)
        {
            gridLayoutGroup.constraintCount = columntCount[0];
            gridLayoutGroup.cellSize = cellSizes[0];
            gridLayoutGroup.spacing = spacing[0];
        }
        else if (MemoryButtonCount == 4)
        {
            gridLayoutGroup.constraintCount = columntCount[1];
            gridLayoutGroup.cellSize = cellSizes[1];
            gridLayoutGroup.spacing = spacing[1];
        }
        else if (MemoryButtonCount == 6)
        {
            gridLayoutGroup.constraintCount = columntCount[2];
            gridLayoutGroup.cellSize = cellSizes[2];
            gridLayoutGroup.spacing = spacing[2];
        }
        else if (MemoryButtonCount == 9)
        {
            gridLayoutGroup.constraintCount = columntCount[3];
            gridLayoutGroup.cellSize = cellSizes[3];
            gridLayoutGroup.spacing = spacing[3];
        }
        #endregion

        SpawnButtons();
         
        ShuffleFruits();

        for (int i = 0; i < MemoryButtons.Count; i++)
        {
            trueFruitList.Add(fruits[i]);
            trueFruitImages.Add(picturesFruit[i]);
            MemoryButtons[i].GetComponent<Image>().sprite = picturesFruit[i];
            MemoryButtons[i].name = fruits[i];
            MemoryButtons[i].interactable = false;
            
        }
    }
    void GetValues()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        fruitCon = GameObject.Find("FruitContainer");
        checkFruitButton = GameObject.Find("CheckFruitButton"); 
        totalWrongAnswers = PlayerPrefs.GetInt("WszystkieZleOdpowiedzi");
        totalGoodAnswers = PlayerPrefs.GetInt("WszystkiePoprawneOdpowiedzi");
      
        checkFruitButton.gameObject.SetActive(false);
        CurrentAttempsText.gameObject.SetActive(false);

        slider = GameObject.Find("Slider").GetComponent<Slider>();
        slider.value = Timer;
        slider.maxValue = Timer;

        TimerInt = Mathf.RoundToInt(Timer);
        timeText.text = TimerInt.ToString();
        answerTime = false;
        czasNaOdpowiedzTimer = CzasNaOdpowiedz;
    }
    void SpawnButtons()
    {
        for (int i = 0; i < MemoryButtonCount; i++)
        {
            Button go = Instantiate(button, transform.position, transform.rotation);
            MemoryButtons.Add(go);
            go.transform.parent = transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentAttempt <= numberOfAttempts)
        {
            AttemptTimer();
        }
        if (currentAttempt > numberOfAttempts)
        {
            EndRound();
        }
        if(!answerTime)
        slider.value = Timer;

        if(currentAttempt > numberOfAttempts && !summary.showSummary)
        {
            summary.ShowSummary();
            answerTime = false;
        }
        if(answerTime)
        {
            AnswerTime();
        }
    }
    void AttemptTimer()
    {
        Timer -= Time.deltaTime;
        TimerInt = Mathf.RoundToInt(Timer);
        timeText.text = TimerInt.ToString();
        if (Timer <= 0)
        {
            Timer = 0;
            answerTime = true;
            CurrentAttempsText.gameObject.SetActive(true);
            for (int i = 0; i < MemoryButtonCount; i++)
            {
                if (!goodClick && firstBool)
                {
                    MemoryButtons[i].GetComponent<Image>().sprite = null;
                    MemoryButtons[i].interactable = true;
                }
            }
            if (firstBool)
            {
                RandomFruit();
                checkFruitButton.gameObject.SetActive(true);
                firstBool = false;
            }
        }
        CurrentAttempsText.text = currentAttempt + "/" + numberOfAttempts;
    }
    void EndRound()
    {
        endRound = true;
        if (endRound)
        {
            for (int i = 0; i < MemoryButtonCount; i++)
            {
                MemoryButtons[i].interactable = false;
            }
        }
    }
    void AnswerTime()
    {
        czasNaOdpowiedzTimer -= Time.deltaTime;
        slider.value = czasNaOdpowiedzTimer;
        slider.maxValue = CzasNaOdpowiedz;
        if (czasNaOdpowiedzTimer <= 0)
        {
            currentAttempt++;
            CountWrongAnswer();
            czasNaOdpowiedzTimer = CzasNaOdpowiedz;
        }
    }
    private void ShuffleFruits()
    {
        foreach (Button child in fruitCon.transform.GetComponentsInChildren<Button>())
        {
            child.transform.SetSiblingIndex(UnityEngine.Random.Range(0, MemoryButtons.Count));
        }
    }     

    public void OnClick(Button button)
    {

        if (button.name == checkFruitButton.name)
        {
            czasNaOdpowiedzTimer = CzasNaOdpowiedz;
            CountGoodAnswer();
            goodClick = true;
            if (checkFruitButton.name == "Jablko")
            {
                button.GetComponent<Image>().sprite = picturesFruit[0];
                trueFruitList.Remove("Jablko");
                trueFruitImages.RemoveAll(x => x.name == "jabluszko");

            }
            if (checkFruitButton.name == "Gruszka")
            {
                button.GetComponent<Image>().sprite = picturesFruit[1];
                trueFruitList.Remove("Gruszka");
                trueFruitImages.RemoveAll(x => x.name == "gruszka");
            }
            if (checkFruitButton.name == "Banan")
            {
                button.GetComponent<Image>().sprite = picturesFruit[2];
                trueFruitList.Remove("Banan");
                trueFruitImages.RemoveAll(x => x.name == "bananik");

            }
            if (checkFruitButton.name == "Wisnia")
            {
                button.GetComponent<Image>().sprite = picturesFruit[3];
                trueFruitList.Remove("Wisnia");
                trueFruitImages.RemoveAll(x => x.name == "wisienka");
            }
            if (checkFruitButton.name == "Winogrono")
            {
                button.GetComponent<Image>().sprite = picturesFruit[4];
                trueFruitList.Remove("Winogrono");
                trueFruitImages.RemoveAll(x => x.name == "winogronko");
            }
            if (checkFruitButton.name == "Ananas")
            {
                button.GetComponent<Image>().sprite = picturesFruit[5];
                trueFruitList.Remove("Ananas");
                trueFruitImages.RemoveAll(x => x.name == "ananasik");
            }
            if (checkFruitButton.name == "Marchewka")
            {
                button.GetComponent<Image>().sprite = picturesFruit[6];
                trueFruitList.Remove("Marchewka");
                trueFruitImages.RemoveAll(x => x.name == "marcheweczka");
            }
            if (checkFruitButton.name == "Truskawka")
            {
                button.GetComponent<Image>().sprite = picturesFruit[7];
                trueFruitList.Remove("Truskawka");
                trueFruitImages.RemoveAll(x => x.name == "truskawka");
            }
            if (checkFruitButton.name == "Brokul")
            {
                button.GetComponent<Image>().sprite = picturesFruit[8];
                trueFruitList.Remove("Brokul");
                trueFruitImages.RemoveAll(x => x.name == "brokulek");
            }

        }
        if (button.name != checkFruitButton.name)
        {
            CountWrongAnswer();
        }
        if(goodClick)
        {     
            RandomFruit();
            goodClick = false;
        }
        currentAttempt++;
        if(currentAttempt == 1)
        Timer = 5f;
        
    }
    private void RefreshFruit()
    {
        for (int i = 0; i < MemoryButtonCount; i++)
        {
            MemoryButtons[i].GetComponent<Image>().sprite = picturesFruit[i];
            
        }
    }
    private void RandomFruit()
    {
        if (currentAttempt <= numberOfAttempts - 1)
        {
            int randomFruit = Random.Range(0, trueFruitList.Count);
            checkFruitButton.GetComponentInChildren<TextMeshProUGUI>().text = trueFruitList[randomFruit].ToString();
            checkFruitButton.name = trueFruitList[randomFruit];
            checkFruitButton.GetComponent<Image>().sprite = trueFruitImages[randomFruit];
        }
    }

    private void CountWrongAnswer()
    {
        totalWrongAnswers++;
        PlayerPrefs.SetInt("WszystkieZleOdpowiedzi", totalWrongAnswers);
        WrongAnswers++;
        PlayerPrefs.SetInt("memoryZleOdpowiedzi", WrongAnswers);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("memoryZleOdpowiedzi"));
    }
    private void CountGoodAnswer()
    { 
        totalGoodAnswers++;
        PlayerPrefs.SetInt("WszystkiePoprawneOdpowiedzi", totalGoodAnswers);
        GoodAnswers++;
        PlayerPrefs.SetInt("memoryDobreOdpowiedzi", GoodAnswers);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetInt("memoryDobrOdpowiedzi"));
    }
}
