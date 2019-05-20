using UnityEngine;
using System.Collections;

public class Basic : KaleidoscopeDesign {
	enum OctType { Square1, Square2};


	public Basic(KaleidoscopeManager m) {
		manager = m;
		parentObject = new GameObject("Basic");
		for(int i = 1; i < 9; i++) GenerateOctant(i);

	}

	GameObject GenerateOctant(int i){
		OctType octType = OctType.Square1;
		Vector3 rot = Vector3.zero;
		switch(i){
		case 1:
			octType = OctType.Square1;
			rot = new Vector3(0,90,0);
			break;
		case 2:
			octType = OctType.Square2;
			rot = new Vector3(270,90,180);
			break;
		case 3:
			octType = OctType.Square1;
			rot = new Vector3(90,90,0);
			break;
		case 4:
			octType = OctType.Square2;
			rot = new Vector3(0,90,180);
			break;
		case 5:
			octType = OctType.Square1;
			rot = new Vector3(180,90,0);
			break;
		case 6:
			octType = OctType.Square2;
			rot = new Vector3(90,90,180);
			break;
		case 7:
			octType = OctType.Square1;
			rot = new Vector3(270,90,0);
			break;
		case 8:
			octType = OctType.Square2;
			rot = new Vector3(180,90,180);
			break;
		}
		
		GameObject go = new GameObject("Octant " + i);
		MeshFilter meshFilter = (MeshFilter)go.AddComponent(typeof(MeshFilter));
		meshFilter.mesh = GenerateMesh(octType);
		MeshRenderer renderer = (MeshRenderer) go.AddComponent(typeof(MeshRenderer));
		
		renderer.material = manager.KaleidoscopeMat;
		renderer.material.mainTexture = manager.KaleidoscopeTexture;

		
		go.transform.eulerAngles = rot;
		go.transform.parent = parentObject.transform;
		
		return go;
	}
	
	Mesh GenerateMesh(OctType octType){
		Mesh m  = new Mesh();
		m.name = "triangle";
		m.vertices = new Vector3[]{
			Vector3.zero,
			new Vector3(0, manager.squareSize, 0),
			new Vector3(0, manager.squareSize, manager.squareSize)
		};
		
		
		float origin = 0;
		float corner = 1;
		
		m.uv = new Vector2[]{
			new Vector2(origin,origin),
			new Vector2(origin,corner),
			new Vector2(corner,corner),
		};
		
		m.triangles = new int[]{
			0,1,2
		};
		m.RecalculateNormals();
		
		if(octType == OctType.Square2) m.triangles = new int[]{
			2,1,0
		};
		return m;
	}
}
