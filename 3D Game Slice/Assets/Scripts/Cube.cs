using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, iPoolable
{

    [SerializeField] float sideVelocity;
    [SerializeField] float upVelocity; //Creating the editable fields

    public void OnObjectPolled()
    {
        Vector3 velocity = new Vector3();
        velocity.x = Random.Range(-sideVelocity, sideVelocity); //Assigning the values given to change the velocity of the objects as their spawned using rigidbody
        velocity.y = Random.Range(0, upVelocity);
        velocity.z = Random.Range(-sideVelocity, sideVelocity);

        GetComponent<Rigidbody>().velocity = velocity;
    }
}