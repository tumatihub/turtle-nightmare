using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowShark : MonoBehaviour {
    public enum States {PATROLLING, FOLLOWING, ATTACKING}
    public States state = States.PATROLLING;
    public float speed=3;
    public bool movingRight = true;
    public GameObject aggroBox;
    public AudioSource audioSource;
    public AudioClip attackClip;
    private GameObject player;
    private PlayerStateController playerScript;
    private AggroBox aggroScript;
    private float height;
    private Animator anima;
   
    
   
	// Use this for initialization
	void Start () {
        audioSource = GetComponent<AudioSource>();
        aggroScript = aggroBox.GetComponent<AggroBox>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<PlayerStateController>();
        height = transform.position.y;
        anima = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
       

    }
    private void FixedUpdate() {
        switch (state)
        {
            case States.PATROLLING:
                if (aggroScript.aggro == true && playerScript.isHidden == false)
                {
                    state = States.FOLLOWING;
                    break;
                }
                anima.SetBool("Attacking", false);
                //checa se tem algum objeto em frente ou abismo e acerta a direção. 
                if (this.GetComponentInChildren<ObstacleDetection>().obstacle == true)
                {
                    movingRight = !movingRight;

                }
                if (movingRight == false)
                {
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);

                }
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                break;

            case States.FOLLOWING:
                if (player.transform.position.x > transform.position.x - 1f && player.transform.position.x < transform.position.x + 1f)
                {
                    if (playerScript.dead == false)
                    {
                        state = States.ATTACKING;
                        return;
                    }
                }
                if (!aggroScript.aggro)
                {
                    state = States.PATROLLING;
                    return;
                }
                anima.SetBool("Attacking", false);
                if (player.transform.position.x > transform.position.x)
                {
                    movingRight = true;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
                if (player.transform.position.x < transform.position.x)
                {
                    movingRight = false;
                    transform.eulerAngles = new Vector3(0, -180, 0);
                }
                transform.Translate(Vector2.right * (speed * 2f) * Time.deltaTime);
                break;

            case States.ATTACKING:
                anima.SetBool("Attacking", true);
                break;
        }  
    }

    private void AttackEnd()
    {
        anima.SetBool("Attacking", false);
        state = States.FOLLOWING;
    }

    private void PlayAttack()
    {
        audioSource.clip = attackClip;
        audioSource.Play();
    }
}

