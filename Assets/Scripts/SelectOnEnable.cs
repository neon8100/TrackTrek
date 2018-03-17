using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Selectable))]
public class SelectOnEnable : MonoBehaviour {

	private void OnEnable()
    {
        Selectable button = GetComponent<Selectable>();
        button.Select();
	}

}
