using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeedGame : MonoBehaviour
{
    private GameObject rotateAround;
    public GameObject rotator;
    [Range(1,10)]
    [SerializeField] private float speed;
    private Button startButton;
    private bool startRotate;
    private Transform target;
    public bool canTouch = false;
    private Button stopButton;
    private float distanceMax = 60;
    public int szybkoscPunktyDobre;
    public int szybkoscPunktyZle;
    public bool lastround;
    public int currentScene;
    public SpeedSummary Speed;
    private bool turnOffClicks;

    public Image correntImage;
    public Image badImage;
    public float duration;
    public Text correctText;
    public float timeToNextScene;
    public string nextScene;

    public bool good;
    public bool wrong;
    private bool firstClick;
    void Start()
    {
        GetValues();
        if (Speed == null)
            return;
    }

    void GetValues()
    {
        szybkoscPunktyDobre = PlayerPrefs.GetInt("szybkoscDobre");
        szybkoscPunktyZle = PlayerPrefs.GetInt("szybkoscZle");
        rotateAround = GameObject.FindGameObjectWithTag("RotateAround");
        startButton = GameObject.FindGameObjectWithTag("StartButton").GetComponent<Button>();
        stopButton = GameObject.FindGameObjectWithTag("StopButton").GetComponent<Button>();
        startButton.onClick.AddListener(ButtonStart);
        stopButton.onClick.AddListener(StopButton);
        target = GameObject.Find("Target").transform;
        rotator.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(startRotate)
        {
            StartRotate();
        }
        if (good)
            GoodAnswer();
        if (wrong)
            BadAnswer();
        
    }
    void StartRotate()
    {
        float currentDistance = Vector2.Distance(target.transform.position, rotator.transform.position);

        rotator.transform.RotateAround(rotateAround.transform.position, Vector3.forward, speed * 15 * Time.deltaTime);

        if (currentDistance < distanceMax)
        {
            canTouch = true;
        }
        else
            canTouch = false;
    }
    private void ButtonStart()
    {
        startRotate = true;
        rotator.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);
        stopButton.gameObject.SetActive(true);
    }
    private void StopButton()
    {
        if (canTouch && !turnOffClicks)
        {
            if (!firstClick)
            {
                szybkoscPunktyDobre++;
                PlayerPrefs.SetInt("szybkoscDobre", szybkoscPunktyDobre);
                PlayerPrefs.Save();
                firstClick = true;
            }
            if (!lastround)
                good = true;
            if(lastround)
            {
                Speed.ShowSummary();
                turnOffClicks = true;
                
            }
        }
        if (!canTouch && !turnOffClicks)
        {
            if(!firstClick)
            { 
            szybkoscPunktyZle++;
            PlayerPrefs.SetInt("szybkoscZle", szybkoscPunktyZle);
            PlayerPrefs.Save();
                firstClick = true;
                }
            if (!lastround)
                wrong = true;
            if(lastround)
            {
                Speed.ShowSummary();
                turnOffClicks = true;
            }
        }
    }
    void GoodAnswer()
    {
        Time.timeScale = 1;
        correntImage.fillAmount += duration * Time.unscaledDeltaTime;
        correntImage.raycastTarget = true;
        if (correntImage.fillAmount >= 0.5f)
        {
            correctText.text = "BRAWO";
            correctText.gameObject.SetActive(true);
            if (correntImage.fillAmount >= 1)
            {
                timeToNextScene -= Time.unscaledDeltaTime * 1;
                if (timeToNextScene <= 0)
                {
                  SceneManager.LoadScene(nextScene);
                }
            }
        }
    }
    void BadAnswer()
    {
        badImage.fillAmount += duration * Time.deltaTime;
        correntImage.raycastTarget = true;
        if (badImage.fillAmount >= 0.5f)
        {
            correctText.text = "NIESTETY";
            correctText.gameObject.SetActive(true);
            if (badImage.fillAmount >= 1)
            {
                timeToNextScene -= Time.deltaTime * 1;
                if (timeToNextScene <= 0)
                {
                   SceneManager.LoadScene(nextScene);
                }
            }
        }
    }
}
