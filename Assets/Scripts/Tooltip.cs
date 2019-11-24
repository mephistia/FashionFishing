using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    public Text text;

	// Use this for initialization
	void Start () {
        text = GetComponentInChildren<Text>();
        gameObject.SetActive(false);

        text.text = "";
		
	}

    public void UpdateText(Item item)
    {
        string[] frases = new string[3];

        string t = item.name.Replace("_", " ");

        // nome e descrição
        frases[0] = t;
        frases[1] = "\n\n" + item.desc;

        // se é equipável
        if (item.isEquip)
        {
            if (!item.equipped)
                frases[2] = "\n\n\nClique com o Botão Direito para equipar a isca";
            else
                frases[2] = "\n\n\nEquipada";
        }

        else
        {
            frases[2] = "\n\n\nVocê pode vender o peixe na Loja";
        }

        for (int i = 0; i < 3; i++)
        {
            text.text += frases[i];
        }


        // ativa o objeto
        gameObject.SetActive(true);
    }
}
