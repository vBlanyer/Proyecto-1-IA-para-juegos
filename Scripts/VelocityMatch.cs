using System.Collections.Generic;
using UnityEngine;

public class VelocityMatch : MonoBehaviour
{
    public Kinematic character;  
    public Kinematic target;     

    public float maxAcceleration = 10.0f;  
    public float timeToTarget = 0.1f;    

    private Animator anim;  

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SteeringOutput steering = GetSteering();
        ApplySteering(steering, Time.deltaTime);

        if (anim != null)
        {
            anim.SetFloat("Velx", character.velocity.x);
            anim.SetFloat("Vely", character.velocity.z);
        }
    }

    public SteeringOutput GetSteering()
    {
        SteeringOutput result = new SteeringOutput();

        result.linear = (target.velocity - character.velocity) / timeToTarget;

        if (result.linear.magnitude > maxAcceleration)
        {
            result.linear = result.linear.normalized * maxAcceleration;
        }

        result.angular = 0f;

        return result;
    }

    void ApplySteering(SteeringOutput steering, float deltaTime)
    {
        // Actualiza la velocidad del personaje basado en el steering calculado
        character.velocity += steering.linear * deltaTime;

        // Actualiza la posición del personaje usando su nueva velocidad
        character.position += character.velocity * deltaTime;
    }
}
