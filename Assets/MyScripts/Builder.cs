using System.Collections.Generic;
using Unity.Play.Publisher.Editor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

internal class Builder : Buildable
{
    private GameObject currentPreviewObject;
    private Camera mainCamera;
    private float gridSize = 4f;
    private List<Vector3> usedCoordinates = new List<Vector3>();

    
    public Builder(string buildableName, string mode) : base(buildableName, mode)
    {
        
    }
    
    
    public Vector3 GridSnap(Vector3 position)
    {
        float snappedX = Mathf.Round(position.x / gridSize) * gridSize;
        float snappedZ = Mathf.Round(position.z / gridSize) * gridSize;
        float snappedY = Mathf.Round(position.y / gridSize) * gridSize;
        Debug.Log("grid snap x: "+snappedX + "y: " +snappedY + "z: "+ snappedZ);

        /*if (!AddToCoordinatesList(new Vector3(snappedX, snappedY, snappedZ)))
        {
            return new Vector3(snappedX, snappedY, snappedZ);
        }*/
        return new Vector3(snappedX, snappedY, snappedZ);
    }
    public GameObject SpawnGameObject(Vector3 position, string name, Quaternion quaternion, string mode)
    {
        GridSnap(position);
        GameObject spawnedObject = Object.Instantiate(GetPrefabFromName(name), GridSnap(position), quaternion);//Quaternion.Euler(-90f, 0, 0)
        ChangeMaterial(spawnedObject, "transparent");
        return spawnedObject;
    }
    
    private bool AddToCoordinatesList(Vector3 coordinates)
    {
        if (!usedCoordinates.Contains(coordinates))
        {
            usedCoordinates.Add(coordinates);
            return true;
        }
        else { Debug.Log("Coordinates already used"); return false;  }
    }
}
