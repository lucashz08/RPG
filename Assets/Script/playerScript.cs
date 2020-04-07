using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    private _GameController gControl;

    private Animator player;
    private Rigidbody2D playerRb;

    public bool ground;
    public int anime;

    public float speed;  // velocidade do personagem
    public float jump;  // força do pulo
    public bool lookleft; // esquerda ou direito

    public Transform groundCheck;  //verifica a colisão do chão
    public LayerMask isGrounded; // verifica se e chão ou não

    private float h, v;

    public bool attacking; // verifica se o personagem esta efetuando um ataque.


    public Collider2D stand, crounch;  // colisor em pe e abaixado

    private Vector3 dir = Vector3.right;
    public Transform hand;
    public LayerMask interaction;
    public GameObject gObjInteraction;

    //sistema de armas

    public GameObject[] guns;

    // Life

    public float maxLife, nowLife;  // vida maxima e vida atual do player

    // Use this for initialization
    void Start()
    {
        nowLife = 1f;

        gControl = FindObjectOfType(typeof(_GameController)) as _GameController;

        player = GetComponent<Animator>();
        playerRb = GetComponent<Rigidbody2D>();

        foreach (GameObject gObj in guns) // desativa o display de armas
        {

            gObj.SetActive(false);

        }
    }


    void FixedUpdate() // taxa de atualização de 0.02
    {
        if (nowLife < maxLife)
            nowLife += 0.25f;

        playerRb.velocity = new Vector2(h * speed, playerRb.velocity.y);

        ground = Physics2D.OverlapCircle(groundCheck.position, 0.02f, isGrounded); // verifica se esta tocando no chão

        interagir();
    }

    // Update is called once per frame
    void Update()
    {

        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //print(h);

        if (h > 0 && lookleft == true)
        {

            flip();
        }
        else if (h < 0 && lookleft == false)
        {
            flip();
        }

        if (h != 0)
        {
            anime = 1; // andado
        }
        else if (v == -1)
        {
            anime = 2; // abaixado
            h = 0; /// fixed para não andar abaixado


        }
        else
        {
            anime = 0;
        }

        if (Input.GetButtonDown("Fire1") && gObjInteraction == null)
        {

            player.SetTrigger("attack");
        }

        if (Input.GetButtonDown("Fire1") && gObjInteraction != null) // interação com bau e itens
        {
            gObjInteraction.SendMessage("interaction", SendMessageOptions.DontRequireReceiver);




        }


        if (Input.GetButtonDown("Jump") && ground == true)
        {

            playerRb.AddForce(new Vector2(0, jump));

        }



        player.SetBool("grounded", ground);
        player.SetInteger("idAnimation", anime);
        player.SetFloat("speedY", playerRb.velocity.y);

        if (v == -1 && ground == true) // colizor em pe e aguaixado
        {
            crounch.enabled = true;
            stand.enabled = false;

        }
        else
        {
            crounch.enabled = false;
            stand.enabled = true;
        }


    }


    void flip()
    {

        lookleft = !lookleft; // inverte o lavor da variavel boollean

        float x = transform.localScale.x;
        x *= -1; // inverte o sinal do escale x

        transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        dir.x = x;


    }

    public void attack(int atk)
    {
        attacking = atk == 1 ? true : false;

        if (attacking == false)
        {
            guns[2].SetActive(false);
        }
    }

    public void interagir()
    {
        RaycastHit2D hit = Physics2D.Raycast(hand.position, dir, 0.4f, interaction); ;
        Debug.DrawRay(hand.position, dir * 0.4f, Color.red);

        if (hit == true)
        {
            gObjInteraction = hit.collider.gameObject;
        }
        else
        {
            gObjInteraction = null;
        }
    }


    public void armsControl(int id)
    {

        foreach (GameObject gObj in guns)
        {
            gObj.SetActive(false);
        }
        guns[id].SetActive(true);
    }
    // Funçoes para tratar colisores

    public void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Coin":
                 //Destroy(collision.gameObject);
                //gControl.gold += 1; // aumenta a quantidade de moeda acresentando mais 1

                collision.gameObject.SendMessage("getCoin", SendMessageOptions.DontRequireReceiver);
                
                break;
        }
    }

    /*
    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Box") // identifica colisao por tag ao sair
        {
            print("saiu");
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
         if (collision.gameObject.tag == "Box") // identifica colisao, continua colidindo
        {
            print("esta colidindo com o item");
        }
    }


    public void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        
    }
    */

}
