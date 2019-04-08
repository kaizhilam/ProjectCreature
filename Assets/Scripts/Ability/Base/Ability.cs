using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
	public float UITimer = 0;
	public abstract void Init();
	public abstract void Run();
	public abstract void End();
}

public class NullAbility : PassiveAbility
{
	public override void End()
	{
	}

	public override void Init()
	{
	}

	public override void Run()
	{
	}

	public override string ToString()
	{
		return "Empty";
	}
}
