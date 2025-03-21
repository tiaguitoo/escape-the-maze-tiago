using UnityEngine;

public class RandomMaterialApplier : MonoBehaviour
{
    public Material[] materials; // Assign this array in the inspector

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        Material randomMaterial = materials[Random.Range(0, materials.Length)];
        renderer.material = randomMaterial;
    }
}
