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
                instance.ResetAnimationsExcept("run","dodge", "wielding");
                anim.SetTrigger(jump);
            }
        }
    }

    void SetSlashAnim()
    {
        instance.ResetAnimationsExcept("slash", "wielding");
        anim.SetTrigger(slash);
        
    }

    void SetRunAnim(Vector2 input, Vector2 inputRaw)
    {
        //if animation already playing, don't play run animation. We don't want it to switch to running while mid-dodge, or mid-jump animation
        if (controller.isGrounded && !IsAnimationRunningExcept("wielding"))
        {
            anim.SetBool(run, true);
        }
    }

    void SetDodgeAnim()
    {
        if (!IsAnimationRunning("Jumping_Anim"))
        {
            //we dont want to reset wielding to false if the player is still holding a weapon
            instance.ResetAnimationsExcept("dodge", "run", "wielding");
            if (controller.isGrounded)
            {
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
        GUI.Label(new Rect(20, 100, 200, 20), "CTRL: Dodge");
        GUI.Label(new Rect(20, 120, 200, 20), "B: Open/Close Inventory");
        GUI.Label(new Rect(20, 140, 200, 20), "E: Pickup Item");
        GUI.Label(new Rect(20, 160, 200, 20), "1,2,3: Switch hotbar index");
    }

    public void ResetAnimations()
    {
        foreach (AnimatorControllerParameter parameter in instance.anim.parameters)
        {
            instance.anim.SetBool(parameter.name, false);
        }
    }
    public void ResetAnimationsExcept(params string[] names)
    {
        foreach (AnimatorControllerParameter parameter in instance.anim.parameters)
        {
            //for each animation...
            bool unset = true;
            for (int i = 0; i < names.Length; i++)
                //for each input argument...
            {
                //if parameters name was in argument list, don't unset it
                if(parameter.name == names[i])
                {
                    unset = false;
                }
                
            }
            //if wasn't found in argument list, set to false
            if (unset)
                instance.anim.SetBool(parameter.name, false);
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

    public bool IsAnimationRunningExcept(string name)
    {
        foreach (AnimatorControllerParameter parameter in instance.anim.parameters)
        {
            if (instance.anim.GetBool(parameter.name) && parameter.name != name)
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
        //I can't set this to null if length is zero :( clipInfo can never be set to null
        if (instance.anim.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            clipInfo = instance.anim.GetCurrentAnimatorClipInfo(0)[0];

        }
    }
}
