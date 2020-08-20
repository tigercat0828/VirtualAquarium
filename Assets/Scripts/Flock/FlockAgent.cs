using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour {

    Flock myFlock;  // for recognize whick flock I belong to
    public Flock MyFlock { get { return myFlock; } }

    Collider collider;
    public Collider Collider { get { return collider; } }

    private void Start() {
        collider = GetComponent<Collider>();
        transform.position = new Vector3(0, 0, 0);
    }
    public void BelongToFlock(Flock flock) {
        myFlock = flock;
    }
    public void Move(Vector3 velocity) {
        transform.forward = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
