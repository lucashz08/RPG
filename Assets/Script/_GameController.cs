using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class _GameController : MonoBehaviour {


    public int gold;

    public GameObject[] effectsHit;
    public GameObject effectDeath;

	public TextMeshProUGUI coinText;

	// Use this for initialization
	void Start () {
		
	}

    private void FixedUpdate()
    {
        coinText.text = this.gold.ToString("N0");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
