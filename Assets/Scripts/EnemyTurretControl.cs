using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretControl : MonoBehaviour {

    public float m_maxAngle = 45.0f;
    public GameObject m_tank;
    public GameObject m_player;

	public Transform m_shotSpawn;		// The shell spawn position
	public GameObject m_shell;			// The shell object
	public float m_fireRate;			// The time to reload shell
	private float m_nextFire;

    private Vector3 m_offset;

    // Use this for initialization
    void Start()
    {
        m_offset = transform.position - m_tank.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // set the turret follow the tank chassis
        transform.position = m_tank.transform.position + m_offset;

    }

    void FixedUpdate()
    {
		if (Vector3.Distance(transform.position, m_player.transform.position) < 20.0f)
		{
		
		// get the angle between player and enemy tank
        Vector3 relativeVector = transform.InverseTransformPoint(m_player.transform.position);
		float newSteer = (relativeVector.x / relativeVector.magnitude) * m_maxAngle;

        // rotate the turret on y axis
        Quaternion targetRotation = Quaternion.Euler(0.0f, newSteer, 0.0f);

        // set the turret with tank chassis and mouse 
        transform.rotation = targetRotation * m_tank.transform.rotation;

		}

		if (Vector3.Distance(transform.position, m_player.transform.position) < 15.0f && Time.time > m_nextFire) {
			
			m_nextFire = Time.time + m_fireRate;

			// create shell object
			Instantiate(m_shell, m_shotSpawn.position, m_shotSpawn.rotation);
		}
	}
}
