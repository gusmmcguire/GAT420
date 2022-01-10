using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutonomousAgent : Agent
{
    [SerializeField] Perception perception;
    [SerializeField] Steering steering;
    [SerializeField, Tooltip("True if this agent should seek the target instead of run away")] bool shouldSeek;

    public float maxSpeed;
    public float maxForce;
    
    public Vector3 velocity { get; set; } = Vector3.zero;

    void Update()
    {
        Vector3 acceleration = Vector3.zero;

        GameObject[] gameObjects = perception.GetGameObjects();
        if(gameObjects.Length != 0)
        {
            Debug.DrawLine(transform.position, gameObjects[0].transform.position);

            Vector3 force;
            if(shouldSeek) force = steering.Seek(this,gameObjects[0]);
            else force = steering.Flee(this,gameObjects[0]);
            acceleration += force.normalized * 3;
        }

        velocity += acceleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;

        if(velocity.sqrMagnitude > 0.1f) transform.rotation = Quaternion.LookRotation(velocity);
    }
}
