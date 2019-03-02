using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControl : MonoBehaviour {

    private int m_itemEffect;
    private bool m_alive;

	// Use this for initialization
	void Start () {
        m_itemEffect = 1;
        m_alive = true;

    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

        m_itemEffect = Random.Range(1, 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            // 1 for heal the player, 2 for speed up, 3 for fast reload
            switch(m_itemEffect)
            {
                case 1:
                    PlayerControl.healing = true;
                    break;
                case 2:
                    PlayerControl.speedUp = true;
                    break;
                case 3:
                    PlayerControl.fastReload = true;
                    break;
            }
			PlayerControl.score += 100;
			Destroy (gameObject);
        }
    }
}
