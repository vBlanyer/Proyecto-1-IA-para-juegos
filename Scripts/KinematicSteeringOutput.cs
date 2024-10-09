using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSteeringOutput : MonoBehaviour
{
  public Vector3 velocity;
  public float rotation;

  public KinematicSteeringOutput()
  {
    velocity = Vector3.zero;
    rotation = 0.0f;
  }
}
