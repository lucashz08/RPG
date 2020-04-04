using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class hitEnemy : MonoBehaviour
{
    _GameController             gControl;
    private playerScript        playerScript;

    private SpriteRenderer      sRender;
    public bool                 lookleft, playerLeft;

    public Transform            groundCheck;
    public LayerMask            isGrounded;
    // Kock Back

    public GameObject           knockForce;    // força de repulçao
    public Transform            knockPosition; //ponto de origem da força
    public float                knockX;
    private float               kx;

    public int                  enemyHP, actuallyHP;
    private float               hpBarPorcent;
    public GameObject           hpBar; // objeto contendo todas as barras
    public Transform            hpBarLife; // objeto indicador da quantidade de vida
    public GameObject           damageText; // objeto que ira exibir o dano tomado


    public Color[]              characterColor; // controle de cor do personagem
    public bool                 getHit;
    private bool                isDead;

    // configuração de loot

    public GameObject           drop;

    //animação do personagem

    Animator enemy; 

    void Start()
    {
        // inicialização de objetos

        enemy = GetComponent<Animator>();

        gControl = FindObjectOfType(typeof(_GameController)) as _GameController;
        playerScript = FindObjectOfType(typeof(playerScript)) as playerScript;

        sRender = GetComponent<SpriteRenderer>();



        hpBar.SetActive(false);
        actuallyHP = enemyHP;
        hpBarLife.localScale = new Vector3(1, 1, 1);


        characterColor[0] = sRender.color;

        if (lookleft == true)
        {

            float x = transform.localScale.x;
            x *= -1; // inverte o sinal do escale x

            transform.localScale = new Vector3(x, transform.localScale.y, transform.localScale.z);

        }
    }

    private void FixedUpdate()
    {

        float xPlayer = playerScript.transform.position.x;

        if (xPlayer < transform.position.x)
        {
            playerLeft = true;
        }
        if (xPlayer > transform.position.x)
        {
            playerLeft = false;         
        }

        if (lookleft == true && playerLeft == true)
        {
            kx = knockX;
        }
        else if (lookleft == false && playerLeft == true)
        {
            kx = knockX * -1;
        }
        else if (lookleft == true && playerLeft == false)
        {
            kx = knockX * -1;
        }
        else if (lookleft == false && playerLeft == false)
        {
            kx = knockX;
        }
        knockPosition.localPosition = new Vector3(kx, knockPosition.localPosition.y, 0);

        if (lookleft == false)
        {
            hpBar.transform.localScale = new Vector3(1, hpBar.transform.localScale.y, hpBar.transform.localScale.z);

        }
        else
        {
            hpBar.transform.localScale = new Vector3(-1, hpBar.transform.localScale.y, hpBar.transform.localScale.z);
        }

    }
    // Update is called once per frame
    void Update()
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(isDead == true)
            return;


        switch (collision.gameObject.tag)
        {
            case "Gun":
                if (getHit == false && isDead == false)
                {
                    getHit = true;
                    hpBar.SetActive(true);

                    int damageHit = Random.Range( collision.gameObject.GetComponent<gunsInfo>().damageMin, collision.gameObject.GetComponent<gunsInfo>().damageMax + 1);

                    GameObject  effectHitTemp = Instantiate(gControl.effectsHit[0], transform.position, transform.localRotation);
                  
                    Destroy(effectHitTemp, 1f);

                    //mostrar  barra de hp

                    actuallyHP -= Mathf.RoundToInt(damageHit);
                    hpBarPorcent = (float)actuallyHP / (float)enemyHP;

                    if (hpBarPorcent < 0.0f)
                    {
                        hpBarPorcent = 0.0f;
                    }
                    else if (hpBarPorcent > 1.0f)
                    {
                        hpBarPorcent = 1.0f;
                    }

                    hpBarLife.localScale = new Vector3(hpBarPorcent, hpBarLife.localScale.y, hpBarLife.localScale.z);


                    GameObject danoTextTemp = Instantiate(damageText, transform.position, transform.localRotation);
                    danoTextTemp.GetComponentInChildren<TextMeshPro>().text = Mathf.RoundToInt(damageHit).ToString();
                    danoTextTemp.GetComponentInChildren<Rigidbody2D>().AddForce(new Vector2( (playerLeft == true) ? 50 : -50 , 200));
                    danoTextTemp.GetComponentInChildren<MeshRenderer>().sortingLayerName = "HUD";


                    Destroy(danoTextTemp, 0.8f);

                    GameObject knockTemp = Instantiate(knockForce, knockPosition.position, knockPosition.localRotation);
                    Destroy(knockTemp, 0.02f);

                    StartCoroutine("invuneravel");

                    if (actuallyHP <= 0) // adiciona animação de morte e destroi o objeto
                    {
                        isDead = true;
                        enemy.SetInteger("idAnimationEnemy", 3);
                        //Destroy(this.gameObject, 1.3f);
                        

                        StartCoroutine("death");
                    }
                }


                break;
        }
    }

    IEnumerator death()
    {
        yield return new WaitForSeconds(1.3f);
        sRender.enabled = false;
        GameObject deathTemp = Instantiate(gControl.effectDeath, transform.position, transform.localRotation);

        int numberCoins = Random.Range(1,10);

        for(int i = 0; i < numberCoins; i++)
        { 
            GameObject dropTemp = Instantiate(drop, transform.position, transform.localRotation);
            dropTemp.GetComponent<Rigidbody2D>().AddForce(new Vector2( Random.Range( -20f, 20f), 100));

            yield return new WaitForEndOfFrame();
        }

        Destroy(deathTemp, 1f);
        Destroy(this.gameObject);
    }

    IEnumerator invuneravel()
    {
       
        enemy.SetTrigger("getEnemyHit"); // adiciona animação de hit na cena

        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.15f);
        sRender.color = characterColor[0];
        yield return new WaitForSeconds(0.15f);
        sRender.color = characterColor[1];
        yield return new WaitForSeconds(0.15f);
        sRender.color = characterColor[0];
        yield return new WaitForSeconds(0.07f);

        getHit = false;
        hpBar.SetActive(false);
    }
}
