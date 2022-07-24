using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSpawner : MonoBehaviour {

    public GameObject d6;

    public void Spawn() {
        var obj = Instantiate(d6, transform.position, Quaternion.identity);
        obj.GetComponent<Rigidbody>().AddForce(Random.onUnitSphere);
    }
}
