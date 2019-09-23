using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDash : Ability
{
    public Vector3 moveDirection;
    public const float maxDashTime = 1.0f;
    public float dashDistance = 10;
    public float dashStoppingSpeed = 0.1f;
    float currentDashTime = maxDashTime;
    float dashSpeed = 8;

    CharacterController controller;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {       
        AnimatorClipInfo info = AnimationManager.instance.clipInfo;
        if (controller.isGrounded && info.clip.name != "Dodge_Dive_anim")
        {
            if (Input.GetKeyDown(KeyCode.Q)) //Q button
            {
                GetComponent<PlayerSoundManager>().StopSounds();
                GetComponent<PlayerSoundManager>().
                    SetSoundOfName(PlayerSoundManager.SoundTypes.dash);
                currentDashTime = 0;
            }
            if (currentDashTime < maxDashTime)
            {
                moveDirection = transform.forward * dashDistance;
                currentDashTime += dashStoppingSpeed;
                controller.Move(moveDirection * Time.deltaTime * dashSpeed);
            }
            else
            {
                moveDirection = Vector3.zero;
            }
            
        }

    }
}
