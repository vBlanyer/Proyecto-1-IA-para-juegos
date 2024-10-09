using System.Collections.Generic;
using UnityEngine;

public class Align : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxAngularAcceleration;
    public float maxRotation;
    private Animator anim;

    public float targetRadius;
    public float slowRadius;
    public float timeToTarget;

    protected virtual void Start()
    {
        maxAngularAcceleration = 100.0f;
        maxRotation = 100.0f;
        targetRadius = 0.5f;
        slowRadius = 2.5f;
        timeToTarget = 0.1f;

        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        SteeringOutput steering = GetSteering();
        ApplySteering(steering, Time.deltaTime);

        anim.SetFloat("Velx", character.velocity.x);
        anim.SetFloat("Vely", character.velocity.z);
    }

    protected virtual SteeringOutput GetSteering()
    {
        SteeringOutput result = new SteeringOutput();
        float targetRotation = 0f;

        float rotation = target.orientation - character.orientation;

        rotation = mapToRange(rotation);

        float rotationSize = Mathf.Abs(rotation);

        if (rotationSize < targetRadius)
        {
            return new SteeringOutput();
        }

        if (rotationSize > slowRadius)
        {
            targetRotation = maxRotation;
        }
        else
        {
            targetRotation = maxRotation * (rotationSize / slowRadius);
        }

        targetRotation *= Mathf.Sign(rotation); 

        result.angular = targetRotation - character.rotation;
        result.angular /= timeToTarget;

        float angularAcceleration = Mathf.Abs(result.angular);
        if (angularAcceleration > maxAngularAcceleration)
        {
            result.angular /= angularAcceleration; 
            result.angular *= maxAngularAcceleration;
        }

        result.linear = Vector3.zero;
        return result;
    }
    
    private void ApplySteering(SteeringOutput steering, float deltaTime)
    {
        if (steering.angular != 0)
        {
            character.rotation += steering.angular;
        }
    }

    private float mapToRange(float angle)
    {
        angle = angle % 360;
        if (angle > 180) angle -= 360;
        else if (angle < -180) angle += 360;
        return angle;
    }
}
