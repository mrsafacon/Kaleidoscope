using UnityEngine;
using System.Collections;

public class CaptureCam : MonoBehaviour {
	Transform screen;
	WebCamDevice[] devices;
	public WebCamTexture monitorTexture = null;
	int currentDevice;

	float minScale = 1.5f;
	float maxScale = 4f;

	float oscillateRate = 0;

	public UnityEngine.UI.Text DEBUG = null;

	public void Init(){
		devices = WebCamTexture.devices;
		monitorTexture = new WebCamTexture (devices[currentDevice].name);

		screen = GenerateScreen();
		transform.position = new Vector3(40,0,0);
		monitorTexture.Play();
	}

    private void Update() {
        if (Input.mouseScrollDelta.y != 0) Zoom(Input.mouseScrollDelta.y * 6);
        screen.Rotate(new Vector3(0, 0, oscillateRate * Time.deltaTime));
    }

    Transform GenerateScreen(){
		GameObject go = new GameObject("Screen");
		go.transform.parent = transform;
		go.transform.position = new Vector3(0,0,GetComponent<Camera>().nearClipPlane);
		MeshFilter meshFilter = (MeshFilter)go.AddComponent(typeof(MeshFilter));
		meshFilter.mesh = GenerateMesh();
		MeshRenderer renderer = (MeshRenderer) go.AddComponent(typeof(MeshRenderer));
		
		renderer.material = (Material)Resources.Load("mat");
		renderer.material.mainTexture = monitorTexture;


		go.layer = LayerMask.NameToLayer("Screen");
		go.transform.localScale = new Vector3(minScale, minScale, 0);
		return go.transform;
	}

	Mesh GenerateMesh(){
		Mesh m  = new Mesh();
		Camera cam = GetComponent<Camera>();
		m.name = "screen";
		m.vertices = new Vector3[]{
			cam.ViewportToWorldPoint(new Vector3(0,0,0)),
			cam.ViewportToWorldPoint(new Vector3(0,1,0)),
			cam.ViewportToWorldPoint(new Vector3(1,1,0)),
			cam.ViewportToWorldPoint(new Vector3(1,0,0)),
		};

		m.uv = new Vector2[]{
			new Vector2(0,0),
			new Vector2(0,1),
			new Vector2(1,1),
			new Vector2(1,0)
		};

		m.triangles = new int[]{
			0,1,2,
			0,2,3
		};


		m.RecalculateNormals();

		return m;
	}
	


	public void FlipCamera(){
		currentDevice++;
		if(devices.Length <= currentDevice) currentDevice = 0;
		monitorTexture.Stop();
		monitorTexture.deviceName = devices[currentDevice].name;
		monitorTexture.Play();
	}

	public void SetCamera(int i){
		if(devices.Length > i){
			monitorTexture.Stop();
			monitorTexture.deviceName = devices[i].name;
			monitorTexture.Play();
		}
	}

	public void Zoom(float i){
		i = i * Time.deltaTime * .1f;
		float current = screen.localScale.x;
		float target = current + i;
		target = Mathf.Clamp(target, minScale, maxScale);
		screen.localScale = new Vector3(target, target, 1);

		DebugMsg("Scale: " + screen.localScale.x.ToString());
	}

	public void DebugMsg (string s){
		if(DEBUG != null) DEBUG.text = s;
	}

	public void Rotate(VelocityDirection d, float i){
		switch(d){
		case VelocityDirection.clockwise:
			screen.Rotate(new Vector3(0,0,i * Time.deltaTime));
			break;
		case VelocityDirection.counterclockwise:
			screen.Rotate(new Vector3(0,0,i * Time.deltaTime * -1));
			break;
		}
	}

	public void SetOscillationRate(float f){
		oscillateRate = f;
	}


}
