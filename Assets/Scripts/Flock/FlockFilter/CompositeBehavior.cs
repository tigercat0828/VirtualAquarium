using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Composite")]
public class CompositeBehavior : FlockBehavior {

    public FlockBehavior[] BehaviorList;
    public float[] Weights;

    public override Vector3 CalculateMove(FlockAgent agent, List<Transform> subFlock, Flock flock) {
        if (BehaviorList.Length != Weights.Length) {
            Debug.LogError("Data dismatch in " + name, this);
            return Vector3.zero;
        }
        Vector3 totalMove = Vector3.zero;
        for (int i = 0; i < BehaviorList.Length; i++) {
            Vector3 move = BehaviorList[i].CalculateMove(agent, subFlock, flock) * Weights[i];
            if (move != Vector3.zero) {
                if (move.sqrMagnitude > Weights[i] * Weights[i]) {
                    move.Normalize();
                    move *= Weights[i];
                }
            }
            totalMove += move;
        }
        return totalMove;
    }
}
