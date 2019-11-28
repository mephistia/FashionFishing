using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moedas : MonoBehaviour {

    float moedas, fama;

    void Awake()
    {
        moedas = 0;
        fama = 0;
    }


    public void addMoedas(float i)
    {
        moedas += i;

        // se ficar menor que 0, deixa 0
        if (moedas < 0) { moedas = 0; }
    }

    public void addFama(float i)
    {
        fama += i;
        if (fama < 0) { fama = 0; }

    }

    public float getMoedas()
    {
        return moedas;
    }

    public float getFama()
    {
        return fama;
    }

    // para "gastar" é só add valor negativo
}
