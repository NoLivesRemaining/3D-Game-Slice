using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour {

    [SerializeField] GameObject arrow;
    [SerializeField] Transform reference;
    [SerializeField] Player Player;

    private void OnEnable()
    {
        Player.OnFire.AddListener(Fire);
    }

    private void OnDisable()
    {
        Player.OnFire.RemoveListener(Fire);
    }

    private void Fire()
    {
        Instantiate(arrow, reference.position, reference.rotation);
    }
}
