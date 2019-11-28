using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtualizaMoedas : MonoBehaviour {

    public GameObject textMoedas, textFama;
    public Inventario inventario;

    // Update is called once per frame
    void Update () {

        textMoedas.GetComponent<Text>().text = inventario.moedasEFama.getMoedas().ToString();
        textFama.GetComponent<Text>().text = inventario.moedasEFama.getFama().ToString();

    }
}
