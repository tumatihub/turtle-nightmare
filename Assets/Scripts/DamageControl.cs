using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageControl : MonoBehaviour {
    public GameObject Player;
    public int damage = 1;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print("Damage1");
            Player.GetComponent<PlayerStateController>().Damaged(damage);
        }
    }
}
