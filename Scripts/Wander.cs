using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Face
{
    public float wanderOffset;
    public float wanderRadius;
    public float wanderRate; 
    public float wanderOrientation; 
    public float maxAcceleration; 

    protected override void Start()
    {
        base.Start();

        wanderOffset = 0.5f; 
        wanderRadius = 1.0f; 
        wanderRate = 0.4f; 
        wanderOrientation = 0.0f;
        maxAcceleration = 1.0f;

        if (faceTarget == null)
        {
            faceTarget = new Kinematic();
        }
    }

    protected override void Update()
    {
        base.Update();

        SteeringOutput steering = GetSteering();
        ApplySteering(steering, Time.deltaTime);
    }

    protected override SteeringOutput GetSteering()
    {
        SteeringOutput result = new SteeringOutput();

        wanderOrientation += Random.Range(-wanderRate, wanderRate); 

        faceTarget.orientation = wanderOrientation + character.orientation;

        float characterOrientationRadians = Mathf.Deg2Rad * character.orientation;
        faceTarget.position = character.position + wanderOffset * new Vector3(Mathf.Sin(characterOrientationRadians), 0, Mathf.Cos(characterOrientationRadians));

        float wanderTargetOrientationRadians = Mathf.Deg2Rad * faceTarget.orientation;
        faceTarget.position += wanderRadius * new Vector3(Mathf.Sin(wanderTargetOrientationRadians), 0, Mathf.Cos(wanderTargetOrientationRadians));

        result = base.GetSteering();

        Vector3 direction = (faceTarget.position - character.position).normalized;
        result.linear = maxAcceleration * direction;

        return result;
    }

    private void ApplySteering(SteeringOutput steering, float deltaTime)
    {
        character.velocity += steering.linear * deltaTime;

        float maxSpeed = 1.0f;
        if (character.velocity.magnitude > maxSpeed)
        {
            character.velocity = character.velocity.normalized * maxSpeed;
        }
    }
}
