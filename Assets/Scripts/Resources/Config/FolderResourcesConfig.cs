using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FolderResourcesConfig", menuName = "ScriptableObjects/FolderResourcesConfig", order = 1)]
public class FolderResourcesConfig : ScriptableObject
{
    public string cropsFolder;
    public string charactersFolder;
    public string mapFolder;
    public string objectsFolder;
    public string npcFolder;
}
