using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Player jogador1;
    public Player jogador2;
    private Player jogadorAtual;

    public TMP_Text textoTurno;
    public TMP_Text textoDado;
    public TMP_Text textoPontosJogador1;
    public TMP_Text textoPontosJogador2;

    public Button botaoJogar;
    public GameObject painelVitoria;
    public TMP_Text textoVitoria;

    private bool jogoFinalizado = false;

    void Start()
    {
        jogadorAtual = jogador1;
        AtualizarTextoTurno();
        AtualizarTextoPontos();
        textoDado.text = "Dado: ?";
        painelVitoria.SetActive(false);
    }

    public void JogarDado()
    {
        if (jogoFinalizado) return;

        int resultado = Random.Range(1, 7);
        textoDado.text = "Dado: " + resultado;

        jogadorAtual.Mover(resultado);
        AtualizarTextoPontos();

        if (jogadorAtual.pontos >= 20)
        {
            textoVitoria.text = jogadorAtual == jogador1 ? "Jogador 1 venceu!" : "Jogador 2 venceu!";
            painelVitoria.SetActive(true);
            jogoFinalizado = true;
            botaoJogar.interactable = false;
            return;
        }

        StartCoroutine(TrocarTurnoComDelay());
    }

    IEnumerator TrocarTurnoComDelay()
    {
        yield return new WaitForSeconds(1f);

        jogadorAtual = (jogadorAtual == jogador1) ? jogador2 : jogador1;
        AtualizarTextoTurno();

        if (jogadorAtual == jogador2)
        {
            yield return new WaitForSeconds(1f);
            JogarDado();
        }
    }

    void AtualizarTextoTurno()
    {
        textoTurno.text = jogadorAtual == jogador1 ? "Vez do Jogador 1" : "Vez do Jogador 2 (IA)";
    }

    void AtualizarTextoPontos()
    {
        textoPontosJogador1.text = "Pontos Jogador 1: " + jogador1.pontos;
        textoPontosJogador2.text = "Pontos Jogador 2: " + jogador2.pontos;
    }

    public void ReiniciarJogo()
    {
        jogador1.pontos = 0;
        jogador2.pontos = 0;

        jogador1.transform.position = jogador1.casas[0].position;
        jogador2.transform.position = jogador2.casas[0].position;

        jogadorAtual = jogador1;
        jogoFinalizado = false;
        painelVitoria.SetActive(false);
        botaoJogar.interactable = true;
        textoDado.text = "Dado: ?";
        AtualizarTextoTurno();
        AtualizarTextoPontos();
    }
}
