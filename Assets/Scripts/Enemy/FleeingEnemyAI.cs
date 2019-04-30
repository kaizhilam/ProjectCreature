using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleeingEnemyAI : MonoBehaviour
{
    private float MovementSpeed; //between 0 and 1
    private int spotRange;
    private GameObject Player;
    private float dist;
    private bool _CanMove = false;
    private SkinnedMeshRenderer skin;
    private bool _Wandering = false;
    private bool _IsRotating = false;
    private bool _IsWalking = false;
    private RaycastHit hit;
    public float rotSpeed = 100f;
    private float health;
    private Enemy _En;

    NavMeshAgent agent;
    public LayerMask mask;

    private Rigidbody _Rb;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        spotRange = 50;
        _Rb = GetComponent<Rigidbody>();
        _Rb.angularDrag = 0;
        _En = GetComponent<Enemy>();
        health = _En.Health;
        MovementSpeed = _En.MovementSpeed1;
        //agent = GetComponent<NavMeshAgent>();
    }

    // using FixedUpdate instead of update so PCs with slow framerates don't skip important calculations
    void FixedUpdate()
    {
        if (DetectsEnemy())
        {
            //if player is spotted by the enemy, chase the player and stop wandering
            RunAway();
            StopCoroutine(Wander());
        }
        else if (!_Wandering)
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

        //enemy will wait for walkWait seconds, will then walk for walkTime seconds
        //with then pause for rotateWait seconds, then will rotate for rotTime
        //then will start the process again
        //each of these times are set randomly, with their range indicated below
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
        dist = Vector3.Distance(this.transform.position, Player.transform.position);
        Renderer rend = Player.GetComponentInChildren<Renderer>();
        if (dist < spotRange && rend.enabled == true)
        {
            
            Ray objectRay = new Ray(transform.position + Vector3.up * 4, Player.transform.position - (transform.position + Vector3.up * 4));
            //Debug.DrawRay(transform.position + Vector3.up * 4, Player.transform.position - (transform.position + Vector3.up * 4), Color.red);
            if (Physics.Raycast(objectRay, out hit, 1000))
            {
                //if sees player, run away, otherwise do nothing
                if (hit.collider.tag == "Player")
                {
                    return true;

                }
                return false;
            }
            else
            {
                //print("ray not hitting anything at all?");

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
        //if comes into contact with projectile
        if (collision.gameObject.tag == "Attack")
        {
            //take that projectiles damage and deduct it from itself
            _En.TakeDamage(collision.gameObject.GetComponent<ProjectileBehavior>().Damage);
            _Rb.AddForce(-collision.relativeVelocity * 50);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _CanMove = false;
        }
    }
    private void RunAway()
    {
        //get vector from this to player
        Vector3 distanceVector = Player.transform.position - this.transform.position;
        //create position that is this.position - (this to player) vector
        Vector3 awayPosition = this.transform.position - distanceVector;
        //face that new position (away form player)
        this.transform.LookAt(awayPosition);

        Vector3 movement = Vector3.forward * MovementSpeed * Time.deltaTime; //move forward
        _Rb.transform.Translate(movement); //move towards the player
    }


}
