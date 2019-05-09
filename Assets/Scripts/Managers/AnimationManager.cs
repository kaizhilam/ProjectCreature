using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public static AnimationManager instance = null;
	Animator anim;
    CharacterController controller;
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
        InputManager.instance.StoppedMoving += UnsetRunAnim;
        InputManager.instance.QKey += SetDodgeAnim;
        _player = GameObject.Find("Player");
		anim = _player.GetComponent<Animator>();
        controller = _player.GetComponent<CharacterController>();

		
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
        if (anim.GetBool(slash) != true)
        anim.SetTrigger(slash);
        
    }

    void SetRunAnim(Vector2 input, Vector2 inputRaw)
    {
        if (controller.isGrounded && anim.GetBool(run)!=true)
        {
            anim.SetBool(run, true);
        }
    }

    void SetDodgeAnim()
    {
        if (controller.isGrounded && anim.GetBool(dodge) != true)
        {
            print("running dodge anim");
            UnsetRunAnim();
            anim.SetTrigger(dodge);
        }
    }

    void UnsetRunAnim()
    {
        if(anim.GetBool(run)==true)
            anim.SetBool(run, false);
    }
	
	void OnGUI()
	{
		GUI.Label(new Rect(20, 20, 200, 20), "Q: Dodge");
		GUI.Label(new Rect(20, 40, 200, 20), "WASD: Move");
		GUI.Label(new Rect(20, 60, 200, 20), "Space: Jump");
		GUI.Label(new Rect(20, 80, 200, 20), "MouseLB: Attack");
	}
}
