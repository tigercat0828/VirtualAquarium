using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Flock/Behavior/Stay In Radius")]
public class StayInRadiusBehavior : FlockBehavior {

    public Vector3 Center;
    public float Radius = 15f;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> subFlock, Flock flock) {
        //strength of back to the center
        Vector3 centerOffset = Center - agent.transform.position;
        float rate = centerOffset.magnitude / Radius;
        if(rate < 0.8f) {   // if in the ring, keep current step
            return Vector3.zero;
        }
        return centerOffset * 0.8f ;
    }
}
