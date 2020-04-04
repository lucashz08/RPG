using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {


    private _GameController gControl;

    // Use this for initialization
    void Start () {

		gControl = FindObjectOfType(typeof(_GameController)) as _GameController;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void getCoin()
    {
        gControl.gold = gControl.gold + 1;
        Destroy(this.gameObject);
    }
}
