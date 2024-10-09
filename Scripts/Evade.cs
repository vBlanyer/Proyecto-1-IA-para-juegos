using System.Collections.Generic;
using UnityEngine;

public class Evade : Flee
{
    public float maxPrediction;
    public Kinematic pursueTarget;

    protected override void Start()
    {
        base.Start();
        maxPrediction = 1.0f;
    }

    protected override SteeringOutput GetSteering()
    {   
        Kinematic explicitTarget = new Kinematic();
        float distance, speed, prediction;

        Vector3 direction = pursueTarget.position - character.position;

        distance = direction.magnitude;

        speed = character.velocity.magnitude;

        if (speed <= distance / maxPrediction)
        {
            prediction = maxPrediction;
        } else {
            prediction = distance / speed;
        }

        explicitTarget.position = pursueTarget.position + pursueTarget.velocity * prediction;
        base.target = explicitTarget;

        return base.GetSteering();
    }
}
