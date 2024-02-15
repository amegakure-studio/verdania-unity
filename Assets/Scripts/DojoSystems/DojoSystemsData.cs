using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DojoSystemsData", menuName = "ScriptableObjects/DojoSystemsData", order = 3)]
public class DojoSystemsData : ScriptableObject
{
    public string mapSystemAdress;
    public string farmSystemAdress;
    public string playerSystemAdress;
    public string interactSystemAdress;
}

