using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UIElements;

internal class Ramp : BuildableObject
{
    
    public Ramp(Vector3 scale) 
    {
        //buildableGameObject
        GameObject Prefab = Resources.Load<GameObject>("BuildPrefabs/Ramp");
        objectMeshFilter = Resources.Load<MeshFilter>("MeshFilters/Ramp");
        Prefab.transform.localScale = scale;
        if (buildableGameObject != null)
        {
            Debug.Log("Ramp Prefab found");
        }
        else
        {
            Debug.Log("Ramp Prefab not found");
        }
        objectRenderer.gameObject.AddComponent<Renderer>();
        objectMeshFilter.gameObject.AddComponent<MeshFilter>();
        //objectRotation
        //objectRenderer.material.
        buildableObjectName = "Ramp";
        objectScale = scale;
        buildableGameObject.AddComponent<Health>();

    }
    public override GameObject SpawnGameObject(Vector3 position)
    {
        GameObject spawnedObject = Object.Instantiate(buildableGameObject, position, Quaternion.Euler(-90f, 0, 0));
        buildableGameObject.transform.localScale = objectScale;
        objectPosition = position;// saving position to objectPosition variable
        return buildableGameObject;
    }
    
}
