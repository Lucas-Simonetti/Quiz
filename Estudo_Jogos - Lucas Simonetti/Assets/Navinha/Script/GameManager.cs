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
    public GameObject painelGameOver;

    [Header("Win")]
    public bool winGame = false;
    public GameObject painelWin;

    [Header("Gerar Boss")]
    public bool podeBoss = false;
    public GameObject limiteAcima;
    public int vidaBoss;

    [Header("Botão")]
    public GameObject restartButton;


    private void Awake()
    {
        instancia = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GerarAlan());
        painelGameOver.SetActive(false);
        painelWin.SetActive(false);
        restartButton.SetActive(false);

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

        if(vidaBoss == 0)
        {
            WinGame();
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
        painelGameOver.SetActive(true);
        Player.instancia.podeAtirar = false;
        Player.instancia.podeMoverX = false;
        Player.instancia.podeMoverY = false;
        Player.instancia.velocidade = 0;
        podeAlan = false;
        StopAllCoroutines();
        restartButton.SetActive(true);
    }

    public void WinGame()
    {
        painelWin.SetActive(true);
        Player.instancia.podeAtirar = false;
        Player.instancia.podeMoverX = false;
        Player.instancia.podeMoverY = false;
        Player.instancia.velocidade = 0;
        podeAlan = false;
        StopAllCoroutines();
        restartButton.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
