using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KinematicWander : MonoBehaviour
{ 
    public Static character;
    public float maxSpeed;
    public float maxRotation;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 2f;
        maxRotation = 2000.0f;

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        KinematicSteeringOutput steering = GetSteering();
        character.position += steering.velocity * Time.deltaTime;
        character.orientation += steering.rotation * Time.deltaTime;

        // Actualiza los parámetros del animador
        anim.SetFloat("Velx", steering.velocity.x);
        anim.SetFloat("Vely", steering.velocity.z);
    }

    public KinematicSteeringOutput GetSteering()
    {
        KinematicSteeringOutput result = new KinematicSteeringOutput();

        float orientationRadians = Mathf.Deg2Rad * character.orientation;
        result.velocity = maxSpeed * new Vector3(Mathf.Sin(orientationRadians), 0, Mathf.Cos(orientationRadians));

        // Change our orientation randomly
        result.rotation = Random.Range(-maxRotation, maxRotation);  

        return result;
    }
}
