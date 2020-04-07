using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hud : MonoBehaviour {

	private playerScript player;
	public Image[] hp;
	public Sprite half, full;
	// Use this for initialization
	void Start () {
		player = FindObjectOfType(typeof(playerScript)) as playerScript;
	}
	
	// Update is called once per frame
	void Update () {
        controlHP();
    }

    void controlHP()
    {
        float life = (float)player.nowLife / (float)player.maxLife;
   

        //print(life);
        //representa 100 vida
        foreach (Image img in hp)
        {
            img.enabled = true;
            img.sprite = full;
        }

       
        if (life >= 1f)
        {

        }
        else if (life >= 0.9f)
        {
            hp[4].sprite = half;
        }
        else if (life >= 0.8f)
        {
            hp[4].enabled = false;

        }
        else if (life >= 0.7f)
        {
            hp[4].enabled = false;

            hp[3].sprite = half;
        }
        else if (life >= 0.6f)
        {
            hp[4].enabled = false;

            hp[3].enabled = false;
        }
        else if (life >= 0.5f)
        {
            hp[4].enabled = false;
            hp[3].enabled = false;

            hp[2].sprite = half;
        }
        else if (life >= 0.4f)      
        {
            hp[4].enabled = false;
            hp[3].enabled = false;

            hp[2].enabled = false;

        }
        else if (life >= 0.3f)
        {
            hp[4].enabled = false;
            hp[3].enabled = false;
            hp[2].enabled = false;

            hp[1].sprite = half;
        }
        else if (life >= 0.2f)
        {
            hp[4].enabled = false;
            hp[3].enabled = false;
            hp[2].enabled = false;

            hp[1].enabled = false;
        }
        else if (life >= 0.1f)
        {
            hp[4].enabled = false;
            hp[3].enabled = false;
            hp[2].enabled = false;
            hp[1].enabled = false;

            hp[0].sprite = half;
        }
        else if (life >= 0f)
        {
            hp[4].enabled = false;
            hp[3].enabled = false;
            hp[2].enabled = false;
            hp[1].enabled = false;

            hp[0].enabled = false;
        }
        
    }
}
