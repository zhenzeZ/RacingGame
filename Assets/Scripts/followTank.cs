using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followTank : MonoBehaviour {

	public GameObject tank;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = tank.transform.position;
	}
}
