using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OpenCover.Framework.Model;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    public static GameManager instancia;

    [Header("Gerador de Alan")]
    public GameObject objetoAlan;
    public Transform[] geradoresAlan;
    public float taxaAlan;
    public bool podeAlan = true;

    [Header("PowerUp")]
    public GameObject powerUp;

    [Header("Score")]
    public int score;
    public TMP_Text pontuacao;
    public int scoreMax = 20;

    [Header("Game Over")]
    public bool gameOver = false;
    public GameObject painelGameOver;


    private void Awake()
    {
        instancia = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GerarAlan());
        painelGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        pontuacao.text = score.ToString();
        if(taxaAlan > 0.2)
        {
            if (score > scoreMax)
            {
                taxaAlan -= 0.2f;
                scoreMax += 20;
                powerUp.SetActive(true);
            }
        }
        if(gameOver == true)
        {
            GameOver();
            if (podeAlan == false)
            {
                StopAllCoroutines();
            }

        }
    }

    IEnumerator GerarAlan()
    {
        int rnd = Random.Range(0, geradoresAlan.Length);
        Instantiate(objetoAlan, geradoresAlan[rnd].position , Quaternion.identity);
        yield return new WaitForSeconds(taxaAlan);
        StartCoroutine(GerarAlan());
    }

    public void GameOver()
    {
        painelGameOver.SetActive(true);
        Player.instancia.podeAtirar = false;
        Player.instancia.podeMoverX = false;
        Player.instancia.podeMoverY = false;
        Player.instancia.velocidade = 0;
        podeAlan = false;
    }
}