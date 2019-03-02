using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellControl : MonoBehaviour {

	public float m_launchForce;

	public GameObject explosion;

	private Rigidbody rb;

	private float m_aliveTimer; // the time reduce to 0 destroy the object

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();

		rb.velocity = transform.forward * (PlayerControl.runSpeed * 2 + m_launchForce);

		m_aliveTimer = 5.0f;
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag != "CheckPoint" && other.tag != "Item")
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (other.tag == "Player")
        {
            PlayerControl.playerHealth -= 10;
        }

    }
}
