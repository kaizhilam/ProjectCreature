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
        InputManager.instance.Space += SetJumpAnim;
        InputManager.instance.LeftClick += SetSlashAnim;
        InputManager.instance.Movement += SetRunAnim;
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

    void SetJumpAnim()
    {
        if (controller.isGrounded)
        {
            anim.SetTrigger(jump);
        }
    }

    void SetSlashAnim()
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

    void SetRunAnim(Vector2 input, Vector2 inputRaw)
    {
        print("set run anim");
	    anim.SetBool(run, true);

        if (Input.GetKeyDown(KeyCode.E) && controller.isGrounded)
        {
            anim.SetTrigger(dodge);
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
