using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Biblioteki 

public class SpawnObject : MonoBehaviour
{
    //Intedzer daj¹cy mo¿liwosæ ustawienie iloœæi kulek(guzików) które zostan¹ stworzone na scenie.
    public int numberToSpawn;

    //Lista obiektów(guzików) które zostaj¹ pobrane do puli podczas spawnowania(towrzenia). W tym wypadku jest to tylko jeden obiekt.
    public List<GameObject> spawnPool;

    //prywatna lista stworzonych obiektów.
    private List<GameObject> SpawnedButton = new List<GameObject>();

    //background - jest to te¿ pole spawnu naszych obiektów.
    public Image backgroud;

    //konterner dla wszystkich elementów UI - User Interface.
    public Canvas canvas;

    //Obiekt który zostanie wybrany jako guzik, który musimy klikn¹æ, aby zaliczyæ poziom.
    public GameObject randomButton;

    //Czas zanim guzik zmieni kolor na taki jak reszta.
    [SerializeField] private float TimeToChangeColor;

    //prywatna wartoœæ przymuj¹ca waroœæ "Timer"
    private float timer2;

    //Domyœlny kolor ka¿dego elementu który porusza siê po planszy.
    public Color defaultColor;

    //Kolor który wyró¿nia obiekt, który musi œledziæ gracz.
    public Color targetColor;

    //Czas po którym wszystkie obiekty siê zatrzymuj¹.
    public int timeToStopMove;

    //Bool odwo³uj¹cy siê do skrypu "ButtonOnClick" - ten skrypt jest przypisany do ka¿dego stworzonego guzika.
    [HideInInspector] public bool canClick;

    //float zliczaj¹cy wartoœci "Timer oraz secondsToStop"
    private float allTimer;

    //Pasek postêpu na górze ekranu.
    public Slider slider;

    //Rodzic kulek/guzików.
    public GameObject buttonsParent;

    //Skrypt "granic" ekranu.
    public EdgeOfScreenCollision edgeOfScreenCollision;

    //Funkcja wywo³uj¹ca siê w pierwszej klatce po starcie gry - natychmiastowo.
    void Start()
    {
        //Wywo³anie funkcji spawnObject() i GetValues()
        spawnObjects();
        GetValues();
    }
    void GetValues()
    {
        //Ustawienie wszystkich timerów i czasów oraz przypisanie obiektów
        timer2 = TimeToChangeColor;
        allTimer = timer2 + timeToStopMove;
        slider.maxValue = allTimer;
        buttonsParent = GameObject.Find("ButtonHolderParent");
    }

    public void spawnObjects()
    {  
        //Metoda która niszczy na samym pocz¹tku wszystkie elementy
        destroyObjects();
        //Ustawia lokalne zminne int i GameObject oraz BoxCollider2D
        int randomItem = 0;
        GameObject toSpawn;
        //BoxCollider pobiera wymiary zdjêcia "background".
        BoxCollider2D c = backgroud.GetComponent<BoxCollider2D>();
        //I ustawia jego wielkoœæ na podstawie szerokoœci i wysokoœci "backgroundu"
        c.size = new Vector2(backgroud.rectTransform.sizeDelta.x, backgroud.rectTransform.sizeDelta.y);

        //Lokalne zmienne float oraz Vector2(x,y)
        float screenX, screenY;
        Vector2 pos;

        //pêtla for która wykona siê tyle razy, ile ustawiliœmy kulek do stworzenia.
        for (int i = 0; i < numberToSpawn; i++)
        {
            //wybranie losowej liczby(od 0 do liczby ustalonej przez nas) - w tym wypadku liczby wariantów guzików, u nas jest to jeden element.
            randomItem = Random.Range(0, spawnPool.Count);

            //Przypisanie do GameObjectu toSpawn losowanego elementu - z listy spawnPool (Tutaj mamy tylko jeden wariant guzika)
            toSpawn = spawnPool[randomItem];
            //Wybiera losowe miejsce na ekranie w osi X I Y - wziête z wielkoœci komponentu Image "background"
            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            //Ustawia Vector2 na podstawie wylosowanego miejsca.
            pos = new Vector2(screenX, screenY);

        //Tworzy GameOject o nazwie "go" (co,   gdzie,      jaka rotacja)
            GameObject go = Instantiate(toSpawn, pos, toSpawn.transform.rotation);
            //Ustawia rodzica stworzonego obiektu jako canvas.
            go.transform.parent = canvas.transform;
            //ustawia lokaln¹ skalê oraz pozycjê.
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            go.GetComponent<RectTransform>().localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0);
            //Dodaje stworzony guzik do Listy.
            SpawnedButton.Add(go);
        }
        //Po zespawnowaniu wszytkich odpala funkcje Randomobject()
        RandomObject();

        
    }
    //Wybiera losowy guzik sposód stworzonej bazy, wywo³uje siê tylko raz.
    void RandomObject()
    {
        //Losowa liczba (Od 0 do liczby iloœci wytworzonych kulek)
        int index = Random.Range(0, SpawnedButton.Count);

        //Losowy guzik ma zmieniony kolor
        SpawnedButton[index].GetComponent<Image>().color = targetColor;
        //Losowy guik ma ustawiony tag.
        SpawnedButton[index].tag = "Target";
        //GameObject randomButton dostaje informacjê o wybranym obiekcie i ten obiekt zostaje do niego przypisany.
        randomButton = SpawnedButton[index];

        //Ustawienie odpowiednio rodziców elementów w celu poprawnego wyœwielania siê w widoku gry.
        GameObject buttonHolder = new GameObject();

        for (int i = 0; i < SpawnedButton.Count; i++)
        {
            SpawnedButton[i].transform.parent = buttonHolder.transform;
        }
        buttonHolder.transform.parent = buttonsParent.transform;
    }
    // Update wykonuje siê raz na klatkê.
    private void Update()
    {
        //Odpalenie zegarów oraz przypisanie slidera do wartoœci allTimer (animowany pasek na górze ekranu)
        allTimer -= Time.deltaTime;
        slider.value = allTimer;
        TimeToChangeColor -= Time.deltaTime;
        //Jeœli czas zejdzie do zera
        if (TimeToChangeColor < 0)
        {
            TimeToChangeColor = 0;
            //Pêtla z wybraniem wszystkich guzików. Zmiana ich koloru na domyœlny, oraz odpaleniem funkcji czasowych (Coroutine)
            for (int i = 0; i < SpawnedButton.Count; i++)
            {
                SpawnedButton[i].GetComponent<Image>().color = defaultColor;
                //Zaprestanie poruszania siê
                StartCoroutine(StopMoving());
                //Zaprzestanie przenikania
                StartCoroutine(StopPassingThrough());
            }
        }
       
    }
    //Niszczenie wszystkich obiektów z tagiem Spawnable
    private void destroyObjects()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Spawnable"))
        {
            Destroy(o);
        }
    }
    //Funkcja czasowa z opóŸnieniem w tym wypadku 1 sekundy oraz wy³¹czeniem przenikania siê
    IEnumerator StopPassingThrough()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < SpawnedButton.Count; i++)
        {
            //Wy³¹czenie przenikania
            SpawnedButton[i].GetComponent<RandomMovement>().ignore = false;
        }
    }
    //Funkcja czasowa z opóŸnieniem w tym wypadku z wartoœci¹ "timeToStopMove"
    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(timeToStopMove);
        for (int i = 0; i < SpawnedButton.Count; i++)
        {
            //Wy³¹cza fizykê - zatrzymuje wszystkie kulki/guziki
            SpawnedButton[i].GetComponent<Rigidbody2D>().simulated = false;
            //Ustawia mo¿liwoœæ klikniêcia na guzik
            TaskOnClick();
        }
    }
    //bool canClick odczytywany jest w skrypcie ButtonOnClick w funkcji PlayerCanClickButton i jest to moment, w którym gracz mo¿e fizycznie klikn¹æ na dan¹ kulkê.
    public void TaskOnClick()
    {
        canClick = true;
    }
}

