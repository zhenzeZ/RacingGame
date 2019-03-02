using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//use this to change scenes in unity
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour {

	// laps and checkpoints
	public Transform[] checkPoints;
	public static Transform[] checkPointArray;  // get the all checkpoint
	public static int currentCheckpoint = 0;
	public static int currentLap = 0;
	public static int maxLapNumber;
	public static int playerHealth = 20;
    public static bool healing = false;
    public static bool speedUp = false;
    public static bool fastReload = false;
	public static float runSpeed;
	public static int score = 0;

	public int maxLaps;
	private int m_lap;

	public Text lapsText;  // text for laps
    public Text healthText; // text for player health
	public Text winLoseText; // text for win or lose
	public Text reloadText;
	public Text scoreText;

	public float m_speed;                 // How fast the tank moves forward and back.
	public float m_TurnSpeed;            // How fast the tank turns in degrees per second.
	private float m_keyPressTimer = 0;

	public Transform m_shotSpawn;		// The shell spawn position
	public GameObject m_shell;			// The shell object
    public GameObject explosion;
    public float m_fireRate;			// The time to reload shell

	private Rigidbody rb;              // Reference used to move the tank.
	private float m_MovementInputValue;         // The current value of the movement input.
	private float m_TurnInputValue;             // The current value of the turn input.

	private float m_nextFire;
	//public int m_health = 100;
	private Slider health;
	private Slider reloading;
	private Slider speed;

	private bool m_tankDestroyed = false;
	private float m_gameoverTimer = 2.0f;

    private float m_speedUpTimer;
    private float m_fastReloadTimer;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
		maxLapNumber = maxLaps;

		health = GameObject.Find ("HealthBar").GetComponent<Slider> ();
		reloading = GameObject.Find ("ReloadBar").GetComponent<Slider> ();
		speed = GameObject.Find ("SpeedBar").GetComponent<Slider> ();
	}
	
	// Update is called once per frame
	void Update () {
		runSpeed = m_speed;
		checkPointArray = checkPoints;
		m_lap = currentLap;

		lapsText.text = "Laps : " + m_lap + " / " + maxLaps;
		scoreText.text = "Score : " + score;
        
		// send the value to UI
        if (playerHealth > 100 && !m_tankDestroyed)
        {
            playerHealth = 100;
        }
        else if (playerHealth <= 0 && !m_tankDestroyed)
        {
			m_tankDestroyed = true;
            Instantiate(explosion, transform.position, transform.rotation);
        }
		health.value = playerHealth;
		healthText.text = "Health : " + playerHealth + " / 100"; 

        // calculate the item effect timer
        if (m_speedUpTimer > 0)
        {
            m_speedUpTimer -= Time.deltaTime;

            if (m_speedUpTimer <= 0)
            {
                m_speed /= 2;
            }
        }

        if (m_fastReloadTimer > 0)
        {
            m_fastReloadTimer -= Time.deltaTime;

            if (m_fastReloadTimer <= 0)
            {
                m_fireRate *= 2;
				Debug.Log("time up");
            }
        }

		gameOver ();
	}

	void gameOver()
	{
		if (m_tankDestroyed) {
			winLoseText.text = "DESTROYED!";

            m_gameoverTimer -= Time.deltaTime;
            Debug.Log(m_gameoverTimer);
			if (m_gameoverTimer <= 0.0f) {
                Debug.Log("to game over");
                SceneManager.LoadScene(sceneName:"EndScreen");
			}
		}

		if (m_lap >= maxLaps) {
			winLoseText.text = "Finish!";
			m_gameoverTimer -= Time.deltaTime;

			if (m_gameoverTimer <= 0.0f) {
				SceneManager.LoadScene (sceneName: "EndScreen");
			}
		}
	}

	void FixedUpdate(){

       
		float moveHorizontal = Input.GetAxis("Vertical");
		float turnVertical = Input.GetAxis("Horizontal");

		if (moveHorizontal != 0 && m_keyPressTimer < 1.0f) {
			m_keyPressTimer += Time.deltaTime;
		} else if (moveHorizontal == 0 && m_keyPressTimer > 0f) {
			m_keyPressTimer -= Time.deltaTime;
		}

		speed.value = m_keyPressTimer;

		// Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
		Vector3 movement = transform.forward * moveHorizontal * (m_speed * m_keyPressTimer) * Time.deltaTime;

		// Determine the number of degrees to be turned based on the input, speed and time between frames.
		float turn = turnVertical * m_TurnSpeed * Time.deltaTime;

		// Make this into a rotation in the y axis.
		Quaternion turnRotation = Quaternion.Euler (0f, turn, 0f);

        if (!m_tankDestroyed)
        {
            // Apply this rotation to the rigidbody's rotation.
            rb.MoveRotation(rb.rotation * turnRotation);

            // Apply this movement to the rigidbody's position.
            rb.MovePosition(rb.position + movement);

            fire();
        }

        
        itemEffect();
	}

    // restore the player heal, speed up and reduce the reload time
    void itemEffect()
    {
        if (healing)
        {
            playerHealth += 20;
            healing = false;
        }

        if (speedUp)
        {
            m_speed *= 2;
            m_speedUpTimer = 1.5f;
            speedUp = false;
        }

        if (fastReload)
        {
            m_fireRate /= 2;
            m_fastReloadTimer = 3.0f;
            fastReload = false;
        }
    }

	void fire()
	{
		if (Input.GetButton("Fire1") && Time.time > m_nextFire) {
			Debug.Log ("Pressed primary button.");

			m_nextFire = Time.time + m_fireRate;

			// create shell object
			Instantiate(m_shell, m_shotSpawn.position, m_shotSpawn.rotation);
		}

		if (Time.time < m_nextFire) {
			reloading.value = 3.0f - (m_nextFire - Time.time);
			reloadText.text = "Reloading!";
		} else {
			reloadText.text = "READY!";
		}
	}

}
