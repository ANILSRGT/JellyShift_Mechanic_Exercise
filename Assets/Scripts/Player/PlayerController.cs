using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float cubeScaleSpeed = 0.2f;

    private PlayerPathFollower pathFollower;
    private float lastMouseY;
    private float maxScale;
    private float minScale = 0.5f;

    private void Start()
    {
        pathFollower = GetComponent<PlayerPathFollower>();
        maxScale = GameManager.Instance.currentLevel.maxScale;
        float avgScale = (maxScale + minScale) / 2;
        transform.localScale = new Vector3(avgScale, avgScale, transform.localScale.z);
    }

    private void Update()
    {
        if (GameManager.Instance.gameStatus != GameManager.GameStatus.PLAY) return;

        pathFollower.FollowPath();
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMouseY = Input.mousePosition.y;
        }
        else if (Input.GetMouseButton(0))
        {
            float mouseY = Input.mousePosition.y;
            float deltaY = Input.mousePosition.y - lastMouseY;
            float scalingX = transform.localScale.x;
            float scalingY = transform.localScale.y;

            if (deltaY > 0f)
            {
                scalingY += cubeScaleSpeed * Time.deltaTime;
                if (scalingY > maxScale) scalingY = maxScale;
            }
            else if (deltaY < 0f)
            {
                scalingY -= cubeScaleSpeed * Time.deltaTime;
                if (scalingY < minScale) scalingY = minScale;
            }

            scalingX = (maxScale + minScale) - scalingY;
            transform.localScale = new Vector3(scalingX, scalingY, transform.localScale.z);
            lastMouseY = Input.mousePosition.y;
        }
    }

    public void OnObstacleNormal(Vector2 requiredPlayerScale)
    {
        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;
        Debug.Log("scaleX: " + scaleX + " scaleY: " + scaleY);
        if (scaleX > requiredPlayerScale.x + 0.06f || scaleY > requiredPlayerScale.y + 0.06f) GameManager.Instance.Fail();
    }
}
