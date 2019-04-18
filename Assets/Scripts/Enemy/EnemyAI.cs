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
    public GameObject lookAt;
    private SkinnedMeshRenderer skin;

    NavMeshAgent agent;
    public LayerMask mask;

    private Rigidbody _Rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        skin = GetComponent<Dino>().GetComponent<SkinnedMeshRenderer>();
        spotRange = 50;
        _Rb = GetComponent<Rigidbody>();
        _Rb.angularDrag = 0;
        Enemy _en = GetComponent<Enemy>();
        MovementSpeed = _en.enemyStats.MovementSpeed;
        agent = GetComponent<NavMeshAgent>();
    }

    // using FixedUpdate instead of update so PCs with slow framerates don't skip important calculations
    void FixedUpdate()
    {
        NewEnemyDetection();
    }

    private void NewEnemyDetection()
    {
        dist = Vector3.Distance(this.transform.position, Player.transform.position);
        //if player is close enough...
        if (dist < spotRange)
        {
            //face the player
            this.transform.LookAt(Player.transform.position + (Vector3.up*2));
            RaycastHit hit;
            Ray objectRay = new Ray(transform.position + Vector3.up*4, Player.transform.position - (transform.position + Vector3.up * 4));
            //Debug.DrawRay(transform.position + Vector3.up * 4, Player.transform.position - (transform.position + Vector3.up * 4), Color.red);
            if (Physics.Raycast(objectRay, out hit, 1000))
            {
                //if sees player, move towards it, otherwise do nothing
                if (hit.collider.tag == "Player" && _CanMove == true)
                {
                    transform.LookAt(hit.collider.gameObject.transform.position + (Vector3.up*2)); //look at player
                    Vector3 movement = Vector3.forward * MovementSpeed * Time.deltaTime; //move forward
                    _Rb.transform.Translate(movement); //move towards the player
                }
            }
            else
            {
                print("ray not hitting anything at all?");
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
