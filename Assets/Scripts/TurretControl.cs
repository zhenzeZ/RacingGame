using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour {

	public float m_TurnSpeed; 
	public GameObject m_player;

	private Vector3 m_offset;

	// Use this for initialization
	void Start () {
		m_offset = transform.position - m_player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// set the turret follow the tank chassis
		transform.position = m_player.transform.position + m_offset;

	}

	void FixedUpdate(){
		Vector3 mousePos = Input.mousePosition;
		float rotationX = mousePos.y / Screen.height - 0.5f;
		float rotationY = mousePos.x / Screen.width - 0.5f;

		// rotate the turret with mouse on x and y asix
		//Quaternion mouseRotation = Quaternion.Euler (-5.0f * rotationX, 90.0f * rotationY * 2, 0.0f);

		// rotate the turret on y axis
		Quaternion mouseRotation = Quaternion.Euler (0.0f, 90.0f * rotationY * 2, 0.0f);

		// set the turret with tank chassis and mouse 
		transform.rotation = mouseRotation * m_player.transform.rotation;

	}
}
