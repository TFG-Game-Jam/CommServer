using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

public class MovePlayer : MonoBehaviour {

	public float speed;
    public float x_input;
	public bool left;
	public bool right;

	// Use this for initialization
	void Start () {
		
	}
	
	

	// Update is called once per frame
	void Update () {

		// GetComponent<Rigidbody> ().position = new Vector3 
		// (
		// 	Mathf.Clamp (GetComponent<Rigidbody> ().position.x, boundary.xMin, boundary.xMax), 
		// 	0.0f, 
		// 	Mathf.Clamp (GetComponent<Rigidbody> ().position.z, boundary.zMin, boundary.zMax)
		// );
	
		
		// StartCoroutine(n.GetOrientation(or));
		// n.GetOrientation(or);

		left = Networking.playerActions.port;
		right = Networking.playerActions.starboard;

		if (left) {
			x_input = -1;
		}
		if (right) {
			x_input = 1;
		}

		// Debug.Log(Networking.playerActions.port);

		Vector3 movement = new Vector3 (x_input, 0.0f, 0.0f);
        GetComponent<Rigidbody> ().velocity = movement * speed;
	}
}
