using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelPescaManager : MonoBehaviour {
    public GameObject PanelSucesso, PanelFracasso, DescFracasso, SlotPescado, TituloSucesso, DescSucesso, BotaoVender; // setar no editor
    private Inventario inventario;
    private Item itemObtido;
    DadosItens dados;



    //teste:
    void Start()
    {
        inventario = GameObject.Find("Player").GetComponent<Inventario>();
        dados = inventario.database;
        Debug.Log("Painel Criado");
    }


    void OnEnable()
    {
        atualizaSucesso();
        atualizaFracasso();
        Debug.Log("Painel atualizado");
    }

    public void atualizaSucesso()
    {
        // trocar ícone pelo peixe pescado
        // se estiver com cada tipo de isca
        Item equipado = inventario.GetItemEquipped();
        Debug.Log("Item existe? " + equipado);
        if (equipado.name == "Isca_Comum")
        {
            SlotPescado.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Peixique");
            TituloSucesso.GetComponent<Text>().text = "Parabéns! Você pescou um Peixique";

            // adiciona o item ao inventário
            inventario.AddItem("Peixique");
            itemObtido = dados.getItem("Peixique");

            DescSucesso.GetComponent<Text>().text = "O peixe mais estiloso da lagoa.\nVocê recebeu " + itemObtido.fama + " de fama. Valor de Venda: $" + itemObtido.priceSell;
            inventario.moedasEFama.addFama(itemObtido.fama);

        }
        else if (equipado.name == "Isca_Rosa")
        {
            SlotPescado.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/Baka-lhau");
            TituloSucesso.GetComponent<Text>().text = "Parabéns! Você pescou um Baka-lhau";

            // adiciona o item ao inventário
            inventario.AddItem("Baka-lhau");
            itemObtido = dados.getItem("Baka-lhau"); // cópia do item

            DescSucesso.GetComponent<Text>().text = "Esse peixe tem um pouco de vergonha.\nVocê recebeu " + itemObtido.fama + " de fama. Valor de Venda: $" + itemObtido.priceSell;
            inventario.moedasEFama.addFama(itemObtido.fama);
        }
    }

    public void atualizaFracasso()
    {
        Item equipado = inventario.GetItemEquipped();

        if (equipado.name == "Isca_Comum")
        {
            DescFracasso.GetComponent<Text>().text = "Você perdeu 10 de fama. Tente novamente.";
            inventario.moedasEFama.addFama(-10);
        }

        else if (equipado.name == "Isca_Rosa")
        {
            DescFracasso.GetComponent<Text>().text = "Você perdeu 15 de fama. Tente novamente.";
            inventario.moedasEFama.addFama(-15);

        }
    }

    // quando clicar em vender
    public void Vender()
    {
        inventario.RemoveItem(itemObtido.id);
        DescSucesso.GetComponent<Text>().text += "\n <b>Vendido!</b>";
        BotaoVender.GetComponent<Button>().interactable = false;

        inventario.moedasEFama.addMoedas(itemObtido.priceSell);
        
    }

}
