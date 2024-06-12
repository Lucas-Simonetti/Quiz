using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using OpenCover.Framework.Model;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instancia;

    [Header("Menu")]
    public GameObject painelInicio;
    public GameObject painelGameplay;
    public GameObject painelGameOver;

    [Header("Gerador de Alan")]
    public GameObject objetoAlan;
    public Transform[] geradoresAlan;
    public float taxaAlan;
    public bool podeAlan = true;

    [Header("Score")]
    public int score;
    public TMP_Text pontuacao;
    public int scoreMax = 20;

    [Header("Game Over")]
    public bool gameOver = false;

    [Header("Gerar Boss")]
    public bool podeBoss = false;
    public GameObject limiteAcima;
    public int vidaBoss;


    private void Awake()
    {
        instancia = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        painelInicio.SetActive(true);
        painelGameplay.SetActive(false);
        painelGameOver.SetActive(false);
        Player.instancia.podeAtirar = false;
        Player.instancia.podeMoverX = false;
        Player.instancia.podeMoverY = false;

    }

    public void IniciarJogo()
    {
        painelInicio.SetActive(false);
        painelGameplay.SetActive(true);
        StartCoroutine(GerarAlan());
        Player.instancia.podeAtirar = true;
        Player.instancia.podeMoverX = true;
        Player.instancia.podeMoverY = true;
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
            }
        }

        if(score == 100)
        {
            podeBoss = true;
            podeAlan = false;
            StopAllCoroutines();
            limiteAcima.SetActive(false);
        }

        if(gameOver == true)
        {
            GameOver();
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
        Player.instancia.podeAtirar = false;
        Player.instancia.podeMoverX = false;
        Player.instancia.podeMoverY = false;
        Player.instancia.velocidade = 0;
        podeAlan = false;
        StopCoroutine(GerarAlan());
        StartCoroutine(FinalizarJogo());
    }

    IEnumerator FinalizarJogo()
    {
        painelGameplay.SetActive(false);
        painelGameOver.SetActive(true);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
