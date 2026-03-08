using UnityEngine;
using UnityEngine.UIElements;

internal class Cube : BuildableObject
{
    private GameObject cube;
    private Rigidbody rb;

    #region ExampleProperties
    //properties
    /*public Vector3 BallPosition
    {//example for me
        get {return ballPosition; }
        set { BallPosition = value; }
    }*/
    #endregion

    public Cube(Vector3 scale, Color color) : base()
    {
        cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        objectScale = scale;
        buildableGameObject = cube;
        objectRenderer = cube.GetComponent<Renderer>();
        objectPosition = cube.transform.position;
        cube.GetComponent<Renderer>().material.color = color;
    }


    public override GameObject SpawnGameObject(Vector3 position)
    {
        
        rb = cube.AddComponent<Rigidbody>();
        cube.transform.position = position;
        cube.transform.localScale = objectScale;
        Debug.Log("Spawned Game object: " + cube + " In position: " + position + " scale: " + objectScale);
        return cube;
    }

}
