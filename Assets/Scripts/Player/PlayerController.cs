using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float cubeScaleSpeed = 0.2f;
    [SerializeField] private TrailRenderer trail;

    private PlayerPathFollower pathFollower;
    private float lastMouseY;
    private float maxScale;
    private float minScale = 0.5f;

    [SerializeField] private GameObject cube;
    private Material material;
    private Vector2 startOutlineSize;

    private void OnEnable()
    {
        Events.OnGameStart.AddListener(OnGameStart);
    }

    private void OnDestroy()
    {
        Events.OnGameStart.RemoveListener(OnGameStart);
    }

    private void Start()
    {
        material = cube.GetComponent<MeshRenderer>().material;
        startOutlineSize = new Vector2(material.GetFloat("_Width"), material.GetFloat("_Height"));
        trail.emitting = false;
        pathFollower = GetComponent<PlayerPathFollower>(); //! Awake() is too early for this

        //! Set the initial position and scale of the player
        maxScale = GameManager.Instance.currentLevel.maxScale;
        float avgScale = (maxScale + minScale) / 2;
        transform.localScale = new Vector3(avgScale, avgScale, transform.localScale.z);
        transform.position = new Vector3(0, 0, GameManager.Instance.currentLevel.playerStartPositionZ);
    }

    private void OnGameStart()
    {
        pathFollower.SetFirstDistance();
        trail.emitting = true;
    }

    private void Update()
    {
        if (GameManager.Instance.gameStatus != GameManager.GameStatus.PLAY) return;

        pathFollower.FollowPath();
        CheckInput();
    }

    /// <summary>
    /// Checks the input of the player.
    /// Scale the player if the mouse is moved.
    /// </summary>
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
            material.SetFloat("_Width", scalingY / 10f);
            material.SetFloat("_Height", scalingX / 10f);
            lastMouseY = Input.mousePosition.y;
        }
    }

    /// <summary>
    /// Triggered when the player hits the obstacle.
    /// </summary>
    public void OnObstacleNormal(Vector2 requiredPlayerScale)
    {
        float scaleX = transform.localScale.x;
        float scaleY = transform.localScale.y;
        if (scaleX > requiredPlayerScale.x || scaleY > requiredPlayerScale.y) GameManager.Instance.Fail();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish")) GameManager.Instance.Success();
    }
}
