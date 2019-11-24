using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleOpen : MonoBehaviour {

    public GameObject parent;
    public Manager manager;

    void Start()
    {
        parent = gameObject.transform.parent.parent.gameObject; // objeto pai do pai
        manager = GameObject.Find("GameManager").GetComponent<Manager>();
    }
    public void ToggleGrandparent()
    {        
       parent.SetActive(!parent.activeSelf);
    }

    public void ToggleSelf()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ToggleParent()
    {
        gameObject.transform.parent.gameObject.SetActive(!gameObject.transform.parent.gameObject.activeSelf);

    }



}
