using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour {

    private float deltaX;
    private Quaternion spineRotation;
    private bool aim;
    public UnityEvent OnFire;
    private Animator animator;
    private bool enableIK;
    private float weightIK;
    private Vector3 positionIK;

    void Start()
    {

        animator = GetComponent<Animator>();
    }

    public void Move(float turn, float forward)
    {
        animator.SetFloat("Turn", turn);
        animator.SetFloat("Forward", forward);
    }
   
   private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            animator.SetTrigger("Hit");
        }
    }

    public void AimFire(bool aimDown, bool aimHold, bool fire)
    {
        animator.SetBool("Aim", aimHold);

        if (aimHold && fire)
        {
            animator.SetTrigger("Fire");

            if (OnFire != null)
            {
                OnFire.Invoke();
            }
        }
    }
}
