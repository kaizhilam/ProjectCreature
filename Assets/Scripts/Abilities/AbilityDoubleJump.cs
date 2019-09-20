using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDoubleJump : Ability
{
    //set variable
    [HideInInspector]
    public GameObject wingPrefab;
    public int MAX_DJUMP = 1;
    public int currentjump;
    private CharacterController _Controller;
    public float JumpHeight = 25f;
    private Vector3 _JumpAmount = new Vector3(0, 0, 0);
    private bool DJ;//use boolean to make state
    // Start is called before the first frame update
    private float _Gravity;
    void Start()
    {
        InputManager.instance.Space += DJump;//call the double jump
        _Controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        _Gravity = PlayerMovement.GetGravity;
        //only work when the statement fulfilled 
        if (DJ)
        {
            //jump
            _JumpAmount.y += _Gravity * Time.deltaTime;
            _Controller.Move(_JumpAmount * Time.deltaTime);
        }

        if (_Controller.isGrounded == true)// reset the state when player is grounded
        {
            currentjump = 0;
            DJ = false;
        }
    }
    void DJump()
    {
        if (_Controller.isGrounded == false && currentjump < MAX_DJUMP)//only trigger when player not on ground
        {
            GetComponent<PlayerSoundManager>().StopSounds();
            GetComponent<PlayerSoundManager>().SetSoundOfName(PlayerSoundManager.SoundTypes.jump);
            _JumpAmount.y = JumpHeight;
            currentjump++;
            DJ = true;
            StartCoroutine(FlapWings());
        }
    }

    IEnumerator FlapWings()
    {

        GameObject wing1 = Instantiate(wingPrefab, Vector3.zero, Quaternion.identity);
        GameObject wing2 = Instantiate(wingPrefab, Vector3.zero, Quaternion.identity);
        GameObject spine = GetComponentInChildren<spineScript>().gameObject;
        wing1.transform.parent = spine.transform;
        wing1.transform.localPosition = new Vector3(0.0049f, 0.0f, 0.0f);
        wing1.transform.localScale = Vector3.one * 0.05f;
        wing1.transform.localRotation = Quaternion.Euler(new Vector3(-92.782f, 0.0f, 0.0f));
        wing2.transform.parent = spine.transform;
        wing2.transform.localPosition = new Vector3(0.007f, -0.012f, -0.037f);
        wing2.transform.localScale = Vector3.one * 0.05f;
        wing2.transform.localRotation = Quaternion.Euler(new Vector3(-69.59f, 179.2f, -2.95f));
        //for (int i = 0; i < 60; i++)
        //{

        //    wing1.transform.RotateAround(transform.TransformPoint(spine.transform.position), Vector3.left, 5.0f);
        //    yield return new WaitForEndOfFrame();
        //}
        //yield return new WaitForSeconds(0.1f);
        //for (int i = 0; i < 60; i++)
        //{
        //    wing1.transform.RotateAround(transform.TransformPoint(spine.transform.position), Vector3.left, -5.0f);
        //    yield return new WaitForEndOfFrame();
        //}
        yield return new WaitForSeconds(2.0f);
        Destroy(wing1);
        Destroy(wing2);
        

    }
}
