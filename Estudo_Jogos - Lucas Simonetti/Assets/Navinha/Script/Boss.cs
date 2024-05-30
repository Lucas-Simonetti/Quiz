using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{

    public static Boss instancia;

    [Header("Componentes")]
    public Rigidbody2D corpoBoss;
    public BoxCollider2D colisorBoss;

    [Header("Movimentação")]
    public float velocidade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (GameManager.instancia.podeBoss == true)
        {
            velocidade = -0.5f;
        }
        corpoBoss.velocity = new Vector2(0, velocidade);
        if (transform.position.y <= -6)
        {
            GameManager.instancia.gameOver = true;
        }
    }
}
