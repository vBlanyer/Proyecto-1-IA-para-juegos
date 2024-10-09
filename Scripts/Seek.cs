using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxAcceleration;
    private Animator anim;

    protected virtual void Start()
    {
        maxAcceleration = 4.0f;
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        SteeringOutput steering = GetSteering();
        ApplySteering(steering, Time.deltaTime);

        // Actualiza los parámetros del animador
        anim.SetFloat("Velx", character.velocity.x);
        anim.SetFloat("Vely", character.velocity.z);
    }

    protected virtual SteeringOutput GetSteering()
    {
        SteeringOutput result = new SteeringOutput();

        result.linear = target.position - character.position;

        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;

        return result;
    }

    private void ApplySteering(SteeringOutput steering, float deltaTime)
    {
        // Actualizar la velocidad del personaje
        character.velocity += steering.linear * deltaTime;
        // Limitar la velocidad a un valor máximo si es necesario.
        float maxSpeed = 4.0f; // Define un valor máximo de velocidad, ajústalo según tu necesidad.
        if (character.velocity.magnitude > maxSpeed)
        {
            character.velocity = character.velocity.normalized * maxSpeed;
        }
    }
}