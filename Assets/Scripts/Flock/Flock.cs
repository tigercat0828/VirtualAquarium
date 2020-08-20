using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour {
    public static float DENSITY = 0.08f;
    public FlockAgent agentPrefab;
    public FlockBehavior behavior;
    List<FlockAgent> agentList = new List<FlockAgent>();

    [Range(10, 500)] public int StartingCount = 100;        
    [Range(1,100)] public float DriveFactor = 1.0f;             // for debug
    [Range(1, 100)] public float FlockSpeed = 5.0f;             
    [Range(0.1f, 10f)] public float NeighborRadius = 1.5f;      
    [Range(0f, 1f)] public float AvoidanceRate = 0.5f;          // AvoidanceRadius = NeighborRadius * AvoidanceRate
    // for optimization 
    private float squareFlockSpeed;                            
    private float squareFlockNeighborRadius;
    private float squareFlockAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareFlockAvoidanceRadius;} }
    public void Start() {
        PrepareSquareData();
        SpawnAgents();
    }
    public void Update() {
        // calculate move offset of all the agent in this flock 
        foreach (FlockAgent agent in agentList) {
            List<Transform> subFlock = GetSubFlock(agent);
            Vector3 move = behavior.CalculateMove(agent, subFlock, this);
            move *= DriveFactor;
            if (move.sqrMagnitude > squareFlockSpeed) { // restrict the speed less than flock speed
                move = move.normalized * FlockSpeed;
            }
            agent.Move(move);
        }
    }
    private void PrepareSquareData() {
        squareFlockSpeed = FlockSpeed * FlockSpeed;
        squareFlockNeighborRadius = NeighborRadius * NeighborRadius;
        squareFlockAvoidanceRadius = squareFlockNeighborRadius * AvoidanceRate * AvoidanceRate;
    }
    private void SpawnAgents() {
        for (int i = 0; i < StartingCount; i++) {
            // spawn agent belong in this flock
            float randX = Random.Range(0f, 179f);
            float randY = Random.Range(0f, 179f);
            float randZ = Random.Range(0f, 179f);
            Vector3 randRotation = new Vector3(randX, randY, randZ);
            FlockAgent newAgent = Instantiate(
              agentPrefab,
              new Vector3(0,0,0),
              Quaternion.Euler(randRotation),
              transform
            );
            newAgent.name = "Agent" + i;
            newAgent.BelongToFlock(this);   // the new agent belong to this flock
            agentList.Add(newAgent);        // add this agent into the flock
        }
        /*
        foreach (var agent in agentList) {
            agent.transform.position = Random.insideUnitSphere * StartingCount * DENSITY;
        }
         */
    }

    public List<Transform> GetSubFlock(FlockAgent agent) {
        List<Transform> subFlock = new List<Transform>();
        // get subflock by check every agent which are in neightbor radius
        Collider[] nearbyColliders = Physics.OverlapSphere(agent.transform.position, NeighborRadius);
        foreach (Collider collider in nearbyColliders) {
            if( collider != agent.Collider) {   // exclude itself
                subFlock.Add(collider.transform);
            }
        }
        return subFlock;
    }
}
