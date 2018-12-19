using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour {

    private Rigidbody m_rigidbody; 
    [SerializeField] float m_power; //Allowing the lauching force of the arrow to be edited

    private void OnEnable()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_rigidbody.AddForce(transform.forward * m_power); //Launching the arrow using rigidbody
    }
}
