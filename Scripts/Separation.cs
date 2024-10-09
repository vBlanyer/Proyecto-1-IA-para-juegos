using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Separation : MonoBehaviour
{  
    public Kinematic character;
    public float maxAcceleration;

    public Kinematic[] targets;

    public float threshold;

    public float decayCoefficient;

    void Start()
    {
        maxAcceleration = 5.0f;
        threshold = 5.0f;
        decayCoefficient = 10.0f;


       // Asignar varios objetivos a la vez
        List<Kinematic> targetList = new List<Kinematic>();

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
        SteeringOutput steering = getSteering();
        ApplySteering(steering, Time.deltaTime);
    }

    public SteeringOutput getSteering()
    {
        SteeringOutput result = new SteeringOutput();

        foreach(Kinematic target in targets)
        {
            Vector3 direction = character.transform.position - target.transform.position;
            float distance = direction.magnitude;

            if(distance < threshold)
            {
                float strength = Mathf.Min(decayCoefficient / (distance * distance), maxAcceleration);

                direction.Normalize();
                result.linear += strength * direction;

            }
        }
        return result;
    }

    private void ApplySteering(SteeringOutput steering, float deltaTime)
    {
        character.velocity += steering.linear * deltaTime;
    }
}
