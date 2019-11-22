using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeguirMouse : MonoBehaviour {

	void Update()
    {

        // o item no espaço de item selecionado fica junto do mouse
        transform.position = Input.mousePosition;
    }
}
