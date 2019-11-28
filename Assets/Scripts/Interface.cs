using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;
using System.Linq;

public class Interface : MonoBehaviour {

    SerialPort sp;


    void Start () {

        // inicialização do Serial:
            // trocar a string da porta!!
        sp = new SerialPort("COM4", 115200, Parity.None, 8, StopBits.One);
        sp.DtrEnable = false;
        sp.ReadTimeout = 1;
        sp.WriteTimeout = 1;

        sp.Open();
        if (sp.IsOpen)
            sp.Write("Hello World");
        else
            Debug.LogError("Serial port: " + sp.PortName + " está indisponível");

    }

    public string ChecarDados()
    {

        try  // evitar dados incompletos
        {
            string dados = sp.ReadLine();
            int tamDados = dados.Count();
            if (tamDados > 0)
            {
                Debug.Log("ARDUINO->|| " + dados + " ||TAMANHO:" + tamDados.ToString());
            }
            // limpar buffer
            tamDados = 0;
            sp.BaseStream.Flush();
            sp.DiscardInBuffer();
            return dados;
        }
        catch { return string.Empty; }
    }
	
	void Update () {
        ChecarDados(); // usar info que vier daqui

        // fecha a conexão ao apertar ESC
        if (Input.GetKeyDown(KeyCode.Escape) && sp.IsOpen)
            sp.Close();
	}
}
