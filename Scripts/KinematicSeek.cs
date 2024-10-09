using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicSeek : MonoBehaviour
{
    public Static character;
    public Static target;
    public float maxSpeed;
    private Animator anim;

    void Start()
    {
        maxSpeed = 2f;
        character = gameObject.AddComponent<Static>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        KinematicSteeringOutput steering = GetSteering();

        // Update the position and orientation
        character.position += steering.velocity * Time.deltaTime;
        character.orientation = NewOrientation(character.orientation, steering.velocity);

        anim.SetFloat("Velx", steering.velocity.x);
        anim.SetFloat("Vely", steering.velocity.z);
    }

    public KinematicSteeringOutput GetSteering()
    {
        KinematicSteeringOutput result = new KinematicSteeringOutput();

        // Get the direction to the target
        result.velocity = target.position - character.position;
        
        // The velocity is along this direction, at full speed.
        result.velocity.Normalize();
        result.velocity *= maxSpeed;

        // Face in the direction we want to move.
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
