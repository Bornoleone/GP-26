using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class LectureScript : MonoBehaviour
{
    public KeyCode useKey = KeyCode.E;
    public Transform player;
    public GameObject cube;
    private float spawnOffset = 1.5f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(useKey))
        {

            cube.transform.position = new Vector3(player.position.x, player.position.y - spawnOffset, player.position.z);
        }
    }
}
