using System.Collections;
using System.Collections.Generic;
using PathCreation;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public PathCreator pathCreator;
    public float maxScale = 4;
    public float playerStartPositionZ; //! Player position => Vector3(0, 0, z) - Player only moves in the z-axis
}
