using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPower : MonoBehaviour
{
	private Text _Text;
	private void Start()
	{
		_Text = GetComponent<Text>();
	}
	private void Update()
	{
		string result = "";
		for (int i = 0; i < PlayerStat.Abilities.Length; i++)
		{
			if (PlayerStat.Abilities[i] is ActiveAbility)
			{
				result += i+1+" - "+PlayerStat.Abilities[i] + " " + PlayerStat.Abilities[i].UITimer + "\n";
			}
			else if (PlayerStat.Abilities[i] is PassiveAbility)
			{
				result += i+1 + " - " + PlayerStat.Abilities[i] + " PASSIVE\n";
			}
		}
		_Text.text = result;
	}
}
