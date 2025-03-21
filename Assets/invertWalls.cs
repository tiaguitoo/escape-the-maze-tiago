using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invertWalls : MonoBehaviour
{
    public void Invertwalls(){
        GameObject[] invertWallObjects = GameObject.FindGameObjectsWithTag("invertWall");
            foreach (GameObject obj in invertWallObjects)
            {
                obj.SetActive(false); // This will disable the game objects
            }
    }
}
