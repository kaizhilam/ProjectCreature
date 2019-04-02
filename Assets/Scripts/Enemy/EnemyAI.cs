using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private float MovementSpeed; //between 0 and 1
    private int spotRange;
    private GameObject Player;
    private float dist;
    private bool _CanMove = false;
    public Transform face;

    NavMeshAgent agent;

    private Rigidbody _Rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    
        spotRange = 50;
        _Rb = GetComponent<Rigidbody>();
        _Rb.angularDrag = 0;
        FirstEnemy _fe = GetComponent<FirstEnemy>();
        MovementSpeed = _fe.enemyStats.MovementSpeed;

        agent = GetComponent<NavMeshAgent>();
    }

    // Update is calle  d once per frame
    void FixedUpdate()
    {
        NewEnemyDetection();
    }

    private void NewEnemyDetection()
    {
        dist = Vector3.Distance(this.transform.position, face.position);
        if (dist < spotRange)
        {
            RaycastHit hit;
            Ray objectRay = new Ray(transform.position, face.position-transform.position);
            Debug.DrawRay(transform.position, face.position-transform.position, Color.red);
            if (Physics.Raycast(objectRay, out hit))
            {
                if (hit.collider.tag == "Player" && _CanMove == true)
                {
                    transform.LookAt(face); //look at player
                    Vector3 movement = Vector3.forward * MovementSpeed * Time.deltaTime; //move forward
                    _Rb.transform.Translate(movement); //move towards the player
                }
            }
            else
            {

            }
            //agent.enabled = true;
            //agent.SetDestination(Player.transform.position);
        }
    }

    private void EnemyDetection()
    {
        dist = Vector3.Distance(this.transform.position, Player.transform.position);
        if (dist < spotRange)
        {
            this.transform.LookAt(Player.transform);
            RaycastHit hit;
            Ray objectRay = new Ray(transform.position, transform.TransformDirection(Vector3.forward));
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward), Color.red);
            if (Physics.Raycast(objectRay, out hit))
            {
                if (hit.collider.tag == "Player" && _CanMove == true)
                {
                    Vector3 movement = new Vector3(MovementSpeed * Time.deltaTime, 0, MovementSpeed * Time.deltaTime);
                    _Rb.transform.Translate(movement); //move towards the player
                }
            }
            else
            {

            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _CanMove = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _CanMove = false;
        }
    }
}
