using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chest : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] imgObj;

    public bool chestOpen;
    public GameObject dropChest;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void interaction()
    {
        if (chestOpen == false)
        {
            chestOpen = true;
       

            spriteRenderer.sprite = imgObj[1];

            int numberCoins = Random.Range(1, 10); 

            for (int i = 0; i < numberCoins; i++)
            {
                GameObject dropTemp = Instantiate(dropChest, transform.position, transform.localRotation);
                dropTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-20f, 20f), 100));
             
            }

        }
        else
        {
            chestOpen = false;
            spriteRenderer.sprite = imgObj[0];
        }
        print("Interaction by the Chest");
    }
}
