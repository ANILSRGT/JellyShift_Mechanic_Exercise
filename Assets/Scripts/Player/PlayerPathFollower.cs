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
        pathCreator = GameManager.Instance.currentLevel.pathCreator; //! Awake() is too early for this
        pathCreator.GetComponent<RoadMeshCreator>().TriggerUpdate();
    }

    /// <summary>
    /// Takes the initial distance between the player and the road before the game starts.
    /// The nearest waypoint the player needs to start
    /// </summary>
    public void SetFirstDistance()
    {
        if (pathCreator != null)
        {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }

    /// <summary>
    /// The player follows the path.
    /// </summary>
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