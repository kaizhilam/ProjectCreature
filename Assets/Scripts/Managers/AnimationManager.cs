using UnityEngine;

public class AnimationManager : MonoBehaviour
{

    public static AnimationManager instance = null;
	public Animator anim;
    CharacterController controller;
    GameObject _player;
    public AnimatorClipInfo clipInfo;
	
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
        InputManager.instance.CTRLKey += SetDodgeAnim;
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
        if (!IsAnimationRunning("Dodge_Dive_anim"))
        {
            //we don't want to play jump animation if its already playing
            if (controller.isGrounded)
            {
                instance.ResetAnimations();
                anim.SetTrigger(jump);
            }
        }
    }

    void SetSlashAnim()
    {
        instance.ResetAnimationsExcept("slash");
        anim.SetTrigger(slash);
        
    }

    void SetRunAnim(Vector2 input, Vector2 inputRaw)
    {
        //if animation already playing, don't play run animation. We don't want it to switch to running while mid-dodge, or mid-jump animation
        if (controller.isGrounded && !IsAnimationRunning())
        {
            anim.SetBool(run, true);
        }
    }

    void SetDodgeAnim()
    {
        if (!IsAnimationRunning("Jumping_Anim"))
        {
            instance.ResetAnimationsExcept("dodge");
            if (controller.isGrounded)
            {
                print("running dodge anim");
                anim.SetTrigger(dodge);
            }
        }
    }

    void UnsetRunAnim()
    {
        anim.SetBool(run, false);
    }
	
	void OnGUI()
	{
		GUI.Label(new Rect(20, 20, 200, 20), "CTRL: Dodge");
		GUI.Label(new Rect(20, 40, 200, 20), "WASD: Move");
		GUI.Label(new Rect(20, 60, 200, 20), "Space: Jump");
		GUI.Label(new Rect(20, 80, 200, 20), "MouseLB: Attack");
	}

    public void ResetAnimations()
    {
        foreach (AnimatorControllerParameter parameter in instance.anim.parameters)
        {
            instance.anim.SetBool(parameter.name, false);
        }
    }
    public void ResetAnimationsExcept(string name)
    {
        foreach (AnimatorControllerParameter parameter in instance.anim.parameters)
        {
            if(parameter.name != name)
            {
                instance.anim.SetBool(parameter.name, false);
            }
        }

    }

    public bool IsAnimationRunning()
    {
        foreach (AnimatorControllerParameter parameter in instance.anim.parameters)
        {
            if (instance.anim.GetBool(parameter.name))
            {
                return true;
            }
        }
        return false;
    }

    //returns true is paramater string equals animators current animation
    public bool IsAnimationRunning(string name)
    {
        return instance.clipInfo.clip.name == name;
    }

    public void Update()
    {
        clipInfo = instance.anim.GetCurrentAnimatorClipInfo(0)[0];
    }
}
