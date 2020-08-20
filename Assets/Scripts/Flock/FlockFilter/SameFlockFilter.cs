using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter {
    // Start is called before the first frame update
    public override List<Transform> GetFilteredFlock(FlockAgent agent, List<Transform> original) {
        List<Transform> FilteredFlock = new List<Transform>();
        foreach (Transform item in original) {
            FlockAgent itemAgent = item.GetComponent<FlockAgent>();
            if(itemAgent != null && itemAgent.MyFlock == agent.MyFlock) {   // we are in same race flock
                FilteredFlock.Add(item);
            }
        }
        return FilteredFlock;
    }
}
