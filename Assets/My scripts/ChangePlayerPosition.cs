using UnityEngine;

public class ChangePlayerPosition : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Player prefab's transform
    [SerializeField] private Vector3 teleportLocation; //coordinates where player will move
    
    private void OnTriggerEnter(Collider other)
    {// When player's collider goes in trigger area player's position wil change to teleportLocation coordinates
        ChangePosition();
    }
    void ChangePosition()
    {//changes player's transform component position coordinates to teleportLocation coordinates
        playerTransform.position = teleportLocation;
    }
}
