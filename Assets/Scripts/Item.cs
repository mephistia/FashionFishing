using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public int id;
    public string name;
    public string desc;
    public bool isEquip; // para iscas
    public bool canSell;
    public Sprite icon;

    public float priceBuy;
    public float priceSell;
    public int fama; // requerida para comprar iscas, ganha ao vender peixes
    public int raridade; // 0 = comum, 1 = incomum etc

    public bool equipped = false; // nenhum começa como equipado

    public Item(int id, string name, string desc, bool isEquip, bool canSell, float priceBuy, float priceSell, int fama, int raridade) // sem Sprite icon porque é definido conforme o nome
    {
        this.id = id;
        this.name = name;
        this.desc = desc;
        this.isEquip = isEquip;
        this.canSell = canSell;
        this.icon = Resources.Load<Sprite>("Sprites/" + name); // nomear com mesmo nome do arquivo
        this.priceBuy = priceBuy;
        this.priceSell = priceSell;
        this.fama = fama;
        this.raridade = raridade;

    }


    // cópia de item
    public Item(Item item)
    {
        this.id = item.id;
        this.name = item.name;
        this.desc = item.desc;
        this.isEquip = item.isEquip;
        this.canSell = item.canSell;
        this.icon = Resources.Load<Sprite>("Sprites/" + name); // nomear com mesmo nome do arquivo
        this.priceBuy = item.priceBuy;
        this.priceSell = item.priceSell;
        this.fama = item.fama;
        this.raridade = item.raridade;

    }
}
