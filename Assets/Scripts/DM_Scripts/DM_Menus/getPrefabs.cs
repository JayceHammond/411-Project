using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class getPrefabs : MonoBehaviour
{
    public String path = "Assets/Prefabs/DMPrefabs/DMObjects";
    private GameObject prefab;

    public Array getAllPrefabs(){
        DirectoryInfo DMPrefabObjects = new DirectoryInfo(path);
        FileInfo[] AllPrefabs = DMPrefabObjects.GetFiles("*.prefab");

        return AllPrefabs.ToArray();
    }
}
