using System.Collections;
using UnityEngine;

public class PlayerPiece : MonoBehaviour
{
    public int currentPosition = 0;
    public float moveSpeed = 2f;
    public BoardPath boardPath;

    public IEnumerator MoveSteps(int steps)
    {
        while (steps > 0 && currentPosition < boardPath.pathPoints.Count - 1)
        {
            currentPosition++;
            Vector3 nextPos = boardPath.GetPosition(currentPosition);
            yield return MoveToPosition(nextPos);
            steps--;
        }
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
