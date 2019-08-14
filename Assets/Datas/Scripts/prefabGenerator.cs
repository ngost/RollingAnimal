using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabGenerator : MonoBehaviour {
	GameObject cube_prefab;
	Vector3 first_pos = new Vector3(0f,0f,0f);
	Vector3[] create_pos;
	GameObject[] cubes;
	int cube_num = 100;
	// Use this for initialization
	void Start () {
		initCubePos ();
		createCube ();
		cubes = new GameObject[cube_num];
	}


	// Update is called once per frame
	void Update () {
		
	}

	void initCubePos(){
		create_pos = new Vector3[cube_num];
		int width = (int)Mathf.Sqrt (cube_num);
		int height = width;
		int pos = 0;
		for(int i=0; i<width; i++){
			for(int j=0; j<height; j++){
				float x = i * 6f;
				float z = j * 6f;
				create_pos [pos] = new Vector3 (x, 0.5f, z);
				pos++;
			}	
		}
	}

	void createCube(){
        return;
//		cube_prefab = (GameObject) Resources.Load ("cube/cube_obj");

		for (int i = 0; i < cube_num; i++) {
			Debug.Log ("generated : "+i);
			Debug.Log ("position : "+create_pos[i]);
			Instantiate (cube_prefab, create_pos[i], Quaternion.identity);	

		}	
	}
}
