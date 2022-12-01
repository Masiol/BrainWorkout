using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class RequirePoints
{
    public int requiredPoints;
    public int prize;
    public Button gifts;
    public Image CuteAnimal;
   // public bool isPossible;
}
public class Nagrody : MonoBehaviour
{
    public int punkty;
    public Color NotAllowedColor;
    public Color startColor = new Color(255, 255, 255);
    public Wyniki wyniki;
    public RequirePoints[] requirePoints;
    private int sumaPunktow;

    private void Start()
    {
        punkty = PlayerPrefs.GetInt("sumaWszystkich") + PlayerPrefs.GetInt("sumaNagrod");
        //Jak chcesz zresetowac prezenty, to musisz wpisac wszystkie tak jak linijka wyzej, od 0 do 5 a i zabrac ukoœniki.

      

        //PlayerPrefs.DeleteKey("gifts0"); - o to to




        Remember();
        SetGifts();
    }
    public void Remember()
    {
        if(PlayerPrefs.GetInt("gifts0") == 1)
        {
            requirePoints[0].gifts.gameObject.SetActive(false);
            requirePoints[0].CuteAnimal.gameObject.SetActive(true);
        }
        else
        {
            requirePoints[0].gifts.gameObject.SetActive(true);
            requirePoints[0].CuteAnimal.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("gifts1") == 1)
        {
            requirePoints[1].gifts.gameObject.SetActive(false);
            requirePoints[1].CuteAnimal.gameObject.SetActive(true);
        }
        else
        {
            requirePoints[1].gifts.gameObject.SetActive(true);
            requirePoints[1].CuteAnimal.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("gifts2") == 1)
        {
            requirePoints[2].gifts.gameObject.SetActive(false);
            requirePoints[2].CuteAnimal.gameObject.SetActive(true);
        }
        else
        {
            requirePoints[2].gifts.gameObject.SetActive(true);
            requirePoints[2].CuteAnimal.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("gifts3") == 1)
        {
            requirePoints[3].gifts.gameObject.SetActive(false);
            requirePoints[3].CuteAnimal.gameObject.SetActive(true);
        }
        else
        {
            requirePoints[3].gifts.gameObject.SetActive(true);
            requirePoints[3].CuteAnimal.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("gifts4") == 1)
        {
            requirePoints[4].gifts.gameObject.SetActive(false);
            requirePoints[4].CuteAnimal.gameObject.SetActive(true);
        }
        else
        {
            requirePoints[4].gifts.gameObject.SetActive(true);
            requirePoints[4].CuteAnimal.gameObject.SetActive(false);
        }
        if (PlayerPrefs.GetInt("gifts5") == 1)
        {
            requirePoints[5].gifts.gameObject.SetActive(false);
            requirePoints[5].CuteAnimal.gameObject.SetActive(true);
        }
        else
        {
            requirePoints[5].gifts.gameObject.SetActive(true);
            requirePoints[5].CuteAnimal.gameObject.SetActive(false);
        }
    }

    private void SetGifts()
    {

        for (int i = 0; i < requirePoints.Length; i++)
        {  
            if (requirePoints[i].requiredPoints > punkty)
            {
                requirePoints[i].gifts.GetComponent<Image>().color = NotAllowedColor;
                requirePoints[i].gifts.interactable = false;
            }
            else
            {
                requirePoints[i].gifts.GetComponent<Image>().color = startColor;
                requirePoints[i].gifts.interactable = true;
            }
            
        }
    }
    public void GetPrize(Button button)
    {

        if (button.name == "Element0")
        {
            punkty += requirePoints[0].prize;
            
            PlayerPrefs.SetInt("sumaNagrod", (PlayerPrefs.GetInt("sumaNagrod") + requirePoints[0].prize));
            PlayerPrefs.SetInt("gifts0", 1);
            PlayerPrefs.Save();
            requirePoints[0].CuteAnimal.gameObject.SetActive(true);
            requirePoints[0].gifts.GetComponent<Image>().enabled = false;  
        }
        if(button.name == "Element1")
        {
            punkty += requirePoints[1].prize;
            PlayerPrefs.SetInt("sumaNagrod", (PlayerPrefs.GetInt("sumaNagrod") + requirePoints[1].prize));
            PlayerPrefs.SetInt("gifts1", 1);
            PlayerPrefs.Save(); 
            requirePoints[1].CuteAnimal.gameObject.SetActive(true);
            requirePoints[1].gifts.GetComponent<Image>().enabled = false;
            
        }
        if(button.name == "Element2")
        {
            punkty += requirePoints[2].prize;
            PlayerPrefs.SetInt("sumaNagrod", (PlayerPrefs.GetInt("sumaNagrod") + requirePoints[2].prize));
            PlayerPrefs.SetInt("gifts2", 1);
            PlayerPrefs.Save();
            requirePoints[2].CuteAnimal.gameObject.SetActive(true);
            requirePoints[2].gifts.GetComponent<Image>().enabled = false;
        }
        if(button.name == "Element3")
        {
            punkty += requirePoints[3].prize;
            PlayerPrefs.SetInt("sumaNagrod", (PlayerPrefs.GetInt("sumaNagrod") + requirePoints[3].prize));
            PlayerPrefs.SetInt("gifts3", 1);
            PlayerPrefs.Save();
            requirePoints[3].CuteAnimal.gameObject.SetActive(true);
            requirePoints[3].gifts.GetComponent<Image>().enabled = false;
        }
        if (button.name == "Element4")
        {
            punkty += requirePoints[4].prize;
            PlayerPrefs.SetInt("sumaNagrod", (PlayerPrefs.GetInt("sumaNagrod") + requirePoints[4].prize));
            PlayerPrefs.SetInt("gifts4", 1);
            PlayerPrefs.Save();
            requirePoints[4].CuteAnimal.gameObject.SetActive(true);
            requirePoints[4].gifts.GetComponent<Image>().enabled = false;
        }
        if (button.name == "Element5")
        {
            punkty += requirePoints[5].prize;
            PlayerPrefs.SetInt("sumaNagrod", (PlayerPrefs.GetInt("sumaNagrod") + requirePoints[5].prize));
            PlayerPrefs.SetInt("gifts5", 1);
            PlayerPrefs.Save();
            requirePoints[5].CuteAnimal.gameObject.SetActive(true);
            requirePoints[5].gifts.GetComponent<Image>().enabled = false;
        }
        SetGifts();
        SetPoints();
        
    }
    void SetPoints()
    {
        Debug.Log(PlayerPrefs.GetInt("sumaNagrod"));
        wyniki.sumaPrezentow = PlayerPrefs.GetInt("sumaNagrod");
        wyniki.wyniki.text = wyniki.suma.ToString();

    }

}
