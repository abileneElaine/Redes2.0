using System.Collections.Generic;
using UnityEngine;

public class BoardPath : MonoBehaviour
{
    public List<Transform> pathPoints; // Marque as casas manualmente no editor

    public Vector3 GetPosition(int index)
    {
        return pathPoints[index].position;
    }
}
