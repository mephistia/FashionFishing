using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// classe do SLOT

public class UIItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{

    public Item item;
    private Image imagem;
    private Text text;
    //private UIItem itemSelect;
    private Inventario inventario;
    private UIInventario uiInventario;
    public Canvas canvas;
    private GraphicRaycaster rc;
    public UIItem itemSelect, slotAtual;

    private Tooltip tooltip;


    void Awake()
    {
        // imagem que está no item
        imagem = GetComponent<Image>();
        //UpdateIcon(null); // espaço vazio

        // inventário
        inventario = GameObject.Find("Player").GetComponent<Inventario>();

        // inventário ui
        uiInventario = GameObject.Find("PanelInventario").GetComponent<UIInventario>();

        // texto
        text = gameObject.GetComponentInChildren<Text>();

        // canvas
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();

        rc = canvas.GetComponent<GraphicRaycaster>();

        itemSelect = GameObject.Find("ItemSelect").GetComponent<UIItem>();

        tooltip = GameObject.Find("Tooltip").GetComponent<Tooltip>();
    }

    void Start()
    {
        // atualizar itens existentes
        UpdateIcon(item);
    }


    // atualizar o ícone do item
    public void UpdateIcon(Item item)
    {
        this.item = item;

        if (this.item != null)
        {
            // mostra o icone
            imagem.color = Color.white;
            imagem.sprite = this.item.icon; // pega o ícone que está em item e passa para a imagem

            if (item.qtd != null)
                text.text = item.qtd.ToString(); // muda a quantidade
            else
                text.text = "∞";


            // se for equipado:
            if (this.item.equipped)
            {
                transform.parent.GetComponent<Image>().color = new Color(255,255,255, 0.7f); // slot transparente
            }
            else
            {
                transform.parent.GetComponent<Image>().color = new Color(255, 255, 255, 1);
            }
        }

        else
        {
            // esconde
            imagem.color = Color.clear;
            text.text = "";
            transform.parent.GetComponent<Image>().color = new Color(255, 255, 255, 1);
        }

        //inventario.UpdateUI();

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        // se clicou em um slot com item
        if (this.item != null)
        {
            // se clicou com botão esquerdo e item não está equipado
            if (eventData.button == PointerEventData.InputButton.Left && !this.item.equipped)
            {
                itemSelect.UpdateIcon(this.item); // o item arrastado copia este item
                UpdateIcon(null); // este item fica vazio
            }


        }
 
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && itemSelect.item != null)
        {
            List<RaycastResult> results = new List<RaycastResult>();
            rc.Raycast(eventData, results);

            foreach (var hit in results)
            {

                UIItem slot = hit.gameObject.GetComponent<UIItem>();

                if (slot)
                {
                    if (slot.item == null) // se estiver vazio
                    {
                        // larga no slot
                        slot.UpdateIcon(itemSelect.item);
                        itemSelect.UpdateIcon(null);
                    }

                    // se estiver ocupado, substitui
                    else
                    {
                        UpdateIcon(slot.item); // o anterior recebe o item de onde foi largado
                        slot.UpdateIcon(itemSelect.item); // esse recebe o item que estava selecionado

                    }
                }

                break;

            }
        }

        // se clicou com o direito e é item equípável
        else if (eventData.button == PointerEventData.InputButton.Right && this.item.isEquip)
        {
            inventario.GetItemEquipped().equipped = false;
            // equipar o item clicado
            this.item.equipped = true;

            // atualiza todos os icones
            foreach (UIItem item in uiInventario.itensNaUI)
            {
                item.UpdateIcon(item.item);
            }
            tooltip.text.text = "";
            tooltip.UpdateText(this.item);
        }


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (this.item != null)
        {
            tooltip.UpdateText(this.item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.text.text = "";
        tooltip.gameObject.SetActive(false);
    }
}

  
