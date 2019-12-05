using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Linq;

public class Interface : MonoBehaviour {

    SerialPort sp;

    public int dado;


    void Start () {

        // inicialização do Serial:
        sp = new SerialPort("COM7", 9600);
        sp.ReadTimeout = 1;

        // abrir a porta
        sp.Open();

    }

    public int ChecarDados()
    {

        try  // evitar dados incompletos
        {
            int dados = sp.ReadByte(); // ler o número enviado
            if (dados != 0)
            {
                Debug.Log("ARDUINO ->  " + dados);
            }
            // limpar buffer
            sp.BaseStream.Flush();
            sp.DiscardInBuffer();
            return dados;
        }
        catch { return 0; }
    }
	
	void Update () {
        if (sp.IsOpen)
        {
            dado = ChecarDados(); // usar info que vier daqui
        }
        else
        {
            Debug.Log("ARDUINO -> 0");
            dado = 0;
        }

        // fecha a conexão ao apertar ESC
        if (Input.GetKeyDown(KeyCode.Escape) && sp.IsOpen)
            sp.Close();
	}
}
