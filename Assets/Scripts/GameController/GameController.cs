using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	public enum Quest //add your states here
	{
		Tutorial,
		Main
	};

	public static QuestList<Quest> Quests = new QuestList<Quest>();

	/*Put state logic in Update()
	 * E.g. Spawn enemy, can't use ability, can't swim
	 * */
    private void Update()
    {
		if (Quests.Contains(Quest.Tutorial))
		{
			//tutorial logic here
		}
    }
}
