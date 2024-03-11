using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombineSkinnedMeshes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Get references to all Skinned Mesh Renderers
        SkinnedMeshRenderer[] skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        // Combine meshes
        Mesh combinedMesh = new Mesh();
        CombineInstance[] combineInstances = new CombineInstance[skinnedMeshRenderers.Length];

        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            combineInstances[i].mesh = skinnedMeshRenderers[i].sharedMesh;
            combineInstances[i].transform = skinnedMeshRenderers[i].transform.localToWorldMatrix;
            // Disable original Skinned Mesh Renderer
            skinnedMeshRenderers[i].enabled = false;
        }

        combinedMesh.CombineMeshes(combineInstances, true, false);

        // Create a new GameObject to hold the combined mesh
        GameObject combinedGameObject = new GameObject("CombinedPlayerMesh");

        // Add Mesh Filter and Mesh Renderer
        MeshFilter meshFilter = combinedGameObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = combinedMesh;

        MeshRenderer meshRenderer = combinedGameObject.AddComponent<MeshRenderer>();
        meshRenderer.sharedMaterials = skinnedMeshRenderers[0].sharedMaterials; // Assuming all Skinned Mesh Renderers share materials

        // Add a collider (e.g., BoxCollider, SphereCollider, etc.)
        combinedGameObject.AddComponent<BoxCollider>();
    }
}
