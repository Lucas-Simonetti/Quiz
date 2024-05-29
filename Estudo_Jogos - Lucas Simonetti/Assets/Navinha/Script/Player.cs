using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instancia;

    [Header("Componentes")]
    public Rigidbody2D corpoPlayer;
    public BoxCollider2D colisorPlayer;
    public Animator animatorPlayer;

    [Header("Movimentação")]
    public float inputX;
    public float inputY;
    public float velocidade;
    public bool podeMoverX;
    public bool podeMoverY;

    [Header("Atirar")]
    public float inputTiro;
    public bool podeAtirar;
    public float taxaTiro;
    public GameObject tiroPlayer;
    public Transform miraPlayer;

    [Header("PowerUp")]
    public GameObject powerUp;
    public int tempoPowerUp = 10;


    private void Awake()
    {
        instancia = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (podeMoverX)
        {
            inputX = Input.GetAxis("Horizontal");
        }
        if (podeMoverY)
        {
            inputY = Input.GetAxis("Vertical");
        }

        inputTiro = Input.GetAxis("Fire1");

        if (inputTiro != 0)
        {
            Atirar();
        }

        if (inputX != 0)
        {
            animatorPlayer.SetInteger("Player", 1);

            if(inputX > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            animatorPlayer.SetInteger("Player", 0);
        }

    }

    private void FixedUpdate()
    {
        corpoPlayer.velocity = new Vector2(inputX * velocidade, inputY * velocidade);
    }

    public void Atirar()
    {
        if (podeAtirar)
        {
            StartCoroutine(AtirarArjan());
        }
    }

    IEnumerator AtirarArjan()
    {

        podeAtirar = false;
        Instantiate(tiroPlayer, miraPlayer.position, Quaternion.identity);
        yield return new WaitForSeconds (taxaTiro);
        podeAtirar = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Alan"))
        {
            GameManager.instancia.gameOver = true;
        }
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            StartCoroutine(PowerUp());
            powerUp.SetActive(false);
        }
    }

    IEnumerator PowerUp()
    {
        taxaTiro = taxaTiro / 2;
        yield return new WaitForSeconds(tempoPowerUp);
        taxaTiro = taxaTiro * 2;
    }
}
