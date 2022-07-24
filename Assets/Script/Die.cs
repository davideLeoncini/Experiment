using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

    public Rigidbody rb;

    public int nFaces;

    private void OnCollisionStay(Collision collision) {
        if (rb.velocity.sqrMagnitude > 0.01) return;
        // capire che faccia è
    }
}
