using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {

	public Transform path;
    public float m_speed = 10.0f;
    public float m_maxSteerAngle = 45.0f;
    public GameObject explosion;

    private Rigidbody rb;              // Reference used to move the tank.
    private List<Transform> nodes;
	private int currentNode = 0;

	private int m_health = 100;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform> ();
		nodes = new List<Transform> ();
		Debug.Log (pathTransforms.Length);
		for (int i = 0; i < pathTransforms.Length; i++) {
			if (pathTransforms [i] != transform) {
				nodes.Add (pathTransforms [i]);
			}
		}

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (m_health <= 0) {
            Instantiate(explosion, transform.position, transform.rotation);
			PlayerControl.score += 1000;
            Destroy(gameObject);
        }

		Movement ();
        CheckWaypointDistance();
    }

    // control the car movement
	void Movement(){

        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 movement = transform.forward * m_speed * Time.deltaTime;

        // Apply this movement to the rigidbody's position.
        rb.MovePosition(rb.position + movement);

        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
        float newSteer = (relativeVector.x / relativeVector.magnitude) * m_maxSteerAngle;

        // Make this into a rotation in the y axis.
        Quaternion turnRotation = Quaternion.Euler(0f, newSteer, 0f);

        // Apply this rotation to the rigidbody's rotation.
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    // when the distence between car and node point less then 2
    void CheckWaypointDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 2.0f)
        {
            if (currentNode >= nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Shell")
        {
            m_health -= 10;
			PlayerControl.score += 100;
			Debug.Log ("hit");
        }
    }
}
