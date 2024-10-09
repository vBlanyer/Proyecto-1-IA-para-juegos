using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Face : Align
{
    public Kinematic faceTarget;

    protected override SteeringOutput GetSteering()
    {
        Vector3 direction = faceTarget.position - character.position;  // Usamos `faceTarget` de la clase Face

        if (direction.magnitude == 0)
        {
            return new SteeringOutput();
        }

        Kinematic explicitTarget = new Kinematic
        {
            position = faceTarget.position,
            orientation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg
        };

        // Asignar `faceTarget` a `base.target` para que `Align` lo use
        base.target = explicitTarget;

        return base.GetSteering();
    }
}
