using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public string Name;
    public string Description;
    public List<Quest> Prerequisite;
    public bool isCompleted;
    public bool isprerequisite;
}
