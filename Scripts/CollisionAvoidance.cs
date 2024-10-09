using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    public Kinematic character;
    public float maxAcceleration;

    public Kinematic[] targets;

    public float radius;

    void Start()
    {
        maxAcceleration = 10.0f;
        radius = 4.0f;

        // Asignar varios objetivos a la vez
        List<Kinematic> targetList = new List<Kinematic>();

        // Buscar todos los objetos con el tag "CubeTag"
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("CubeTag");
        foreach (GameObject cube in cubes)
        {
            Kinematic cubeKinematic = cube.GetComponent<Kinematic>();
            if (cubeKinematic != null)
            {
                targetList.Add(cubeKinematic);
            }
            else
            {
                Debug.LogWarning($"Cube {cube.name} does not have a Kinematic component!");
            }
        }

        // Agregar el objeto con el tag "NpcTag"
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("NpcTag");
        foreach (GameObject npc in npcs)
        {
            Kinematic npcKinematic = npc.GetComponent<Kinematic>();
            if (npcKinematic != null)
            {
                targetList.Add(npcKinematic);
            }
            else
            {
                Debug.LogWarning($"Npc {npc.name} does not have a Kinematic component!");
            }
        }

        targets = targetList.ToArray();
        Debug.Log($"Total targets assigned: {targets.Length}");
        
    }

    void Update()
    {
        SteeringOutput steering = GetSteering();
        ApplySteering(steering, Time.deltaTime);
    }

    private SteeringOutput GetSteering()
    {
        float shortestTime = Mathf.Infinity;

        Kinematic firstTarget = null;
        float firstMinSeparation = 0.0f;
        float firstDistance = 0.0f;
        Vector3 firstRelativePos = Vector3.zero;
        Vector3 firstRelativeVel = Vector3.zero;

        Vector3 relativePos;
        Vector3 relativeVel;
        float relativeSpeed;
        float timeToCollision;
        float distance;
        float minSeparation;

        // Loop through each target
        for (int i = 0; i < targets.Length; i++)
        {
            relativePos = targets[i].position - character.position;
            relativePos.y = 0; 

            relativeVel = targets[i].velocity - character.velocity;
            relativeVel.y = 0; 

            relativeSpeed = relativeVel.magnitude;

            // Avoid division by zero
            if (relativeSpeed == 0)
            {
                Debug.Log("Relative speed is zero, skipping this target.");
                continue;
            }

            timeToCollision = Vector3.Dot(relativePos, relativeVel) / (relativeSpeed * relativeSpeed);

            distance = relativePos.magnitude;
            minSeparation = distance - relativeSpeed * timeToCollision;
            if (minSeparation > 2 * radius)
            {
                Debug.Log($"Skipping target {i}: Minimum separation {minSeparation} is greater than the radius threshold.");
                continue;
            }

            if (timeToCollision > 0 && timeToCollision < shortestTime)
            {
                shortestTime = timeToCollision;
                firstTarget = targets[i];
                firstMinSeparation = minSeparation;
                firstDistance = distance;
                firstRelativePos = relativePos;
                firstRelativeVel = relativeVel;

                Debug.Log($"New closest target found at index {i} with shortest collision time: {shortestTime}");
            }
        }

        if (firstTarget == null)
        {
            Debug.Log("No target found for collision avoidance.");
            return new SteeringOutput();
        }

        if (firstMinSeparation <= 0 || firstDistance < 2 * radius)
        {
            relativePos = firstTarget.position - character.position;
            relativePos.y = 0;  
            Debug.Log("Calculating avoidance based on current position.");
        }
        else
        {
            relativePos = firstRelativePos + firstRelativeVel * shortestTime;
            Debug.Log("Calculating avoidance based on future position.");
        }

        relativePos.Normalize();

        SteeringOutput result = new SteeringOutput
        {
            linear = -relativePos * maxAcceleration,
            angular = 0
        };

        Debug.Log($"Avoidance steering calculated. Linear acceleration: {result.linear}, Angular acceleration: {result.angular}");

        return result;
    }

    private void ApplySteering(SteeringOutput steering, float deltaTime)
    {
        // Update the velocity of the character
        character.velocity += steering.linear * deltaTime;
    }
}
