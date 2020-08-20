using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehavior : FilteredFlockBehavior {
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> subFlock, Flock flock) {
        // keep current moving 
        if(subFlock.Count == 0) {
            return Vector3.zero;
        }
        // average agent position
        Vector3 avoidanceMove = Vector3.zero;
        int numAvoid = 0;
        List<Transform> filteredSubFlock = (filter == null) ? subFlock : filter.GetFilteredFlock(agent, subFlock);
        foreach (Transform item in filteredSubFlock) {
            // find neighbor whose distance between agents is too close
            Vector3 closestPoint = item.gameObject.GetComponent<Collider>().ClosestPoint(agent.transform.position);
            if (Vector3.SqrMagnitude(closestPoint - agent.transform.position) < flock.SquareAvoidanceRadius) {
                numAvoid++;
                avoidanceMove += (agent.transform.position - item.position);
            }
        }
        if(numAvoid > 0) {
            avoidanceMove /= subFlock.Count;
        }
        return avoidanceMove;
    }
}
