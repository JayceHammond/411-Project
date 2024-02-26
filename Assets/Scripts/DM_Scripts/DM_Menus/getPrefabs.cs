using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class getPrefabs : MonoBehaviour
{

    public GameObject prefab;

    public Array getAllPrefabs(){
        return getAllBuildings().Concat(getAllFoilage()).Concat(getAllCharacters()).ToArray();
    }

    private FileInfo[] getAllBuildings(){
        DirectoryInfo DMPrefabObjects = new DirectoryInfo("Assets/Prefabs/DMPrefabs/DMObjects/Buildings");
        FileInfo[] AllPrefabs = DMPrefabObjects.GetFiles("*.prefab");

        return AllPrefabs;
    }

    private FileInfo[] getAllFoilage(){
        DirectoryInfo DMPrefabObjects = new DirectoryInfo("Assets/Prefabs/DMPrefabs/DMObjects/Foilage");
        FileInfo[] AllPrefabs = DMPrefabObjects.GetFiles("*.prefab");

        Debug.Log(AllPrefabs.Length);

        return AllPrefabs;
    }

    private FileInfo[] getAllCharacters(){
        DirectoryInfo DMPrefabObjects = new DirectoryInfo("Assets/Prefabs/DMPrefabs/DMObjects/Characters");
        FileInfo[] AllPrefabs = DMPrefabObjects.GetFiles("*.prefab");

        return AllPrefabs;
    }
}
