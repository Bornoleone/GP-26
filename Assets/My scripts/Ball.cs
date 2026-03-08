using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.UIElements;

internal class Ball : BuildableObject
{
    private GameObject ball;
    private Rigidbody rb;

    #region ExampleProperties
    //properties
    /*public Vector3 BallPosition
    {//example for me
        get {return ballPosition; }
        set { BallPosition = value; }
    }*/
    #endregion
    
    public Ball(Vector3 scale, Color color) : base()
    {
        ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        objectScale = scale;
        buildableGameObject = ball;
        objectRenderer = ball.GetComponent<Renderer>();
        objectPosition = ball.transform.position;
        ball.GetComponent<Renderer>().material.color = color;
    }
    
    
    public override GameObject SpawnGameObject(Vector3 position)
    {
        rb = ball.AddComponent<Rigidbody>();
        
        ball.transform.position = position;
        ball.transform.localScale = objectScale;
        Debug.Log("Spawned Game object: " + ball + " In position: " + position + " scale: " + objectScale );
        return ball;
    }
    
}
