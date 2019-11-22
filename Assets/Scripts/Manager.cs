using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Manager : MonoBehaviour {

    GameObject botoes, PanelInventario, PanelLoja, BarraFundo, BarraFill, AreaPescar, PanelSucesso;
    bool podeLancar;
    bool timer;
    float myTimer;
    bool lancou;
    float numgerado;
    bool podefisgar;
    bool podepuxar;
    Text feedback;
    float vel;
    float tamAtual;
    bool up;

    public enum estados { PERTO, PERFEITO, LONGE}; // 0, 1, 2
    estados estadolinha;

	// Use this for initialization
	void Start () {
        BarraFundo = GameObject.Find("BarraFundo");
        BarraFill = GameObject.Find("BarraFill");
        AreaPescar = GameObject.Find("AreaPescar");
        PanelSucesso = GameObject.Find("PanelSucesso");
        podeLancar = false;
        botoes = GameObject.Find("Botoes");
        timer = false;
        myTimer = 0f;
        lancou = false;
        numgerado = 0f;
        podefisgar = false;
        podepuxar = false;
        feedback = GameObject.Find("TextoUI").GetComponent<Text>();
        PanelInventario = GameObject.Find("PanelInventario");

        PanelInventario.SetActive(false);

        BarraFundo.SetActive(false);
        PanelSucesso.SetActive(false);



        vel = 1.8f;
        

        tamAtual = 0;
        up = true;

    }

    // Update is called once per frame
    void Update () {

        if (lancou)
        {
            Debug.Log("Lançou!");
            feedback.text += " - Linha lançada!";
            // quando chegar a um segundo
            if (myTimer >= 1.0f)
            {
                // zera o timer pra já começar de novo o update em 0
                myTimer = 0f;

                // chama a função para gerar número
                numgerado = gerarnum();

                // se for 1, tranca o timer
                if (numgerado >= 0.5)
                {
                    timer = false;
                    // fechar o loop e mexer o peixe
                    lancou = false;
                    mexer();
                }
                else
                {
                    Debug.Log("Tirou menor :(");
                }

            }
        }


        if (timer)
        {
            myTimer += Time.deltaTime;
        }

		if (podeLancar)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                myTimer = 0f;
                timer = true;
                Debug.Log("Pressionado em: " + Time.deltaTime * 60f);
                feedback.text = "Lançando...";
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                timer = false;
                Debug.Log("Tempo agora: " + Time.deltaTime * 60f);
                Debug.Log("Tempo do timer: " + myTimer);

                if (myTimer > 1.3f) { estadolinha = estados.LONGE; }
                else if (myTimer > 0.3f) { estadolinha = estados.PERFEITO; }
                else { estadolinha = estados.PERTO; }

                podeLancar = false;

                // passar pra próxima função
                lancoulinha(estadolinha);

            }
        }

        if (podefisgar)
        {
            if (Input.GetKeyDown(KeyCode.Space)){
                podepuxar = true;
            }
        }

        if (podepuxar)
        {
            // mostrar a barra
            BarraFundo.SetActive(true);


            if (up)
            { 
                //sobe
                tamAtual += Time.deltaTime / vel;
                BarraFill.GetComponent<Image>().fillAmount = tamAtual;

                if (BarraFill.GetComponent<Image>().fillAmount > 0.99f)
                {
                    up = false;
                }


            }

            else
            {
                // desce
                tamAtual -= Time.deltaTime / vel;
                BarraFill.GetComponent<Image>().fillAmount = tamAtual;

                if (BarraFill.GetComponent<Image>().fillAmount <= 0)
                {
                    up = true;
                }
            }


            if (Input.GetKeyDown(KeyCode.Space))
            {
               // verificar se está na área correta
               if (BarraFill.GetComponent<Image>().fillAmount > 0.46 && BarraFill.GetComponent<Image>().fillAmount < 0.62)
                {
                    // mostrar tela de sucesso
                    podeLancar = false;
                    BarraFundo.SetActive(false);
                }
               else
                {
                    // mostrar tela de fracasso
                    podeLancar = false;
                    BarraFundo.SetActive(false);

                }

            }
        }
	}

    public void hideBotoes()
    {
        botoes.SetActive(false);
        podeLancar = true;
    }

    public void lancoulinha(estados estado)
    {
     
        if (estado == estados.LONGE || estado == estados.PERTO)
        {
            // peixes normais
            Debug.Log("Peixes normais...");
            feedback.text = "Área de peixes normais...";

        }
        else
        {
            // peixes bons
            Debug.Log("Peixes bons!");
            feedback.text = "Área de peixes bons...";
        }

        // aleatório para fisgar ou não:
        // zerar timer
        myTimer = 0f;
        // iniciar timer novamente
        timer = true;

        lancou = true;
    }

   
    float gerarnum()
    {
        int et = Mathf.FloorToInt(Time.deltaTime * 10000f);
        Random.InitState(et);
        Debug.Log("Seed é: " + et);

        float resultado = Random.Range(0f, 1f);

        Debug.Log(resultado);
        return resultado;
    }

    void mexer()
    {
        Debug.Log("Peixe mexendo!!");
        feedback.text = "O peixe está se mexendo!";
        podefisgar = true;

        // feedback do peixe mexendo...
    }


    public void AbrirInventario()
    {
        PanelInventario.SetActive(!PanelInventario.activeInHierarchy);
    }


    public void AbrirLoja()
    {
        PanelLoja.SetActive(!PanelInventario.activeInHierarchy);
    }


    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}
