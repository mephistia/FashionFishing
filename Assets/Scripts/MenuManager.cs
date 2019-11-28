using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour {

    GameObject PanelCreditos;
    

	// Use this for initialization
	void Start () {
        PanelCreditos = GameObject.Find("PanelCreditos");
        PanelCreditos.SetActive(false);
	}
	


    public void AbreCreditos()
    {
        PanelCreditos.SetActive(true);
    }


    public void FechaCreditos()
    {
        PanelCreditos.SetActive(false);
    }


    public void Jogar()
    {
        SceneManager.LoadScene("Main");
    }
}
