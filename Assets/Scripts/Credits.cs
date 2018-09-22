using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour {
    public GameObject fadetoLevel;
    private LevelChanger levelChanger;

	// Use this for initialization
	void Start () {
        levelChanger = fadetoLevel.GetComponent<LevelChanger>();
	}
	
	public void Restart()
    {
        levelChanger.FadeToLevel(0);
    }
}
