using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour {
    public GameObject Player;
    public PlayerStateController playerScript;
    public int damage = 1;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerScript = Player.GetComponent<PlayerStateController>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerScript.dead == false)
        {
            print("Damage1");
            Player.GetComponent<PlayerStateController>().Damaged(damage);
        }
    }
}
