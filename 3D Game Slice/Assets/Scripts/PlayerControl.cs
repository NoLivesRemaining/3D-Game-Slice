using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private float turn;
    private float forward;
    private bool aimDown;
    private bool aimHold;
    private bool fire;



    private Player player;

    private void Start()
    {
        player = GetComponent<Player>();
    }
    // Use this for initialization

    void FixedUpdate()
    {
        // Get Inputs
        turn = Input.GetAxis("Horizontal");
        forward = Input.GetAxis("Vertical");
        aimDown = Input.GetMouseButtonDown(1);
        aimHold = Input.GetMouseButton(1);
        fire = Input.GetMouseButtonDown(0);


        player.Move(turn, forward);
        player.AimFire(aimDown, aimHold, fire);
    }
}
