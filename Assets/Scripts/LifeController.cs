using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour {
    public static LifeController  control;
    public int health = 5;
    public Vector2 currentPosition;
    public Vector2 initialPosition;
    public Canvas hud;
    public Image[] lifes;
    public GameObject fadeLevel;
    public LevelChanger levelChanger;
    


    // Use this for initialization
    private void Awake()
    {
        //if(control == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //    control = this;
        //}
        //else if(control != this)
        //{
        //    Destroy(gameObject);
        //}
    }
    void Start () {
        //hud = GameObject.Find("HUD");
        UpdateHUD();
        fadeLevel = GameObject.Find("FadeLevel");
        levelChanger = fadeLevel.GetComponent<LevelChanger>();
        health = 5;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnDamage(int damage)
    {
        health = health - damage;
        UpdateHUD();
    }

    public void OnlifeGain(int life)
    {
        health = health + life;
    }

    public void UpdateHUD()
    {
        for (var i = 0; i < lifes.Length; i++)
        {
            if (i < health)
            {
                lifes[i].enabled = true;
            }
            else
            {
                lifes[i].enabled = false;
            }
        }
    }

    public void GameOver()
    {
        Cursor.visible = true;
        levelChanger.FadeToLevel(4);
        
    }

}
