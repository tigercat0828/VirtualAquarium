using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Cohesion")]
public class CohesionBehavior : FlockBehavior {
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> subFlock, Flock flock) {
        // if no neighbor, return no adjustment
        if(subFlock.Count == 0) {
            return Vector3.zero;
        
        }
        // average position of all agent
        Vector3 averagePosition = Vector3.zero;
        foreach(Transform individual in subFlock) {
            averagePosition += individual.position;
        }
        averagePosition = averagePosition / subFlock.Count;
        Vector3 cohesionMove = averagePosition - agent.transform.position;
        return cohesionMove;
    }
}
