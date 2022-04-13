using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationForce : Force
{
    [SerializeField] FloatData gravitation;

    public override void ApplyForce(List<Body> bodies)
    {
//        Debug.Log("Gravitation Apply Force Called On <" + bodies.Count + "> Bodies With A Gravitational Force Of <" + gravitation.value + ">");
        for(int i = 0; i < bodies.Count; i++)
        {
            for(int j = 0; j < bodies.Count; j++)
            {
                //if they are the same continue
                if (i == j) continue;
                Body bodyA = bodies[i];
                Body bodyB = bodies[j];

                Vector2 direction = bodyA.position - bodyB.position;
                float distance = Mathf.Max(1, direction.magnitude);
                float force = gravitation.value * (bodyA.mass * bodyB.mass) / distance;

                //Debug.Log("Apply Force On Object <"+i+"> and <"+j+"> With A Force Of <"+force+"> And Direction Of <"+direction.normalized+"> (Total <"+direction.normalized * force+">)");

                bodyA.ApplyForce(-direction.normalized * force, Body.eForceMode.Force);
                bodyB.ApplyForce(direction.normalized * force, Body.eForceMode.Force);
            }
        }
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
