using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour {

	public GameObject m_player;
	public float zoomInTime;

	private Vector3 offset; // the distance of camera and player
	private float zoomInRate;

	// Use this for initialization
	void Start () {
		offset = transform.position - m_player.transform.position;
		zoomInRate = 1.0f;
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = player.transform.position + offset;
		//transform.rotation = player.transform.rotation;



		if (Input.GetMouseButtonDown (1) && zoomInRate >= 0.5f) {
			zoomInRate -= 0.2f * Time.time;
			Debug.Log ("Pressed secondary button.");

		} else if (Input.GetMouseButtonUp (1)) {
			zoomInRate = 1.0f;
			Debug.Log ("Released secondary button.");
		}

		//transform.position = player.transform.position + (offset * zoomInRate);
	}
}
