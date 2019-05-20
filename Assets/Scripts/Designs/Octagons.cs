using UnityEngine;
using System.Collections;

public class Octagons : KaleidoscopeDesign { //unfinished design

	int bloomCount = 8;
	float octSize;
	float bloomSize;


	enum OctType{ Oct1, Oct2, Oct3, Oct4 }

	public Octagons(KaleidoscopeManager m){
		manager = m;

		octSize =  (bloomCount / 2) / manager.squareSize;
		bloomSize = octSize * 4;

		int xSize = Mathf.CeilToInt(manager.ViewPortWidth/bloomSize) + 1;
		int ySize =  Mathf.CeilToInt(manager.ViewPortHeight/bloomSize);

		parentObject = new GameObject("Octagons");

		for(int x = 0; x < xSize; x++){
			for (int y = 0; y < ySize; y++){
				Transform b1 = GenerateBloom();
				b1.position = new Vector3(x * bloomSize, y * bloomSize, 0);
				b1.SetParent(parentObject.transform);

				if(y > 0){
					Transform b2 = GenerateBloom();
					b2.position = new Vector3(x * bloomSize, -y * bloomSize, 0);
					b2.SetParent(parentObject.transform);
				}
				if(x > 0 && y > 0){
					Transform b3 = GenerateBloom();
					b3.position = new Vector3(-x * bloomSize, -y * bloomSize, 0);
					b3.SetParent(parentObject.transform);
				}
				if(x > 0){
					Transform b4 = GenerateBloom();
					b4.position = new Vector3(-x * bloomSize, y * bloomSize, 0);
					b4.SetParent(parentObject.transform);
				}

			}
		}


	}

	Transform GenerateBloom(){
		GameObject go = new GameObject("Bloom");
		Transform o1 = GenerateOctagon(OctType.Oct1);
		o1.SetParent(go.transform);
		o1.position = new Vector3(octSize, octSize);
		Transform o2 = GenerateOctagon(OctType.Oct2);
		o2.SetParent(go.transform);
		o2.position = new Vector3(octSize, -octSize);
		Transform o3 = GenerateOctagon(OctType.Oct3);
		o3.SetParent(go.transform);
		o3.position = new Vector3(-octSize, -octSize);
		Transform o4 = GenerateOctagon(OctType.Oct4);
		o4.SetParent(go.transform);
		o4.position = new Vector3(-octSize, octSize);

		return go.transform;
	}

	Transform GenerateOctagon(OctType t){
		GameObject go = new GameObject(t.ToString());

		MeshFilter meshFilter = (MeshFilter)go.AddComponent(typeof(MeshFilter));
		meshFilter.mesh = GenerateMesh();
		MeshRenderer renderer = (MeshRenderer) go.AddComponent(typeof(MeshRenderer));
		
		renderer.material = manager.KaleidoscopeMat;
		renderer.material.mainTexture = manager.KaleidoscopeTexture;

		switch(t){
		case OctType.Oct1:
			break;
		case OctType.Oct2:
			go.transform.eulerAngles = new Vector3(0,180,180);
			go.transform.localScale = new Vector3(1,1,-1);
			break;
		case OctType.Oct3:
			go.transform.eulerAngles = new Vector3(0,0,180);
			break;
		case OctType.Oct4:
			go.transform.eulerAngles = new Vector3(0,180,0);
			go.transform.localScale = new Vector3(1,1,-1);
			break;
		}

		go.transform.parent = parentObject.transform;
		return go.transform;
	}

	Mesh GenerateMesh(){
		Mesh m = new Mesh();
		m.name = "Octagon";

		// octOffset = octSize;
		float octOffset = octSize /2;

//		Debug.Log(octOffset.ToString());



		m.vertices = new Vector3[]{
			Vector3.zero,
			new Vector3(octOffset, octSize, 0),
			new Vector3(octSize, octOffset , 0),
			new Vector3(octSize, -octOffset, 0),
			new Vector3(octOffset, -octSize, 0),
			new Vector3(-octOffset, -octSize, 0),
			new Vector3(-octSize, -octOffset, 0),
			new Vector3(-octSize,octOffset, 0),
			new Vector3(-octOffset, octSize, 0),
		};


		float size1 = .333f;
		float size2 = .666f;
		float size3 = 1;
		
		m.uv = new Vector2[]{
			new Vector2(.5f, .5f),
			new Vector2(size2, size3),
			new Vector2(size3, size2),
			new Vector2( size3, size1),
			new Vector2( size2, 0),
			new Vector2( size1, 0),
			new Vector2( 0, size1),
			new Vector2( 0, size2),
			new Vector2( size1, size3),
		};
	
		m.triangles = new int[]{
			0,1,2,
			0,2,3,
			0,3,4,
			0,4,5,
			0,5,6,
			0,6,7,
			0,7,8,
			0,8,1,
		};
	
		m.RecalculateNormals();
		


		return m;
	}
	

}
