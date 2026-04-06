using UnityEngine;

internal class Buildable
{
    protected Vector3 buildablePosition;
    protected Vector3 buildableRotation;
    protected Vector3 buildableScale;
    protected string buildableName;
    protected Color buildableColor;
    protected Renderer buildableRenderer;
    protected Rigidbody buildableRigidBody;
    protected Material buildableMaterial;
    protected GameObject buildable;
    protected GameObject[] buildableGameObjects;
    protected Buildable(string name, int mode)
    {
        SetupBuildable();
        GetBuildableFromName(name);
    }
    protected virtual void SetupBuildable()
    {
        buildableGameObjects = Resources.LoadAll<GameObject>("BuildPrefabs");
        if (buildableGameObjects.Length == 0) { Debug.Log("Resources folder missing prefabs"); }
    }
    protected virtual GameObject GetBuildableFromName(string name)
    {
        foreach (GameObject gameObject in buildableGameObjects)
        {
            if (gameObject.name == name) return gameObject;
        }
        return null;
    }
    protected virtual GameObject ChangeMaterial(GameObject gameObject, int mode)// mode 0 = opaque , mode 3 = transparent
    {
        switch (mode)
        {
            case 3: gameObject.GetComponent<Renderer>().material.SetFloat("mode", 3); break;
            case 1: gameObject.GetComponent<Renderer>().material.SetFloat("mode", 1); break;
        }
        return gameObject;
    }
}
