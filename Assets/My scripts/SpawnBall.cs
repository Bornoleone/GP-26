using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField] private KeyCode useKey = KeyCode.T;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useKey))
        {
            SpawningBall();
        }
    }
    public void SpawningBall()
    {
        Ball ball = new Ball(Vector3.one);
        Debug.Log("new ball object created" + ball);
        ball.CreateBall(Vector3.one);
    }
 

}
