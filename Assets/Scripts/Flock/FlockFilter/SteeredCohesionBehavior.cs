using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior {
    public Vector3 CurrentVelocity;
    public float SmoothTime = 0.5f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> subFlock, Flock flock) {
        // if no neighbor, return no adjustment
        if(subFlock.Count == 0) {
            return Vector3.zero;
        }
        // average position of agents
        Vector3 averagePosition = Vector3.zero;
        List<Transform> filteredSubFlock = (filter == null) ? subFlock : filter.GetFilteredFlock(agent, subFlock);
        foreach (Transform item in filteredSubFlock) {
            averagePosition += item.position;
        }
        Vector3 subFlockCenter = averagePosition / subFlock.Count;
        Vector3 cohesionMove = subFlockCenter - agent.transform.position;
        cohesionMove = Vector3.SmoothDamp(agent.transform.forward, cohesionMove, ref CurrentVelocity, SmoothTime);
        return cohesionMove;
    }
}
