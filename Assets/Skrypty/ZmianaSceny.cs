using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZmianaSceny : MonoBehaviour
{
    public string nazwaScenyDoZmiany;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ZmianaScenyGuzik()
    {
        SceneManager.LoadScene(nazwaScenyDoZmiany);
    }
}
