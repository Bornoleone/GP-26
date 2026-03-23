using UnityEngine;
using UnityEngine.UIElements;

internal class Ramp : BuildableObject
{
    
    public Ramp(Vector3 scale) 
    {
        GameObject Prefab = Resources.Load<GameObject>("BuildPrefabs/Ramp");
        buildableGameObject = Prefab;
        Prefab.transform.localScale = scale;
        if (buildableGameObject != null)
        {
            Debug.Log("Ramp Prefab found");
        }
        else
        {
            Debug.Log("Ramp Prefab not found");
        }
        buildableObjectName = "Ramp";
        objectScale = scale;


    }
    public override GameObject SpawnGameObject(Vector3 position)
    {
        GameObject spawnedObject = Object.Instantiate(buildableGameObject, position, Quaternion.Euler(-90f, 0, 0));
        buildableGameObject.transform.localScale = objectScale;
        objectPosition = position;// saving position to objectPosition variable
        return buildableGameObject;
    }
    
}
