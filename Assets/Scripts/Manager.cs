using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class Manager : MonoBehaviour {

    public GameObject botoes, PanelInventario, PanelLoja, BarraFundo, BarraFill, AreaPescar, PanelSucesso, PanelFracasso;
    GameObject AguaGif;

    bool podeLancar;
    bool timer;
    float myTimer;
    bool lancou;
    float numgerado;
    public bool podefisgar, podepuxar, podepuxarArduino, apertouEspaco;
    Text feedback;
    float vel;
    float tamAtual;
    bool up;
    public enum estados { PERTO, PERFEITO, LONGE}; // 0, 1, 2
    estados estadolinha;
    public Interface arduino;
    public int dado;

    void Start () {

        PanelFracasso = GameObject.Find("PanelFracasso");
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
        podepuxarArduino = false;
        apertouEspaco = false;

        feedback = GameObject.Find("TextoUI").GetComponent<Text>();
        PanelInventario = GameObject.Find("PanelInventario");

        PanelInventario.SetActive(false);

        BarraFundo.SetActive(false);
        PanelSucesso.SetActive(false);
        PanelFracasso.SetActive(false);

        AguaGif = GameObject.Find("AguaImagem");
        AguaGif.SetActive(false);



        vel = 0.5f;
        

        tamAtual = 0;
        up = true;

        feedback.text = " ";

        dado = arduino.dado;


}

// Update is called once per frame
void Update () {

        // atualiza o dado
        dado = arduino.dado;

        // teste com teclado
        if (Input.GetKeyDown(KeyCode.Z)){
            dado = 2;
        }

        if (lancou)
        {
            Debug.Log("Lançou!");
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
            // se pressionar o botão, ativa uma flag
       

            if (Input.GetKeyDown(KeyCode.Space))
            {
                //myTimer = 0f;
                //timer = true;
                //Debug.Log("Pressionado em: " + Time.deltaTime * 60f);
                //feedback.text = "Lançando...";


                apertouEspaco = true; // deixa disponível para lançar

            }

            if (apertouEspaco) // pode lançar, mas confere a cada update
            {

                // depois que ativar a flag, se mexeu para frente lança e verifica a distância:

                if (dado == 1)
                {
                    estadolinha = estados.PERTO;
                }
                else if (dado == 2)
                {
                    estadolinha = estados.PERFEITO;
                }
                else if (dado == 3)
                {
                    estadolinha = estados.LONGE;
                }

                podeLancar = false;

                lancoulinha(estadolinha);

            }



            //if (Input.GetKeyUp(KeyCode.Space))
            //{
            //    timer = false;
            //    Debug.Log("Tempo agora: " + Time.deltaTime * 60f);
            //    Debug.Log("Tempo do timer: " + myTimer);

            //    if (myTimer > 1.3f) { estadolinha = estados.LONGE; }
            //    else if (myTimer > 0.3f) { estadolinha = estados.PERFEITO; }
            //    else { estadolinha = estados.PERTO; }

            //    podeLancar = false;

            //    // passar pra próxima função
            //    lancoulinha(estadolinha); // vai para "mexer()"

            //}


        }

        if (podefisgar)
        {


            // se fez o movimento de puxar, podepuxar é true
            if (dado == 1 || dado == 2 || dado == 3)
            {

                podepuxar = true;
                podefisgar = false;
            }

            
        }

        if (podepuxar)
        {
            // mostrar a barra

            BarraFundo.SetActive(true);

            feedback.text = "Pressione Espaço quando a barra estiver na área indicada!";


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


            // se apertar o botão no arduino
            if (Input.GetKeyDown(KeyCode.Space))
            {

                AguaGif.SetActive(false);

                // verificar se está na área correta
                if (BarraFill.GetComponent<Image>().fillAmount > 0.46 && BarraFill.GetComponent<Image>().fillAmount < 0.62)
                {
                    // aguardar puxar a vara de volta
                    puxarLinhaFisgado();
                    BarraFundo.SetActive(false);
                    podepuxar = false;

                }
                else
                {
                    // mostrar tela de fracasso
                    BarraFundo.SetActive(false);
                    PanelFracasso.SetActive(true);
                    botoes.SetActive(true);
                    feedback.text = "";
                    BarraFill.GetComponent<Image>().fillAmount = 0;
                    podepuxar = false;


                }

            }
        }

        if (podepuxarArduino) // quando puder puxar
        {
            // verifica se puxou no acelerometro e mostra tela de sucesso


            if (dado == 1 || dado == 2 || dado == 3)
            {
                PanelSucesso.SetActive(true);
                botoes.SetActive(true);
                feedback.text = "";
                BarraFill.GetComponent<Image>().fillAmount = 0;
                podepuxarArduino = false;

            }


        }
    }

    public void puxarLinhaFisgado()
    {
        feedback.text = "Puxe a vara para pescar o peixe";
        podepuxarArduino = true;


        
    }

    public void hideBotoes()
    {
        botoes.SetActive(false);
        feedback.text = "Mantenha a vara para trás e aperte Espaco, então lance!";
        podeLancar = true;
    }

    public void lancoulinha(estados estado)
    {
     
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
        feedback.text = "O peixe está se mexendo! Puxe a vara para fisgá-lo!";

        // feedback do peixe mexendo... (mostrar imagem)
        AguaGif.SetActive(true);

        podefisgar = true;

    }


    public void AbrirInventario()
    {
        PanelInventario.SetActive(!PanelInventario.activeInHierarchy);
    }


    public void AbrirLoja()
    {
        PanelLoja.SetActive(!PanelInventario.activeInHierarchy);
    }

    public void ToggleOpen()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }


    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }


}
