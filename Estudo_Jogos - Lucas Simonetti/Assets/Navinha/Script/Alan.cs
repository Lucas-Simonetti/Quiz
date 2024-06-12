using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alan : MonoBehaviour
{

    [Header("Componentes")]
    public Rigidbody2D corpoAlan;
    public BoxCollider2D colisorAlan;

    [Header("Movimentação")]
    public float velocidade;

    [Header("Drop")]
    public GameObject powerUp;
    public int chance;

    // Start is called before the first frame update
    void Start()
    {
        Player.instancia.alansAtivos.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(GameManager.instancia.podeAlan == false)
        {
            velocidade = 0;
        }
        corpoAlan.velocity = new Vector2(0, velocidade);
        if (transform.position.y <= -6)
        {
            GameManager.instancia.gameOver = true;
        }
    }

    public void DroparItem()
    {
        int rnd = Random.Range(0, 100);
        if (rnd < chance)
        {
            Instantiate(powerUp, transform.position, Quaternion.identity);
        }
    }
}
