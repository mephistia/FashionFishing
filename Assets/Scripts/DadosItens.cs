using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// dados de todos os itens
public class DadosItens : MonoBehaviour {

    public List<Item> itens = new List<Item>();


    private void Awake()
    {
        // cria os itens ao carregar o script
        criarItens();
    }

    public void criarItens()
    {
        itens = new List<Item>()
        {
            // iscas: (substituir & ao apresentar o nome)
            new Item(0, "Isca_Comum", "Uma isca normal. Pesca peixes Comuns", true, false, 0, 0, 0, 0),
            new Item(1, "Isca_Rosa", "Uma isca um pouco feminina. Pesca peixes Incomuns", true, true, 10, 5, 5, 1),

            // peixes:
            new Item(2, "Peixique", "O peixe mais estiloso da lagoa", false, true, 0, 10, 10, 0),
            new Item (3, "Baka-lhau", "Esse peixe tem um pouco de vergonha", false, true, 0, 15, 15, 1)

        };
    }


    // get
    public Item getItem(int id)
    {
        // procura na lista um objeto Item que tenha o id igual o id do parametro
        // bom para adicionar itens
        return itens.Find(item => item.id == id); 
    }

    // get com nome
    public Item getItem(string name)
    {
        // retorna o item da lista que tem o mesmo nome do parametro
        return itens.Find(item => item.name == name);
    }

    // add um novo item (já existente) na database
    public void addItem(Item novoItem)
    {
        itens.Add(novoItem);
    }


    // add um novo item (criado no momento) na database
    public void addItem(int id, string name, string desc, bool isEquip, bool canSell, float priceBuy, float priceSell, int fama, int raridade)
    {
        Item novoItem = new Item(id, name, desc, isEquip, canSell, priceBuy, priceSell, fama, raridade);
        itens.Add(novoItem);
    }
}
