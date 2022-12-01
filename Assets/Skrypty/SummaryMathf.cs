using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SummaryMathf : MonoBehaviour
{
    public Image image;
    public Image kolo;
    public Image puchar;
    public GameObject smallCircles;
    public bool showSummary = false;
    public TextMeshProUGUI wyniki;
    public TextMeshProUGUI poprawne;
    public TextMeshProUGUI bledne;
    public TextMeshProUGUI summaryText;
    public Button nextLevelButton;
    public TextMeshProUGUI CountWrongAnswersInt;
    public TextMeshProUGUI CountGoodAnswersInt;
    [Space]
    public int wrongAnswersInt;
    public int goodAnswersInt;
    public Memory memory;
    public Image wrongImage;
    public Image goodImage;

    public string scene;

    void Start()
    {
        poprawne.gameObject.SetActive(false);
        bledne.gameObject.SetActive(false);
        wyniki.gameObject.SetActive(false);
        smallCircles.SetActive(false);
        kolo.gameObject.SetActive(false);
        puchar.gameObject.SetActive(false);
        summaryText.gameObject.SetActive(false);
        CountGoodAnswersInt.gameObject.SetActive(false);
        CountWrongAnswersInt.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        if (memory == null)
        {
            return;
        }
    }

    // Update is called once per frame
    public void ShowSummary()
    {
        wrongAnswersInt = PlayerPrefs.GetInt("punktyDzialanieBlad");
        goodAnswersInt = PlayerPrefs.GetInt("punktyDzialanie");
        CountGoodAnswersInt.text = goodAnswersInt.ToString();
        CountWrongAnswersInt.text = wrongAnswersInt.ToString();
        LeanTween.alpha(image.GetComponent<RectTransform>(), 1f, 1f);
        showSummary = true;
        StartCoroutine(ShowInfo());
    }
    IEnumerator ShowInfo()
    {
        LeanTween.alpha(wrongImage.GetComponent<RectTransform>(), 0f, 1f);
        if (goodImage != null)
        {
            LeanTween.alpha(goodImage.GetComponent<RectTransform>(), 0f, 1f);
        }
        yield return new WaitForSeconds(2);
        kolo.gameObject.SetActive(true);
        puchar.gameObject.SetActive(true);
        summaryText.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        wyniki.gameObject.SetActive(true);
        poprawne.gameObject.SetActive(true);
        bledne.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        smallCircles.SetActive(true);
        CountWrongAnswersInt.gameObject.SetActive(true);
        CountGoodAnswersInt.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        nextLevelButton.gameObject.SetActive(true);
    }
    public void Click()
    {
        SceneManager.LoadScene(scene);
    }
}
