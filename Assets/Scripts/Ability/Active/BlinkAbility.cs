using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkAbility : ActiveAbility
{
	private float _Timer = 0f;
	private float _CoolDownTime = 5;
	public override void End()
	{
	}

	public override void Init()
	{
		_Timer = 0;
	}

	public override void Run()
	{
		if (Input.GetAxisRaw("Fire1") == 1f && _Timer >= _CoolDownTime && PlayerStat.SelectedAbility is BlinkAbility)
		{
			if (ThirdPersonCamera.LookingAtDistance >= 1 && ThirdPersonCamera.LookingAtDistance <= 100 /*<----change distance here*/)
			{
				_Timer = 0;
                Vector3 blinkLocation = ThirdPersonCamera.LookingAtPoint;
				blinkLocation += Vector3.up;
				Transform playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
                if (blinkLocation.y - playerTransform.position.y < 8) /*<---- change vertical distance here*/
                {
                    playerTransform.position = blinkLocation;
                }
                else
                {
                    Debug.Log("Can't blink");
                    _Timer = _CoolDownTime;
                }
            }
            Debug.Log(ThirdPersonCamera.LookingAtDistance);
		}
		_Timer += Time.deltaTime;
		if (_Timer >= _CoolDownTime)
		{
			_Timer = _CoolDownTime;
		}
		UITimer = _CoolDownTime - _Timer;
	}

	public override string ToString()
	{
		return "Blink";
	}
}
