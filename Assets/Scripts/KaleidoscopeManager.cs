using UnityEngine;
using System.Collections;

public class KaleidoscopeManager : MonoBehaviour {
	public KaleidoscopeDesign[] Designs;
	public CaptureCam captureCam;

	public float squareSize;
	public float ViewPortWidth;
	public float ViewPortHeight;

	public RenderTexture KaleidoscopeTexture;
	public Material KaleidoscopeMat;

	void Awake() {
		Application.targetFrameRate = 30;
		KaleidoscopeTexture = (RenderTexture) Resources.Load("ScreenTexture");
		KaleidoscopeMat = (Material)Resources.Load("mat");
		Vector3 tr = Camera.main.ViewportToWorldPoint(new Vector3(1,1, Camera.main.nearClipPlane));
		squareSize = tr.x > tr.y ? tr.x : tr.y;
		ViewPortWidth = tr.x;
		ViewPortHeight = tr.y;

		captureCam.Init();
		Designs = new KaleidoscopeDesign[] {
			new Basic(this),
			new Equilateral(this),
            //new Octagons(this) //unfinished
        };

		SetDesign(0);
	}

	public void SetDesign(int i){
		if(Designs.Length > i){
			foreach(KaleidoscopeDesign des in Designs){
				if(des != Designs[i])des.parentObject.SetActive(false);
				else des.parentObject.SetActive(true);
			}
		}
	}
}
