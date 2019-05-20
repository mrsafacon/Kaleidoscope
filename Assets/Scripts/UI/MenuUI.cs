using UnityEngine;
using System.Collections;

public class MenuUI : MonoBehaviour {

	public GameObject Menu;

	KaleidoscopeManager manager;
	public CaptureCam captureCamera;

	void Start(){
		manager = GetComponent<KaleidoscopeManager>();
		Menu.SetActive(false);
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.Menu) || Input.GetKeyDown(KeyCode.Space)){
			 Menu.SetActive(true);
		}
	}

	public void Oscillate(float f){
		float rate = f/4;
		if(f > -20 && f < 20) rate = 0;
		captureCamera.SetOscillationRate(rate);
	}

	public void SetCamera(float i){
		int cam  = (int) Mathf.RoundToInt(i);	
		captureCamera.SetCamera(cam);
	}

	public void SetDesign(float i){
		int design  = (int) Mathf.RoundToInt(i);
		manager.SetDesign(design);
	}

	public void CloseMenu(){
		Menu.SetActive(false);
	}
}
