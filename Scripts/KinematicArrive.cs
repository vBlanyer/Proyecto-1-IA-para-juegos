using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicArrive : MonoBehaviour
{
    public Static character;
    public Static target;
    public float maxSpeed;

    public float radius;

    public float timeToTarget = 0.25f;
    private Animator anim;

    void Start()
    {
        maxSpeed = 2f;
        radius = 3f;
        character = gameObject.AddComponent<Static>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        KinematicSteeringOutput steering = GetSteering();

        character.position += steering.velocity * Time.deltaTime;
        anim.SetFloat("Velx", steering.velocity.x);
        anim.SetFloat("Vely", steering.velocity.z);
    }

    public KinematicSteeringOutput GetSteering()
    {
        KinematicSteeringOutput result = new KinematicSteeringOutput();
        result.velocity = Vector3.zero;
        result.rotation = 0f;

        result.velocity = target.position - character.position;

        if (result.velocity.magnitude < radius)
        {
            result.velocity = Vector3.zero;
            return result;
        }

        result.velocity /= timeToTarget;

        if (result.velocity.magnitude > maxSpeed)
        {
            result.velocity.Normalize();
            result.velocity *= maxSpeed;
        }

        character.orientation = NewOrientation(character.orientation, result.velocity);

        result.rotation = 0;
        return result;
    }

    private float NewOrientation(float current, Vector3 velocity)
    {
        if (velocity.magnitude > 0) 
        {
            return Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;
        }
        return current;
    }
}
