using UnityEngine;
using UnityEngine.UIElements;

internal class Ramp : BuildableObject
{
    void Start()
    {
        buildableGameObject = Resources.Load<GameObject>("MyPrefabs/BuildPrefabs/Ramp");

        if (buildableGameObject != null)
        {
            Debug.Log("Ramp Prefab found");
        }
        else
        {
            Debug.Log("Ramp Prefab not found");
        }
    }
    public Ramp() : base()
    {

    }
    public override GameObject SpawnGameObject(Vector3 position)
    {
        throw new System.NotImplementedException();
    }
}
