using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 
public class StartGame : MonoBehaviour {
 
    public GameObject theTree;
    public GameObject theBush;
    float offset;
    List<TreeInstance> SavedTrees;
 
    // Use this for initialization
    void Start () {

        theTree = Resources.Load<GameObject>("Prefabs/Tall Tree");
        offset = theTree.GetComponent<SpriteRenderer>().size.y/2;
        // Grab the Ground's terrain data
        TerrainData theGround;
        theGround = GameObject.Find("Ground").GetComponent<Terrain>().terrainData;
        Debug.Log(offset);
        // For every tree on the island
        foreach (TreeInstance tree in theGround.treeInstances) {
            // Find its local position scaled by the terrain size (to find the real world position)
            Vector3 worldTreePos = Vector3.Scale(tree.position, theGround.size) + Terrain.activeTerrain.transform.position;
            worldTreePos.y = worldTreePos.y + offset;
            Instantiate (theTree, worldTreePos, Quaternion.identity); // Create a prefab tree on its pos
        }
        
        // Then delete all trees on the island
        List<TreeInstance> newTrees = new List<TreeInstance>(0);
        SavedTrees = new List<TreeInstance>(theGround.treeInstances.Length);
        SavedTrees = theGround.treeInstances.ToList();
        theGround.treeInstances = newTrees.ToArray();
        
    }

    void OnApplicationQuit(){
        TerrainData theGround;
        theGround = GameObject.Find("Ground").GetComponent<Terrain>().terrainData;

        theGround.treeInstances = SavedTrees.ToArray();
    }
}
 