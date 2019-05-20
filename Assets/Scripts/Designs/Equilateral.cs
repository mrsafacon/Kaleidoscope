using UnityEngine;
using System.Collections;

public class Equilateral : KaleidoscopeDesign {

	int triAmount = 3;
	float triSize;
	float triHeight;
	float halfTriSize;

	public Equilateral(KaleidoscopeManager m){
		manager = m;
		parentObject = new GameObject("Equilateral");

		triSize = manager.squareSize / triAmount;
		halfTriSize = triSize / 2;

		triHeight = Mathf.Sqrt(Mathf.Pow(triSize, 2) - Mathf.Pow(halfTriSize, 2));

		float ySpacing = triHeight * 2;
		float xSpacing = 1.5f * triSize;


		int xBlooms_even = Mathf.CeilToInt(manager.ViewPortWidth/triSize/2);
		int xBlooms_odd = Mathf.CeilToInt((manager.ViewPortWidth-halfTriSize)/triSize/2);
		int xBlooms = Mathf.Max(xBlooms_even, xBlooms_odd);
		int yBlooms = Mathf.CeilToInt(manager.ViewPortHeight/ySpacing);

//		Debug.Log(string.Format("Blooms: ({0}, {1})", xBlooms, yBlooms));
//		Debug.Log(string.Format("Even: ({0}, Odd: {1})", xBlooms_even, xBlooms_odd));

		bool evenXisLarger =  xBlooms_even >= xBlooms_odd;

		int i = 0;


		//GenerateBloom(i++);

		for(int x = 0; x <= xBlooms; x++){
			for(int y = 0; y <= yBlooms; y++){
				if(x == 0 && y == 0){
					Transform center = GenerateBloom(i++);
					center.SetParent(parentObject.transform);

				} else if(x % 2 == 0 && xBlooms_even >= x  ){
					Transform q1 = GenerateBloom(i++);
					q1.position = new Vector3(x * xSpacing, y * ySpacing, 0);
					q1.SetParent(parentObject.transform); 

					Transform q2 = GenerateBloom(i++);
					q2.position = new Vector3(x * xSpacing, y * -ySpacing, 0);
					q2.SetParent(parentObject.transform); 

					if(x != 0) {
						Transform q3 = GenerateBloom(i++);
						q3.position = new Vector3(x * -xSpacing, y * -ySpacing, 0);
						q3.SetParent(parentObject.transform); 
					}

					if(x != 0){
						Transform q4 = GenerateBloom(i++);
						q4.position = new Vector3(x * -xSpacing, y * ySpacing, 0);
						q4.SetParent(parentObject.transform); 
					}
				
				} else if (x % 2 != 0 && xBlooms_odd >= x) {
					//Debug.Log(string.Format("{0},{1}",x,y));
					Transform q1 = GenerateBloom(i++);
					q1.position = new Vector3(x * xSpacing, y * ySpacing + (ySpacing/2), 0);
					q1.SetParent(parentObject.transform); 


					Transform q2 = GenerateBloom(i++);
					q2.position = new Vector3(x * xSpacing, y * -ySpacing - (ySpacing/2), 0);
					q2.SetParent(parentObject.transform); 



					Transform q3 = GenerateBloom(i++);
					q3.position = new Vector3(x * -xSpacing, y * -ySpacing - (ySpacing/2), 0);
					q3.SetParent(parentObject.transform); 
					
					
					Transform q4 = GenerateBloom(i++);
					q4.position = new Vector3(x * -xSpacing, y * ySpacing + (ySpacing/2), 0);
					q4.SetParent(parentObject.transform); 

				}

			}
		}

		//go up
		/*
		for(int y = -triAmount; y < triAmount; y++){
			float rowPos = y * triHeight;
			for(int x = 0; x < triAmount * 2; x++ ){
				float xPos = x * triSize;
				Transform tri = GenerateTriangle(i);


				tri.position = new Vector3(xPos,rowPos,0);

				i++;
			}
		}
		*/
	}

	Transform GenerateBloom(int id){
		GameObject go = new GameObject("Bloom " + id);

		for(int i = 1; i < 7; i++){ 	
			Transform t = GenerateTriangle(i);
			t.parent = go.transform;
			t.localEulerAngles = new Vector3(0,0,i*60);
		}
		return go.transform;
	}



	Transform GenerateTriangle(int i){
		//bool flip = (i % 2 == 0);
		GameObject go = new GameObject(i.ToString());
		MeshFilter meshFilter = (MeshFilter)go.AddComponent(typeof(MeshFilter));
		meshFilter.mesh = GenerateMesh();
		MeshRenderer renderer = (MeshRenderer) go.AddComponent(typeof(MeshRenderer));
		
		renderer.material = manager.KaleidoscopeMat;
		renderer.material.mainTexture = manager.KaleidoscopeTexture;
		
		
		//go.layer = LayerMask.NameToLayer("Screen");
		go.transform.parent = parentObject.transform;
		//if(flip) go.transform.eulerAngles = new Vector3(0, 0, 180);
		return go.transform;			
	}

	Mesh GenerateMesh(){
		Mesh m = new Mesh();
		m.name = "Eq Triangle";


		m.vertices = new Vector3[]{
			Vector3.zero,
			new Vector3(-halfTriSize, triHeight, 0),
			new Vector3(halfTriSize, triHeight, 0),

		};

		//float origin = ;


		m.uv = new Vector2[]{
			new Vector2(0,0),
			new Vector2(.5f,1),
			new Vector2(0,1),
		};

		m.triangles = new int[]{
			0,1,2
		};
		
		m.RecalculateNormals();

		return m;
	}
}
