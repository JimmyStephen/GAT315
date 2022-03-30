using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Integrator
{
    public static void ExplicitEuler(Body body, float dt)
    {
        //body.acceleration = body.force / body.mass;
        body.velocity += body.acceleration * dt;
        body.position += body.velocity * dt;
    }

    public static void SemiImplicitEuler(Body body, float dt)
    {
        //body.acceleration = body.force / body.mass;
        body.velocity += body.acceleration * dt;
        body.position += body.velocity * dt;

    }

}
