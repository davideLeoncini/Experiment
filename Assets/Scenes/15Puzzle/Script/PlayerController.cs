using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public CharacterController characterController;
    public float xSpeed, ySpeed;

    void Update() {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        MovePlayer(x, y);
    }

    void MovePlayer(float x, float y) {
        characterController.SimpleMove(new Vector3(x * xSpeed * Time.deltaTime, 0, y * ySpeed * Time.deltaTime));
        print(x + y);
    }

    void MoveCamera() {

    }
}
