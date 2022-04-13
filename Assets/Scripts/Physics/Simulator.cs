using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : Singleton<Simulator>
{
	[SerializeField] List<Force> forces;
	[SerializeField] IntData fixedFPS;
	[SerializeField] StringData FPS;

	public List<Body> bodies { get; set; } = new List<Body>();
	Camera activeCamera;

	private float timeAccumulator = 0;
	float fixedDeltaTime => 1.0f / fixedFPS.value;



	private void Start()
	{
		activeCamera = Camera.main;
	}

    private void Update()
    {
		//get fps
		FPS.value = (1.0f/Time.deltaTime).ToString("F1");
		
		//add DT to time
		timeAccumulator += Time.deltaTime;

		//apply force
		forces.ForEach(force => force.ApplyForce(bodies));

		while (timeAccumulator > fixedDeltaTime)
		{
			bodies.ForEach(body =>
			{
				Integrator.SemiImplicitEuler(body, fixedDeltaTime);
			});

			timeAccumulator -= fixedDeltaTime;
		}
		bodies.ForEach(body => body.acceleration = Vector2.zero);
	}

    public Vector3 GetScreenToWorldPosition(Vector2 screen)
	{
		Vector2 world = activeCamera.ScreenToWorldPoint(screen);
		return world;
	}

	public Body GetScreenToBody(Vector3 screen)
	{
		Body body = null;

		Ray ray = activeCamera.ScreenPointToRay(screen);
		RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

		if (hit.collider)
		{
			Console.WriteLine("Hit");
			hit.collider.gameObject.TryGetComponent<Body>(out body);
		}
		else
		{
			Console.WriteLine("Miss");
		}
		return body;
	}
}
