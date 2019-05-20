using UnityEngine;
using System.Collections;

public class ShowFPS : MonoBehaviour {

	CaptureCam cam;
	float deltaTime;
	// Use this for initialization
	void Start () {
		cam = GetComponent<CaptureCam>();
	}
	
	// Update is called once per frame
	void Update () {
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		cam.DebugMsg(text);

	}
}
