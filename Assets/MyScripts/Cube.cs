using Unity.FPS.Game;
using UnityEngine;
using UnityEngine.UIElements;

internal class Cube : BuildableObject
{

    public Cube(Vector3 scale, Color color) : base()//object constructor´, Base() method constructs parent class before this child class
    {
        buildableGameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);//assigns primitive game object type cube to be buildableGameObject
        objectScale = scale;// Set the scale from constructor parameter
        buildableGameObject.GetComponent<Renderer>().material.color = color;//assigns coming parameter's Color to be game object's color
        buildableGameObject.AddComponent<Health>();
        buildableGameObject.AddComponent<Damageable>();
        buildableGameObject.AddComponent<Destructable>();
    }


    public override GameObject SpawnGameObject(Vector3 position)
    {
        
        rb = buildableGameObject.AddComponent<Rigidbody>();// Add physics component to the cube
        buildableGameObject.transform.position = GridSnap(position);// Set spawn position
        objectPosition = GridSnap(position);// saving position to objectPosition variable
        buildableGameObject.transform.localScale = objectScale;// Apply scale from constructor
        Debug.Log("Spawned Game object: " + buildableGameObject + " In position: " + position + " scale: " + objectScale);
        return buildableGameObject;//returns the game object
    }
    public override Vector3 GridSnap(Vector3 position)
    {
        float snappedX = Mathf.Round(position.x / gridSize) * gridSize;
        float snappedZ = Mathf.Round(position.z / gridSize) * gridSize;
        float snappedY = Mathf.Round(position.y / gridSize) * gridSize;
        Debug.Log("grid snap x: " + snappedX + "y: " + snappedY + "z: " + snappedZ);
        return new Vector3(snappedX, snappedY, snappedZ);
    }
    public override void SetGridSize(float grid) 
    { 
        gridSize = grid;
    }

}
