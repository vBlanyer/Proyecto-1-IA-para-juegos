using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookWhereYoureGoing : Face
{
    protected override void Start()
    {
        base.Start();

        if (target == null)
        {
            target = new Kinematic();
        }
    }

    protected override SteeringOutput GetSteering()
    {
        Vector3 velocity = character.velocity;
        if (velocity.magnitude == 0)
        {
            return null;
        } 

        target.orientation = Mathf.Atan2(velocity.x, velocity.z) * Mathf.Rad2Deg;

        return base.GetSteering();
    }
}






        
    

        