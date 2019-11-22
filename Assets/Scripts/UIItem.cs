using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIItem : MonoBehaviour, IPointerClickHandler{

    public Item item;
    private Image imagem;
    private UIItem itemSelect;
    private Inventario inventario;
    bool isMouseOverEmpty = false;
    bool isMouseOverItem = false;
    Item itemOver;


    void Awake()
    {
        // imagem que está no item
        imagem = GetComponent<Image>();
        //UpdateIcon(null); // espaço vazio

        // inventário
        inventario = GameObject.Find("Player").GetComponent<Inventario>();

        // o Item selecionado:
        itemSelect = GameObject.Find("ItemSelect").GetComponent<UIItem>();
    }

    void Start()
    {
        // atualizar itens existentes
        UpdateIcon(item);
    }


    // atualizar o ícone do item por fora
    public void UpdateIcon(Item item)
    {
        this.item = item;

        if (this.item != null)
        {
            // mostra o icone
            imagem.color = Color.white;
            imagem.sprite = this.item.icon; // pega o ícone que está em item e passa para a imagem


            // se for equipado:
            if (this.item.equipped)
            {
                transform.parent.GetComponent<Image>().color = new Color(255,255,255, 0.7f);
            }
        }

        else
        {
            // esconde
            imagem.color = Color.clear;
        }
    }


    // quando clicar, equipar
    public void EquipItem(Item item)
    {
        // o item do slot recebe o valor do parametro
        this.item = item;

        // se o slot não estiver vazio
        if (this.item != null)
        {
            this.item.equipped = true;
            UpdateIcon(item);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicou em um item");
        // se clicar em um espaço com item equipável
        if (this.item != null && this.item.isEquip)
        {
            Debug.Log("É um item equipável");

            // se a lista no inventário contém um item que está equipado
            if (inventario.GetItemEquipped() != null)
            {
                // desequipar o item
                inventario.GetItemEquipped().equipped = false;
                // equipar o item clicado
                this.item.equipped = true;
                Debug.Log("Trocou o item que já estava equipado");

            }
            // se não contém nenhum item equipado ainda
            else
            {
                this.item.equipped = true;
                Debug.Log("Equipou o item");

            }

            // no fim atualizar
            UpdateIcon(this.item);
        }
        else
        Debug.Log("Espaço vazio!!");

    }



    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    // se clicar nesse espaço e existir item (UI)
    //    if (this.item != null)
    //    {
    //        Debug.Log("Entrou em esse item não está vazio");
    //        // e se já tiver um item selecionado
    //        if (itemSelect.item != null)
    //        {
    //            Debug.Log("Entrou em item do itemselect não está vazio");

    //            // cria um clone do item clicado
    //            Item clone = new Item(itemSelect.item);
    //            Debug.Log("Criou clone");

    //            // atualiza os ícones
    //            itemSelect.UpdateIcon(this.item);
    //            UpdateIcon(clone);
    //            Debug.Log("Atualizou clone");
    //        }

    //        // mas se não houver um item selecionado
    //        else
    //        {
    //            // atualiza o ícone do item selecionado
    //            itemSelect.UpdateIcon(this.item);

    //            // e deixa o ícone atual vazio
    //            UpdateIcon(null);
    //            Debug.Log("Atualizou item selecionado agora");

    //        }
    //    }

    //    // mas se clicou em espaço vazio e existe um item sendo selecionado
    //    else if (itemSelect.item != null)
    //    {
    //        // atualiza o ícone do que está selecionado para ser esse
    //        UpdateIcon(itemSelect.item);
    //        itemSelect.UpdateIcon(null); // deixa o "selecionado" sem nada
    //        Debug.Log("substituiu icone");

    //    }
    //}

}
