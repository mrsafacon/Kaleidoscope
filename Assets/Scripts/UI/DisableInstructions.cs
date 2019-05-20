using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableInstructions : MonoBehaviour {
    public GameObject Instructions;

	void Start () {
        Invoke("HideInstructions", 4);
	}
	
    void HideInstructions() {
        Instructions.SetActive(false);
    }
}
