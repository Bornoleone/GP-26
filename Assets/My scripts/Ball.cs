using UnityEngine;

public class Ball
{
    public Vector3 ballPosition;
    public Vector3 ballRotation;
    public Vector3 ballScale;
    public string ballName;
    public Color ballColor;

    public GameObject ball;
    public Rigidbody rb;

    public Ball(Vector3 scale)
    {
        ballScale = scale;
    }

    public GameObject CreateBall(Vector3 position)
    {
        ball = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        rb = ball.AddComponent<Rigidbody>();
        ball.transform.position = position;
        ball.transform.localScale = ballScale;
        Debug.Log("Spawned Game object: " + ball + " In position: " + position + " scale: " + ballScale );
        return ball;
    }
}
