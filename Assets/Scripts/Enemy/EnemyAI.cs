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
    private bool _Wandering = false;
    private bool _IsRotating = false;
    private bool _IsWalking = false;
    private RaycastHit hit;
    public float rotSpeed = 100f;

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

    // Update is called once per frame
    void FixedUpdate()
    {
        if (DetectsEnemy())
        {
            ChasePlayer();
            StopCoroutine(Wander());
        }
        else if(!_Wandering)
        {
            StartCoroutine(Wander());
        }
        if (_IsRotating)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }
        if (_IsWalking)
        {
            transform.position += transform.forward * MovementSpeed * Time.deltaTime;
        }
        
    }

    IEnumerator Wander()
    {
        int rotTime = Random.Range(1, 3);
        int rotateWait = Random.Range(1, 4);
        int rotate = Random.Range(-2, 2);
        int walkWait = Random.Range(1, 4);
        int walkTime = Random.Range(1, 5);

        _Wandering = true;

        yield return new WaitForSeconds(walkWait);
        _IsWalking = true;
        yield return new WaitForSeconds(walkTime);
        _IsWalking = false;
        yield return new WaitForSeconds(rotateWait);
        _IsRotating = true;
        yield return new WaitForSeconds(rotTime);
        _IsRotating = false;
        _Wandering = false;
    }

    private bool DetectsEnemy()
    {
        return false;
        dist = Vector3.Distance(this.transform.position, Player.transform.position);
        if (dist < spotRange)
        {
            this.transform.LookAt(Player.transform.position + (Vector3.up*2));
            Ray objectRay = new Ray(transform.position + Vector3.up*4, Player.transform.position - (transform.position + Vector3.up * 4));
            //Debug.DrawRay(transform.position + Vector3.up * 4, Player.transform.position - (transform.position + Vector3.up * 4), Color.red);
            if (Physics.Raycast(objectRay, out hit, 1000))
            {
                print(hit.collider.gameObject.name);
                if (hit.collider.tag == "Player" && _CanMove == true)
                {
                    return true;
                    
                }
                return false;
            }
            else
            {
                print("ray not hitting anything at all?");
                
            }
        }
        return false;
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
    private void ChasePlayer()
    {
        transform.LookAt(hit.collider.gameObject.transform.position + (Vector3.up * 2)); //look at player
        Vector3 movement = Vector3.forward * MovementSpeed * Time.deltaTime; //move forward
        _Rb.transform.Translate(movement); //move towards the player
    }
}
