using UnityEngine;

public class FindPlayer : MonoBehaviour
{
    [SerializeField] private GameObject playerGameObject;
    void Start()
    {
        playerGameObject = GameObject.Find("Player 1");
        if (playerGameObject != null)
        {
            Debug.Log("Found player " + playerGameObject + " GameObjects in the scene"); 
        }
        else if(playerGameObject == null)
        {
            Debug.Log("playerGameObject is null");
        }
    }



}
