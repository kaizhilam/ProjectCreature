using UnityEngine;

public class PlayerTestAnim : MonoBehaviour
{
	Animator anim;
	
	int jump;
	int run;
	int dodge;
	int sheathe;
	int unsheathe;
	int slash;
	bool daggerOut = false;
	
    void Start()
    {
		anim = GetComponent<Animator>();
		
		jump = Animator.StringToHash("tJump");
		run = Animator.StringToHash("bRun");
		dodge = Animator.StringToHash("tDodge");
		unsheathe = Animator.StringToHash("tDaggerUnsheathe");
		sheathe = Animator.StringToHash("tDaggerSheathe");
		slash = Animator.StringToHash("tSlash");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
			anim.SetTrigger(jump);
		}
		
		if(Input.GetKeyDown(KeyCode.E)) {
			anim.SetTrigger(dodge);
		}
		
		if(Input.GetKeyDown(KeyCode.F)) {
			anim.SetTrigger(unsheathe);
			daggerOut = true;
		}
		
		if(daggerOut && Input.GetMouseButtonDown(0)) {
			anim.SetTrigger(slash);
		}
		
		if(Input.GetKeyDown(KeyCode.R)) {
			anim.SetTrigger(sheathe);
			daggerOut = false;
		}
		
		if(Input.GetKey(KeyCode.W)) {
			anim.SetBool(run, true);
		} else {
			anim.SetBool(run, false);
		}
    }
}
