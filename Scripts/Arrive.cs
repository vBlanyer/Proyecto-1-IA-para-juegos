using System.Collections.Generic;
using UnityEngine;

public class Arrive : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxAcceleration;
    public float maxSpeed;
    private Animator anim;

    public float targetRadius;
    public float slowRadius;
    public float timeToTarget;

    void Start()
    {
        maxAcceleration = 1.0f;
        maxSpeed = 3.0f;
        targetRadius = 2.0f;
        slowRadius = 10.0f;
        timeToTarget = 0.1f;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SteeringOutput steering = GetSteering();
        ApplySteering(steering, Time.deltaTime);

        // Actualiza los parámetros del animador
        anim.SetFloat("Velx", character.velocity.x);
        anim.SetFloat("Vely", character.velocity.z);
    }

    public SteeringOutput GetSteering()
    {
        SteeringOutput result = new SteeringOutput();
        float targetSpeed = 0.0f;

        Vector3 direction = target.position - character.position;
        float distance = direction.magnitude;

        if (distance < targetRadius)
        {
            return new SteeringOutput();
        }

        if (distance > slowRadius)
        {
            targetSpeed = maxSpeed;
        }
        else
        {
            targetSpeed = maxSpeed * ((distance - targetRadius) / slowRadius);
        }

        Vector3 targetVelocity = direction.normalized * targetSpeed;
        result.linear = (targetVelocity - character.velocity) / timeToTarget;

        if (result.linear.magnitude > maxAcceleration)
        {
            result.linear = result.linear.normalized * maxAcceleration;
        }

        result.angular = 0;

        return result;
    }

    void ApplySteering(SteeringOutput steering, float deltaTime)
    {
        // Actualizar la velocidad del personaje
        character.velocity += steering.linear * deltaTime;

        // Limitar la velocidad si excede la máxima permitida
        if (character.velocity.magnitude > maxSpeed)
        {
            character.velocity = character.velocity.normalized * maxSpeed;
        }
    }
}