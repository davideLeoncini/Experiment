using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieFace : MonoBehaviour {

    public Rigidbody rb;
    public DieFace oppositeDieface;
    public int numberOnFace;

    private void OnTriggerStay(Collider other) {
        if (!rb.IsSleeping()) return;
        TotalFaceCounter.showResult.Invoke(oppositeDieface.numberOnFace);
    }
}
