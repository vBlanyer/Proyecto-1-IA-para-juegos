using UnityEngine;
using System;

// Clase Kinematic que añade propiedades de movimiento
public class Kinematic : Static
{
    // Propiedades de movimiento
    public Vector3 velocity;  
    public float rotation;    

    // Propiedades adicionales
    public float speedMagnitude;  // Magnitud de la velocidad
    public float accelerationMagnitude;  // Magnitud de la aceleración
    private Vector3 previousVelocity;  // Para calcular la aceleración

    protected override void Start()
    {
        base.Start();
        velocity = Vector3.zero;
        rotation = 0f;
        speedMagnitude = 0f;
        accelerationMagnitude = 0f;
        previousVelocity = Vector3.zero;
    }

    protected override void Update()
    {
        base.Update();

        // Actualiza la posicion y orientacion basadas en la velocidad y rotacion respectivamente
        position += velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;
        orientation = NewOrientation(orientation, velocity);

        // Calculos para observar la magnitud de la velocidad y aceleracion en el inspector

        // Calcular la magnitud de la velocidad
        speedMagnitude = velocity.magnitude;

        // Calcular la magnitud de la aceleración
        Vector3 acceleration = (velocity - previousVelocity) / Time.deltaTime;
        accelerationMagnitude = acceleration.magnitude;

        // Actualizar la velocidad previa
        previousVelocity = velocity;
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
