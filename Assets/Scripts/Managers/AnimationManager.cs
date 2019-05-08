using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public static AnimationManager instance = null;
	Animator anim;
    CharacterController controller;
    BoxCollider daggerHitbox;
    GameObject _player;
	
	int idle;
	int jump;
	int run;
	int dodge;
	int sheathe;
	int unsheathe;
	int slash;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _player = GameObject.Find("Player");
		anim = _player.GetComponent<Animator>();
        controller = _player.GetComponent<CharacterController>();
        daggerHitbox = _player.GetComponent<BoxCollider>();
		
		idle = Animator.StringToHash("idle");
		jump = Animator.StringToHash("jump");
		run = Animator.StringToHash("run");
		dodge = Animator.StringToHash("dodge");
		slash = Animator.StringToHash("slash");
	}

    void SetJump()
    {
        if (controller.isGrounded)
        {
            anim.SetTrigger(jump);
        }
    }

    void Slash()
    {
        anim.SetTrigger(slash);
        Collider[] colliders = Physics.OverlapBox(daggerHitbox.center, daggerHitbox.size);
        for (int i = 0; i < colliders.Length; i++)
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

    void FixedUpdate()
    {
		
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
	
	void OnGUI()
	{
		GUI.Label(new Rect(20, 20, 200, 20), "E: Dodge");
		GUI.Label(new Rect(20, 40, 200, 20), "WASD: Move");
		GUI.Label(new Rect(20, 60, 200, 20), "Space: Jump");
		GUI.Label(new Rect(20, 80, 200, 20), "MouseLB: Attack");
	}
}
