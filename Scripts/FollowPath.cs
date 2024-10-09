using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : Seek
{
    public Path path; 
    public float pathOffset = 0.1f; 
    public float predictTime = 0.5f; 
    private float currentParam; 

    protected override void Start()
    {
        base.Start();

        if (path == null)
        {
            Debug.LogWarning("FollowPath: No se ha asignado un 'Path' al componente. Asigna un camino en el Inspector.");
        }
        else
        {
            Debug.Log("FollowPath: Componente 'Path' asignado correctamente.");
        }
        
        currentParam = 0;
        Debug.Log("FollowPath: Parámetro inicial en el camino establecido a 0.");
    }

    protected override void Update()
    {
        if (path != null)
        {
            Vector3 futurePos = character.position + character.velocity * predictTime;
            Debug.Log("FollowPath: Calculando posición futura en el camino. Posición futura: " + futurePos);

            currentParam = path.GetParam(futurePos, currentParam);
            Debug.Log("FollowPath: Parámetro actual en el camino: " + currentParam);

            float targetParam = currentParam + pathOffset;
            Debug.Log("FollowPath: Parámetro objetivo en el camino con offset: " + targetParam);

            Vector3 targetPosition = path.GetPosition(targetParam);
            Debug.Log("FollowPath: Posición objetivo en el camino: " + targetPosition);

            target.position = targetPosition;
            Debug.Log("FollowPath: Posición del target actualizada a: " + target.position);
        }
        else
        {
            Debug.LogWarning("FollowPath: El 'Path' no está asignado, no se puede seguir el camino.");
        }

        base.Update();
    }

    protected override SteeringOutput GetSteering()
    {
        SteeringOutput steering = base.GetSteering();
        Debug.Log("FollowPath: Calculando SteeringOutput. Linear: " + steering.linear + ", Angular: " + steering.angular);
        return steering;
    }
}

