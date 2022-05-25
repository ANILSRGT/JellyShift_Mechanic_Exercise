using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    private Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (GameManager.Instance.gameStatus != GameManager.GameStatus.PLAY) return;

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
    }

    private void Start()
    {
        offset = transform.position - target.position;
    }
}
