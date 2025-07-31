using UnityEngine;

public class Player : MonoBehaviour
{
    public int pontos = 0;
    public Transform[] casas;
    private int posicaoAtual = 0;

    public void Mover(int passos)
    {
        posicaoAtual += passos;
        if (posicaoAtual >= casas.Length)
        {
            posicaoAtual = casas.Length - 1;
        }

        transform.position = casas[posicaoAtual].position;
        pontos += passos;
    }
}
