using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public bool isDisguised;
    public bool isAggressive;
    public bool isNaughty;
    public bool GlobalAlert;
    public int suspicionMeter;

    void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitGameState();
	}

    private void InitGameState()
    {
        isDisguised = true;
        isAggressive = false;
        isNaughty = false;
        suspicionMeter = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
