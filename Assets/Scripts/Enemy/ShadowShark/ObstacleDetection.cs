using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDetection : MonoBehaviour {
  //public GameObject shark;
  //private ShadowShark sharkscript;
    public float groundDistance=2f;
    public float wallDistance = 0.1f;
    public bool obstacle = false;
    public LayerMask Platform;
   
    

    // Use this for initialization
    void Start () {
    
	}
	
	// Update is called once per frame
	void Update () {
        //verifica com os Raycast se pode continuar o caminho.
        RaycastHit2D groundInfo = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, Platform);
        RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, Vector2.right, wallDistance, Platform);
        RaycastHit2D wallInfo2 = Physics2D.Raycast(transform.position, Vector2.left, wallDistance, Platform);

        if (groundInfo.collider == false||wallInfo.collider == true || wallInfo2.collider == true)
        {
            obstacle = true;
        } else
        {
            obstacle = false;
        }
  

    }
  
}
