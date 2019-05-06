using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	Animator anim;
    CharacterController controller;
    BoxCollider daggerHitbox;
	
	int jump;
	int run;
	int dodge;
	int sheathe;
	int unsheathe;
	int slash;
	
    void Start()
    {
		anim = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        daggerHitbox = GetComponent<BoxCollider>();

		jump = Animator.StringToHash("jump");
		run = Animator.StringToHash("run");
		dodge = Animator.StringToHash("dodge");
		slash = Animator.StringToHash("slash");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) { //controller.isGrounded not working here
            anim.SetTrigger(jump);
		} 
		
		if(Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger(slash);
            Collider[] colliders = Physics.OverlapBox(daggerHitbox.center, daggerHitbox.size);
            for(int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Enemy"))
                {
                    if (daggerHitbox.bounds.Intersects(colliders[i].bounds))
                    {
                        Debug.Log(colliders[i].name);
                    }
                }
            }
		}
		
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A))
        {
			anim.SetBool(run, true);

            if (Input.GetKeyDown(KeyCode.E) && controller.isGrounded)
            {
                anim.SetTrigger(dodge);
            }

        } else
        {
			anim.SetBool(run, false);
		}
    }
}
