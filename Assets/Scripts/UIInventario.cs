using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInventario : MonoBehaviour {


    public List<UIItem> itensNaUI = new List<UIItem>();
    public GameObject slotPrefab;
    public Transform slotPanel;
    public int slotQtd = 9;

    void Awake()
    {

        // para quantidade de slots:
        for (int i = 0; i < slotQtd; i++)
        {
            GameObject novo = Instantiate(slotPrefab); // cria um novo slot
            novo.transform.SetParent(slotPanel); // adiciona o Panel como pai
            itensNaUI.Add(novo.GetComponentInChildren<UIItem>()); // adiciona o objeto da classe UIItem que está no slot na lista do inventário
        }
    }

    // atualiza o slot do índice na lista, colocando o ícone do item
    public void UpdateSlot(int slot, Item item)
    {
        itensNaUI[slot].UpdateIcon(item);

    }




    public void AddItemUI(Item item)
    {
        // atualiza a imagem do primeiro slot vazio que encontrar
        UpdateSlot(itensNaUI.FindIndex(i => i.item == null), item);
    }

    public void RemoveItemUI(Item item)
    {
        // remove a imagem do slot (deixa sem item) que tem o ícone do item
        UpdateSlot(itensNaUI.FindIndex(i => i.item == item), null);
    }


}
