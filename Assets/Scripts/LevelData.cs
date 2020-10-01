using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "Create New Level", order = 1)]
public class LevelData : ScriptableObject
{
    public GameObject[] rampList;
    public Ramp[] ramps;
}

