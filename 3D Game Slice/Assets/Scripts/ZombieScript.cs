using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieScript : MonoBehaviour
{
    private enum NPCSTATE { CHASE, PATROL };
    private NPCSTATE m_NPCSTATE;
    private NavMeshAgent m_NaveMeshAgent;
    private int m_CurrentWaypoint = 0;
    private bool m_IsPlayerNear;
    private Animator Zanimator; //Setting up variables
    public GameObject Wall;

    [SerializeField] float m_FieldOfView;
    [SerializeField] float m_ThresholdDistance;
    [SerializeField] private Transform[] m_Waypoints;
    [SerializeField] GameObject m_Player;

    // Use this for initialization
    void Start()
    {
        m_NPCSTATE = NPCSTATE.PATROL; //Setting State into Patrol
        m_NaveMeshAgent = GetComponent<NavMeshAgent>();
        m_NaveMeshAgent.autoBraking = false;
        Zanimator = GetComponent<Animator>(); //Grabbing components
        HandleAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        CheckPlayer();
        m_NaveMeshAgent.nextPosition = transform.position; //Telling the patroller to move on the navmesh

        switch (m_NPCSTATE)
        {
            case NPCSTATE.CHASE:
                Chase();
                break;
            case NPCSTATE.PATROL:
                Patrol();
                break;
            default:
                break;
        }

        
    }


    void CheckPlayer()
    {
        if (m_IsPlayerNear && CheckFieldOfView() && CheckOclusion())  //Checking to see if the view of the player is unobstructed
        {
            m_NPCSTATE = NPCSTATE.CHASE;
            HandleAnimation();
            return;
        }
        if (m_NPCSTATE == NPCSTATE.CHASE && !CheckOclusion())
        {
            m_NPCSTATE = NPCSTATE.PATROL;
            HandleAnimation();
        }
    }

    void Chase()
    {
        m_NaveMeshAgent.SetDestination(m_Player.transform.position); //Changing the waypoint to the player on detection
    }

    private bool CheckFieldOfView()
    {
        Vector3 distance = m_Player.transform.position - this.transform.position;
        Vector3 angle = (Quaternion.FromToRotation(transform.forward, distance)).eulerAngles; //Creating field of view checker

        if (angle.y > 180.0f) angle.y = 360.0f - angle.y;
        else if (angle.y < 180.0f) angle.y = angle.y + 360.0f; //Setting arc for field of view

        if (angle.y < m_FieldOfView / 2)
        {
            return true;
        }
        return false;
    }


    void Patrol()
    {
        if(m_Waypoints.Length == 0)
        {
            m_NaveMeshAgent.destination = m_Waypoints[m_CurrentWaypoint].position; //Telling zombie to move from one waypoint to another
        }
    }

    void CheckWaypointDistance()
    {
        if (Vector3.Distance(m_Waypoints[m_CurrentWaypoint].position, this.transform.position) < m_ThresholdDistance)
        {
            m_CurrentWaypoint = (m_CurrentWaypoint + 1) % m_Waypoints.Length; //Checking how far the next waypoint is
        }
    }

    bool CheckOclusion()
    {
        RaycastHit Hit;
        Vector3 direction = m_Player.transform.position - transform.position;

        if (Physics.Raycast(this.transform.position, direction, out Hit)) //Making a raycast assigned to the player
        {
            if (Hit.collider.gameObject == m_Player) //Checking if the raycast towards the player is uninterrupted
            {
                return true;
            }
        }
        return false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            m_IsPlayerNear = true; //Checking the collider arund the zombie for the player

        }
    }

    private void OnTriggerExit(Collider other)
    {
        m_IsPlayerNear = false; ; // Ending the chase when the player leaves the collider
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 5.0f);   //Drawing visuals for the raycasts

        Gizmos.color = Color.red;
        Vector3 direction = m_Player.transform.position - transform.position;
        Gizmos.DrawRay(transform.position, direction);

        Vector3 RightDirection = Quaternion.AngleAxis(m_FieldOfView / 2, Vector3.up) * transform.forward;
        Vector3 LeftDirection = Quaternion.AngleAxis(-m_FieldOfView / 2, Vector3.up) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, RightDirection * 5);
        Gizmos.DrawRay(transform.position, LeftDirection * 5);
    }

    void HandleAnimation()
    {

        if (m_NPCSTATE == NPCSTATE.CHASE)
        {
            Zanimator.SetFloat("Run", 1f); //Changing zombies speeds based off detection
        }
        else
        {
            Zanimator.SetFloat("Run", 0.5f);
        }
    }


private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Zanimator.SetTrigger("Attack");  //If the player is near the zombie will attack
        }

        if (other.tag == "Arrow")
        {
            Zanimator.SetTrigger("Hit");
            gameObject.GetComponent<Collider>().isTrigger = true;
            gameObject.GetComponent<Rigidbody>().detectCollisions = false;  //If the arrow hits it will make the zombie play a death animation
            gameObject.GetComponent<Rigidbody>().isKinematic = true;       //It also disables the colliders and rigidbody so they can be based over it also removes the collider on the Wall
            Wall.GetComponent<Collider>().isTrigger = true;
        }
    }
}
