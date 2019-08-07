using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashMovement : MonoBehaviour
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
           //player can only dash when on the ground and not dodging
            AnimatorClipInfo info = AnimationManager.instance.clipInfo;
            if (controller.isGrounded && info.clip.name != "Dodge_Dive_anim")
            {
                if (Input.GetKeyDown(KeyCode.Q)) //Q button
                 {
                    currentDashTime = 0;
                 }
                if (currentDashTime < maxDashTime)
                 {
                    moveDirection = transform.forward * dashDistance;
                    currentDashTime += dashStoppingSpeed;
                 }
                 else
                 {
                     moveDirection = Vector3.zero;
                 }
             controller.Move(moveDirection * Time.deltaTime * dashSpeed);
             }
            
        }
    
}
