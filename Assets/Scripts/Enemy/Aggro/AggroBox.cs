using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AggroBox : MonoBehaviour {
    public bool aggro = false;
    public GameObject player;
    public PlayerStateController playerScript;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerStateController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(playerScript.dead == true)
        {
            aggro = false;
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            aggro = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            aggro = false;
        }
    }

}
