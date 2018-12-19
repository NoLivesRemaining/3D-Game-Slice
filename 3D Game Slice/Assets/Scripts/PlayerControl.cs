using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {

    private float turn;
    private float forward;
    private bool aimDown; //Creating variables for player control animations
    private bool aimHold;
    private bool fire;



    private Player player;

    private void Start()
    {
        player = GetComponent<Player>(); //Getting the player script to attach these to
    }
    // Use this for initialization

    void FixedUpdate()
    {
        // Get Inputs
        turn = Input.GetAxis("Horizontal");
        forward = Input.GetAxis("Vertical");
        aimDown = Input.GetMouseButtonDown(1); //Assigning inputs for animations
        aimHold = Input.GetMouseButton(1);
        fire = Input.GetMouseButtonDown(0);


        player.Move(turn, forward);
        player.AimFire(aimDown, aimHold, fire); //Assigning animations 
    }
}
