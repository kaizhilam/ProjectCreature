using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestList<T>
{
	private List<T> _Quests = new List<T>();

	public int Count
	{
		get { return _Quests.Count; }
	}

	public bool Contains(T quest)
	{
		return _Quests.Contains(quest);
	}

	public void Add(T quest)
	{
		_Quests.Add(quest);
	}

	public bool Remove(T quest)
	{ 
		return _Quests.Remove(quest);
	}
}
