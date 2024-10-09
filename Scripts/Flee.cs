using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour
{
    public Kinematic character;
    public Kinematic target;
    public float maxAcceleration;
    private Animator anim;

    protected virtual void Start()
    {
        maxAcceleration = 2.0f;
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

        result.linear = character.position - target.position;

        result.linear.Normalize();
        result.linear *= maxAcceleration;

        result.angular = 0;

        return result;
    }

    private void ApplySteering(SteeringOutput steering, float deltaTime)
    {
        // Actualizar la velocidad del personaje
        character.velocity += steering.linear * deltaTime;

        // Establecer limite de distancia entre el personaje y el objetivo
        if (Vector3.Distance(character.position, target.position) > 10.0f)
        {
            character.velocity = Vector3.zero;
        }
    }
}