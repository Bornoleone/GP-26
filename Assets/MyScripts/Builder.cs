using Unity.Play.Publisher.Editor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

internal class Builder : Buildable
{
    private GameObject currentPreviewObject;
    private Camera mainCamera;
    private float gridSize = 1f;

    
    public Builder(string buildableName, int mode) : base(buildableName, mode)
    {
        
    }
    
    
    public Vector3 GridSnap(Vector3 position)
    {
        float snappedX = Mathf.Round(position.x / gridSize) * gridSize;
        float snappedZ = Mathf.Round(position.z / gridSize) * gridSize;
        float snappedY = Mathf.Round(position.z / gridSize) * gridSize;
        return new Vector3(snappedX, snappedY, snappedZ);
    }
    public void SpawnGameObject(Vector3 position, string name, Quaternion quaternion)
    {
        GridSnap(position);
        GameObject spawnedObject = Object.Instantiate(GetBuildableFromName(name), GridSnap(position), quaternion);//Quaternion.Euler(-90f, 0, 0)
        
    }
}
