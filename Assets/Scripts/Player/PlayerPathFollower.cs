using System.Collections;
using System.Collections.Generic;
using PathCreation;
using PathCreation.Examples;
using UnityEngine;

public class PlayerPathFollower : MonoBehaviour
{
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;

    private PathCreator pathCreator;
    private float distanceTravelled;

    private void Start()
    {
        pathCreator = GameManager.Instance.currentLevel.pathCreator;
        pathCreator.GetComponent<RoadMeshCreator>().TriggerUpdate();

        if (pathCreator != null)
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }

    public void FollowPath()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }
}