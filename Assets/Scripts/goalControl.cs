using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class goalControl : MonoBehaviour {

	public Text[] lapTimer;  // timer text
    public Text result;

	private float m_millisecond;
	private float m_second;
	private float m_minute;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		m_millisecond += Time.deltaTime;
		if (m_millisecond >= 1.0f) {
			m_second++;
			m_millisecond -= 1.0f;
		}

		if (m_second >= 60.0f) {
			m_minute++;
			m_second -= 60.0f;
		}

		if (PlayerControl.maxLapNumber - 1 >= PlayerControl.currentLap) {
			lapTimer [PlayerControl.currentLap].text = "Lap" + PlayerControl.currentLap + " : " + m_minute + " : " + m_second + " : " + (int)(m_millisecond * 100);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" || other.tag == "Enemy") {
			Debug.Log (other.tag);
		}

		if (PlayerControl.currentCheckpoint >= PlayerControl.checkPointArray.Length) {
			PlayerControl.currentCheckpoint = 0;
			PlayerControl.currentLap++;
            if (PlayerControl.currentLap == PlayerControl.maxLapNumber)
            {
                result.text = "Time Spend: " + m_minute + " : " + m_second + " : " + (int)(m_millisecond * 100);
            }
		}
	}
}
