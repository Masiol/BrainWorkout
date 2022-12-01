using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Biblioteki 

public class SpawnObject : MonoBehaviour
{
    //Intedzer daj�cy mo�liwos� ustawienie ilo��i kulek(guzik�w) kt�re zostan� stworzone na scenie.
    public int numberToSpawn;

    //Lista obiekt�w(guzik�w) kt�re zostaj� pobrane do puli podczas spawnowania(towrzenia). W tym wypadku jest to tylko jeden obiekt.
    public List<GameObject> spawnPool;

    //prywatna lista stworzonych obiekt�w.
    private List<GameObject> SpawnedButton = new List<GameObject>();

    //background - jest to te� pole spawnu naszych obiekt�w.
    public Image backgroud;

    //konterner dla wszystkich element�w UI - User Interface.
    public Canvas canvas;

    //Obiekt kt�ry zostanie wybrany jako guzik, kt�ry musimy klikn��, aby zaliczy� poziom.
    public GameObject randomButton;

    //Czas zanim guzik zmieni kolor na taki jak reszta.
    [SerializeField] private float TimeToChangeColor;

    //prywatna warto�� przymuj�ca waro�� "Timer"
    private float timer2;

    //Domy�lny kolor ka�dego elementu kt�ry porusza si� po planszy.
    public Color defaultColor;

    //Kolor kt�ry wyr�nia obiekt, kt�ry musi �ledzi� gracz.
    public Color targetColor;

    //Czas po kt�rym wszystkie obiekty si� zatrzymuj�.
    public int timeToStopMove;

    //Bool odwo�uj�cy si� do skrypu "ButtonOnClick" - ten skrypt jest przypisany do ka�dego stworzonego guzika.
    [HideInInspector] public bool canClick;

    //float zliczaj�cy warto�ci "Timer oraz secondsToStop"
    private float allTimer;

    //Pasek post�pu na g�rze ekranu.
    public Slider slider;

    //Rodzic kulek/guzik�w.
    public GameObject buttonsParent;

    //Skrypt "granic" ekranu.
    public EdgeOfScreenCollision edgeOfScreenCollision;

    //Funkcja wywo�uj�ca si� w pierwszej klatce po starcie gry - natychmiastowo.
    void Start()
    {
        //Wywo�anie funkcji spawnObject() i GetValues()
        spawnObjects();
        GetValues();
    }
    void GetValues()
    {
        //Ustawienie wszystkich timer�w i czas�w oraz przypisanie obiekt�w
        timer2 = TimeToChangeColor;
        allTimer = timer2 + timeToStopMove;
        slider.maxValue = allTimer;
        buttonsParent = GameObject.Find("ButtonHolderParent");
    }

    public void spawnObjects()
    {  
        //Metoda kt�ra niszczy na samym pocz�tku wszystkie elementy
        destroyObjects();
        //Ustawia lokalne zminne int i GameObject oraz BoxCollider2D
        int randomItem = 0;
        GameObject toSpawn;
        //BoxCollider pobiera wymiary zdj�cia "background".
        BoxCollider2D c = backgroud.GetComponent<BoxCollider2D>();
        //I ustawia jego wielko�� na podstawie szeroko�ci i wysoko�ci "backgroundu"
        c.size = new Vector2(backgroud.rectTransform.sizeDelta.x, backgroud.rectTransform.sizeDelta.y);

        //Lokalne zmienne float oraz Vector2(x,y)
        float screenX, screenY;
        Vector2 pos;

        //p�tla for kt�ra wykona si� tyle razy, ile ustawili�my kulek do stworzenia.
        for (int i = 0; i < numberToSpawn; i++)
        {
            //wybranie losowej liczby(od 0 do liczby ustalonej przez nas) - w tym wypadku liczby wariant�w guzik�w, u nas jest to jeden element.
            randomItem = Random.Range(0, spawnPool.Count);

            //Przypisanie do GameObjectu toSpawn losowanego elementu - z listy spawnPool (Tutaj mamy tylko jeden wariant guzika)
            toSpawn = spawnPool[randomItem];
            //Wybiera losowe miejsce na ekranie w osi X I Y - wzi�te z wielko�ci komponentu Image "background"
            screenX = Random.Range(c.bounds.min.x, c.bounds.max.x);
            screenY = Random.Range(c.bounds.min.y, c.bounds.max.y);
            //Ustawia Vector2 na podstawie wylosowanego miejsca.
            pos = new Vector2(screenX, screenY);

        //Tworzy GameOject o nazwie "go" (co,   gdzie,      jaka rotacja)
            GameObject go = Instantiate(toSpawn, pos, toSpawn.transform.rotation);
            //Ustawia rodzica stworzonego obiektu jako canvas.
            go.transform.parent = canvas.transform;
            //ustawia lokaln� skal� oraz pozycj�.
            go.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            go.GetComponent<RectTransform>().localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y, 0);
            //Dodaje stworzony guzik do Listy.
            SpawnedButton.Add(go);
        }
        //Po zespawnowaniu wszytkich odpala funkcje Randomobject()
        RandomObject();

        
    }
    //Wybiera losowy guzik spos�d stworzonej bazy, wywo�uje si� tylko raz.
    void RandomObject()
    {
        //Losowa liczba (Od 0 do liczby ilo�ci wytworzonych kulek)
        int index = Random.Range(0, SpawnedButton.Count);

        //Losowy guzik ma zmieniony kolor
        SpawnedButton[index].GetComponent<Image>().color = targetColor;
        //Losowy guik ma ustawiony tag.
        SpawnedButton[index].tag = "Target";
        //GameObject randomButton dostaje informacj� o wybranym obiekcie i ten obiekt zostaje do niego przypisany.
        randomButton = SpawnedButton[index];

        //Ustawienie odpowiednio rodzic�w element�w w celu poprawnego wy�wielania si� w widoku gry.
        GameObject buttonHolder = new GameObject();

        for (int i = 0; i < SpawnedButton.Count; i++)
        {
            SpawnedButton[i].transform.parent = buttonHolder.transform;
        }
        buttonHolder.transform.parent = buttonsParent.transform;
    }
    // Update wykonuje si� raz na klatk�.
    private void Update()
    {
        //Odpalenie zegar�w oraz przypisanie slidera do warto�ci allTimer (animowany pasek na g�rze ekranu)
        allTimer -= Time.deltaTime;
        slider.value = allTimer;
        TimeToChangeColor -= Time.deltaTime;
        //Je�li czas zejdzie do zera
        if (TimeToChangeColor < 0)
        {
            TimeToChangeColor = 0;
            //P�tla z wybraniem wszystkich guzik�w. Zmiana ich koloru na domy�lny, oraz odpaleniem funkcji czasowych (Coroutine)
            for (int i = 0; i < SpawnedButton.Count; i++)
            {
                SpawnedButton[i].GetComponent<Image>().color = defaultColor;
                //Zaprestanie poruszania si�
                StartCoroutine(StopMoving());
                //Zaprzestanie przenikania
                StartCoroutine(StopPassingThrough());
            }
        }
       
    }
    //Niszczenie wszystkich obiekt�w z tagiem Spawnable
    private void destroyObjects()
    {
        foreach (GameObject o in GameObject.FindGameObjectsWithTag("Spawnable"))
        {
            Destroy(o);
        }
    }
    //Funkcja czasowa z op�nieniem w tym wypadku 1 sekundy oraz wy��czeniem przenikania si�
    IEnumerator StopPassingThrough()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < SpawnedButton.Count; i++)
        {
            //Wy��czenie przenikania
            SpawnedButton[i].GetComponent<RandomMovement>().ignore = false;
        }
    }
    //Funkcja czasowa z op�nieniem w tym wypadku z warto�ci� "timeToStopMove"
    IEnumerator StopMoving()
    {
        yield return new WaitForSeconds(timeToStopMove);
        for (int i = 0; i < SpawnedButton.Count; i++)
        {
            //Wy��cza fizyk� - zatrzymuje wszystkie kulki/guziki
            SpawnedButton[i].GetComponent<Rigidbody2D>().simulated = false;
            //Ustawia mo�liwo�� klikni�cia na guzik
            TaskOnClick();
        }
    }
    //bool canClick odczytywany jest w skrypcie ButtonOnClick w funkcji PlayerCanClickButton i jest to moment, w kt�rym gracz mo�e fizycznie klikn�� na dan� kulk�.
    public void TaskOnClick()
    {
        canClick = true;
    }
}

