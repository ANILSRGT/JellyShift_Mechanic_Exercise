using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObstacleType { Normal }

public class ObstacleController : MonoBehaviour
{
    public ObstacleType obstacleType;
    public Vector2 requiredPlayerScale;

    private void OnCollisionStay(Collision other)
    {
        if (GameManager.Instance.gameStatus != GameManager.GameStatus.PLAY) return;

        if (other.gameObject.CompareTag("Player"))
        {
            if (obstacleType == ObstacleType.Normal) other.gameObject.GetComponent<PlayerController>().OnObstacleNormal(requiredPlayerScale);
        }
    }
}
