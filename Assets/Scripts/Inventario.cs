using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {

    // cria a database
    public DadosItens database;

    // lista de itens no inventário
    public List<Item> inventario = new List<Item>();

    // inventário da UI
    public UIInventario inventarioUI;

    // moedas
    public Moedas moedasEFama;


    void Start()
    {
        // adicionar a isca comum como padrão e equipar
        AddItem("Isca_Comum");
        GetItem("Isca_Comum").equipped = true;
    }

    // adiciona um item no inventário pelo id na database
    public void AddItem(int id)
    {
        Item newItem = database.getItem(id); // busca o item

        // se ja existe o item no inventario
        if (inventario.Contains(database.getItem(id)))
        {
            database.getItem(id).qtd++;
        }
        else
           inventarioUI.AddItemUI(newItem); // adiciona o item na interface se não existir ainda

        inventario.Add(newItem); // adiciona o item


    }

    // adiciona um item pelo nome
    public void AddItem(string name)
    {
        Item newItem = database.getItem(name);
        // se ja existe o item no inventario
        if (inventario.Contains(database.getItem(name)))
        {
            database.getItem(name).qtd++;
        }
        else
            inventarioUI.AddItemUI(newItem); // adiciona o item na interface se não existir ainda

        inventario.Add(newItem); // adiciona o item
    }

    // pegar um item do inventário por id
    public Item GetItem(int id)
    {
        return inventario.Find(item => item.id == id);
    }

    // pegar um item do inventário por string
    public Item GetItem(string name)
    {
        return inventario.Find(item => item.name == name);
    }

    public Item GetItemEquipped()
    {
        return inventario.Find(item => item.equipped == true);
    }
    // remover o item do inventário(ou vender) por id
    public void RemoveItem(int id)
    {
        // busca pelo item do id no parametro
        Item newItem = GetItem(id);

        // se o item existir
        if (newItem != null)
        {
            // remove do inventário
            inventario.Remove(newItem);
            inventarioUI.RemoveItemUI(newItem); // remove da interface
        }
    }

    // remover o item do inventário(ou vender) por string
    public void RemoveItem(string name)
    {
        // busca pelo item do nome no parametro
        Item newItem = GetItem(name);

        // se o item existir
        if (newItem != null)
        {
            // remove do inventário
            inventario.Remove(newItem);
            inventarioUI.RemoveItemUI(newItem); // remove da interface
        }


    }


    // para setar o item do inventário como equipado
    public void SetEquipped(int id)
    {
        // procura para ver se existe
        Item paraEquipar = GetItem(id);

        if (paraEquipar != null)
        {
            // pega o item do inventário e seta como equipado
            inventario.Find(item => item.id == id).equipped = true;
        }
    }

    // para setar o item do inventário como equipado
    public void SetEquipped(string name)
    {
        // procura para ver se existe
        Item paraEquipar = GetItem(name);

        if (paraEquipar != null)
        {
            // pega o item do inventário e seta como equipado
            inventario.Find(item => item.name == name).equipped = true;
        }
    }



}
