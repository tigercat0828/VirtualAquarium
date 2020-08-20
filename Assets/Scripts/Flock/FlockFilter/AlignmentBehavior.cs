using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(menuName ="Flock/Behavior/Alignment")]
public class AlignmentBehavior : FilteredFlockBehavior {
    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> subFlock, Flock flock) {
        // if no member, maintain current
        if(subFlock.Count == 0) {
            return agent.transform.forward;
        }
        Vector3 alignmentMove = Vector3.zero;
        List<Transform> filteredSubFlock = (filter == null) ? subFlock : filter.GetFilteredFlock(agent,subFlock);
        // get average of sub flock forward as alignment
        foreach (Transform item in filteredSubFlock) {
            alignmentMove += item.transform.forward;
            //alignmentMove += item.forward;
        }
        alignmentMove /= filteredSubFlock.Count;
        return alignmentMove;
    }
}
