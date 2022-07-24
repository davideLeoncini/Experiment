using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    public float distanceFromCam;
    public Rigidbody rb;

    public float forceAmplifier;
    public float torqueAmplifier;
    public float releaseForceAmplifier;
    public float releaseTorqueAmplifier;
    public float drag;

    private void OnMouseDown() {
        rb.useGravity = false;
        rb.drag = drag;
    }

    private void OnMouseDrag() {
        rb.AddForce(forceAmplifier * (Camera.main.ScreenToWorldPoint(Input.mousePosition + Vector3.forward * distanceFromCam) - transform.position));
        rb.AddTorque(torqueAmplifier * rb.velocity);
    }

    private void OnMouseUp() {
        rb.useGravity = true;
        rb.drag = 0;
        rb.AddForce(releaseForceAmplifier * rb.velocity);
        rb.AddTorque(releaseTorqueAmplifier * (Random.insideUnitSphere + Vector3.one));
    }
}
