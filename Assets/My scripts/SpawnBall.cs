using UnityEngine;

public class SpawnBall : MonoBehaviour
{
    [SerializeField] private KeyCode useKeySmallBall = KeyCode.T;
    [SerializeField] private KeyCode useKeyBigBall = KeyCode.Y;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useKeySmallBall))
        {
            SpawningSmallBall();
        }
        if (Input.GetKeyDown(useKeyBigBall))
        {
            SpawningBigBall();
        }
    }
    public void SpawningSmallBall()
    {
        Ball ballSmall = new Ball(Vector3.one);
        Debug.Log("new ballSmall object created" + ballSmall);
        ballSmall.CreateBall(Vector3.one);
    }
    public void SpawningBigBall()
    {
        Ball ballBig = new Ball(new Vector3(2, 2, 2));
        Debug.Log("new ballSmall object created" + ballBig);
        ballBig.CreateBall(Vector3.one);
    }


}
