using UnityEngine;
using System.Collections;

public class ToggleCameraUIElement : MonoBehaviour { //incase device has multiple cameras (includes mobile devices)

	public UnityEngine.UI.Slider slider;


	void Awake(){
		slider = GetComponent<UnityEngine.UI.Slider>();
	}

	public void Toggle(){
		if(slider.value == 0) slider.value = 1;
		else slider.value = 0;
	}
}
