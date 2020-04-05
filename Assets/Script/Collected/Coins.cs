using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour {


    private _GameController gControl;

    int incrementCoin;
    int maxCoin;
    // Use this for initialization
    void Start () {

		gControl = FindObjectOfType(typeof(_GameController)) as _GameController;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void getCoin()
    {
        maxCoin = 2000000000;

        incrementCoin = 1;


        if ((gControl.gold + incrementCoin) < maxCoin)
        {
            gControl.gold = gControl.gold + incrementCoin;
            Destroy(this.gameObject);
        }
        else
        {
            gControl.gold = maxCoin;
            Destroy(this.gameObject);
        }
    }
}
