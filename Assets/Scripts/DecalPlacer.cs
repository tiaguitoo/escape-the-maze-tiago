using UnityEngine;

public class DecalPlacer : MonoBehaviour
{
    public GameObject[] decalPrefabs; // Assign your decal prefabs in the inspector.

    void Start()
    {
        PlaceDecals("UpWall", 180f, 0.1f);
        PlaceDecals("DownWall", 0f, 0.1f);
        PlaceDecals("RightWall", -90f, -0.1f);
        PlaceDecals("LeftWall", 90f, 0.1f);
    }

    void PlaceDecals(string wallTag, float rotationY, float localZPosition)
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag(wallTag);

        foreach (GameObject wall in walls)
        {
            if(wall.transform.childCount == 0) // Check if the wall has no child decals yet
            {
                // Randomly pick a decal prefab.
                GameObject decalPrefab = decalPrefabs[Random.Range(0, decalPrefabs.Length)];
                // Instantiate the decal as a child of the wall with the wall's position.
                GameObject decal = Instantiate(decalPrefab, wall.transform.position, Quaternion.Euler(0f, rotationY, 0f), wall.transform);

                // Adjust decal's local position.
                decal.transform.localPosition = new Vector3(
                    Random.Range(-1f, 1f), // Random X local position
                    Random.Range(1.2f, 2f), // Random Y local position
                    localZPosition // Fixed local Z position based on the wall type
                );
            }
        }
    }
}
