using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro; // importante importar TMP


public class GameManager : MonoBehaviour
{
    public PlayerPiece player1;
    public PlayerPiece player2;
    public BoardPath boardPath;

    public Button rollDiceButton;
    public TMP_Text turnText;
    public TMP_Text diceText;

    private bool isPlayer1Turn = true;
    private bool isRolling = false;

    private void Start()
    {
        player1.boardPath = boardPath;
        player2.boardPath = boardPath;

        rollDiceButton.onClick.AddListener(() => StartCoroutine(PlayerTurn()));
        UpdateUI();
    }

    IEnumerator PlayerTurn()
    {
        if (isRolling) yield break;

        isRolling = true;
        rollDiceButton.interactable = false;

        int diceRoll = Random.Range(1, 7);
        diceText.text = "Dado: " + diceRoll;

        yield return StartCoroutine(player1.MoveSteps(diceRoll));
        yield return new WaitForSeconds(0.5f);

        if (CheckVictory(player1, "Jogador 1")) yield break;

        isPlayer1Turn = false;
        UpdateUI();

        yield return new WaitForSeconds(1f); // Espera antes da IA jogar
        StartCoroutine(IATurn());
    }

    IEnumerator IATurn()
    {
        int diceRoll = Random.Range(1, 7);
        diceText.text = "Dado: " + diceRoll + " (IA)";
        yield return StartCoroutine(player2.MoveSteps(diceRoll));
        yield return new WaitForSeconds(0.5f);

        if (CheckVictory(player2, "Jogador 2 (IA)")) yield break;

        isPlayer1Turn = true;
        UpdateUI();
        rollDiceButton.interactable = true;
        isRolling = false;
    }

    void UpdateUI()
    {
        turnText.text = isPlayer1Turn ? "Turno: Jogador 1" : "Turno: IA";
    }

    bool CheckVictory(PlayerPiece player, string name)
    {
        if (player.currentPosition >= boardPath.pathPoints.Count - 1)
        {
            turnText.text = name + " venceu!";
            diceText.text = "";
            rollDiceButton.interactable = false;
            isRolling = true;
            return true;
        }
        return false;
    }
}
