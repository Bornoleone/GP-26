using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

internal class Ball : BuildableObject
{

    #region ExampleProperties
    //properties
    /*public Vector3 BallPosition
    {//example for me
        get {return ballPosition; }
        set { BallPosition = value; }
    }*/
    #endregion
    
    public Ball(Vector3 scale, Color color) : base()//object constructor
    {
        buildableGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);//assigns primitive game object type sphere to be buildableGameObject
        objectScale = scale; // Set the scale from constructor parameter
        buildableGameObject.GetComponent<Renderer>().material.color = color;//assigns coming parameter's Color to be game object's color
    }
    
    
    public override GameObject SpawnGameObject(Vector3 position)
    {
        rb = buildableGameObject.AddComponent<Rigidbody>();// Add physics component to the Sphere
        buildableGameObject.transform.position = position;// Set spawn position
        objectPosition = position;// saving position to objectPosition variable
        buildableGameObject.transform.localScale = objectScale;// Apply scale from constructor
        Debug.Log("Spawned Game object: " + buildableGameObject + " In position: " + position + " scale: " + objectScale );
        return buildableGameObject; //returns the gameobject
    }
    
}
