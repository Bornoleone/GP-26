using UnityEngine;

public class FindAllGameObjects : MonoBehaviour
{
    public GameObject[] allGameObjects;
    void Start()
    {
        allGameObjects = FindObjectsByType<GameObject>(FindObjectsSortMode.None);//Finds all GameObjects in the scene
        Debug.Log("Found "+ allGameObjects.Length+ " GameObjects in the scene");
    }

    
    
}
