using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinoSight : MonoBehaviour
{
	NavMeshAgent agent;
	Animator anim;
	int chasingPlayer;
	bool chase = false;
	
	public Transform target;
	
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
		anim = GetComponent<Animator>();
		chasingPlayer = Animator.StringToHash("chasingPlayer");
	}

    // Update is called once per frame
    void Update()
    {
		if(Vector3.Distance(target.position, transform.position) > 15.0f) 
		{
			anim.SetBool(chasingPlayer, false);
			agent.Stop();
		} 
		else 
		{
			anim.SetBool(chasingPlayer, true);
			agent.SetDestination(target.position);
		}
		
    }
}
