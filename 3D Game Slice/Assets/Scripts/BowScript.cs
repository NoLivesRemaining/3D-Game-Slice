using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour {

    [SerializeField] GameObject arrow; //Setting up the items needed to shoot the arrow
    [SerializeField] Transform reference; //Reference is the launching point for the arrow
    [SerializeField] Player Player;

    private void OnEnable()
    {
        Player.OnFire.AddListener(Fire); //Allows the bow to fire when the player script calls it
    }

    private void OnDisable()
    {
        Player.OnFire.RemoveListener(Fire);
    }

    private void Fire()
    {
        Instantiate(arrow, reference.position, reference.rotation); //Creates the arrow using the arrow prefab assigned in editor
    }
}
