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
            // trocar a string da porta!!
        sp = new SerialPort("COM7", 9600/*, Parity.None, 8, StopBits.One*/);
        //sp.DtrEnable = false;
        sp.ReadTimeout = 1;
        //sp.WriteTimeout = 1;

        sp.Open();

        //if (sp.IsOpen)
        //    sp.Write("Hello World");
        //else
        //    Debug.LogError("Serial port: " + sp.PortName + " está indisponível");

    }

    public int ChecarDados()
    {

        try  // evitar dados incompletos
        {
            int dados = sp.ReadByte();
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
