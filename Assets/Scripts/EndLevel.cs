using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour {
    private GameObject fadeLevel;
    private LevelChanger levelChanger;
    public int levelIndex =1;
	// Use this for initialization
	void Start () {
        fadeLevel = GameObject.Find("FadeLevel");
        levelChanger = fadeLevel.GetComponent<LevelChanger>();
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            levelChanger.FadeToLevel(levelIndex);
        }
    }
}
