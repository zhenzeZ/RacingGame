using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "Player" || PlayerControl.currentCheckpoint >= PlayerControl.checkPointArray.Length)
			return;

		// check player enter which check point
		if (transform == PlayerControl.checkPointArray [PlayerControl.currentCheckpoint].transform) {
			// check is this last check point
			if (PlayerControl.currentCheckpoint < PlayerControl.checkPointArray.Length) {
				PlayerControl.currentCheckpoint++;
				Debug.Log ("check point: " + PlayerControl.currentCheckpoint);
			} 
		}
	}
}
