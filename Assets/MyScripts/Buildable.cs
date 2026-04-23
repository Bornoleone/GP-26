using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

internal class Buildable
{
    protected Vector3 buildablePosition;
    protected Vector3 buildableRotation;
    protected Vector3 buildableScale;
    protected string buildableName;
    protected Color buildableColor;
    protected Renderer buildableRenderer;
    protected Rigidbody buildableRigidBody;
    protected Material[] buildableMaterials;
    protected GameObject buildable;
    protected GameObject[] buildableGameObjects;
    protected Buildable(string name, string mode)
    {
        SetupBuildable();
        GetPrefabFromName(name);
        //ChangeColor(color);
        GetMaterialFromName(mode);
    }
    protected virtual void SetupBuildable()
    {
        buildableGameObjects = Resources.LoadAll<GameObject>("BuildPrefabs");
        buildableMaterials = Resources.LoadAll<Material>("MyMaterials");
        if (buildableGameObjects.Length == 0) { Debug.Log("Resources folder missing prefabs"); }
        if (buildableMaterials.Length == 0) { Debug.Log("Resources folder missing materials"); }
    }
    protected virtual GameObject GetPrefabFromName(string name)
    {
        foreach (GameObject gameObject in buildableGameObjects)
        {
            if (gameObject.name == name) return gameObject;
        }
        return null;
    }
    private void ChangeColor(Color color)
    {
        buildable.GetComponent<Renderer>().material.color = color;
        buildableColor = color;
    }
    public virtual Material GetMaterialFromName(string name)
    {
        foreach (Material material in buildableMaterials)
        {
            Debug.Log("Material: "+ material.name + " Loaded");
            if (material.name == name) return material;
        }
        return null;
    }
    public virtual void ChangeMaterial(GameObject gameObject, string mode)// mode 0 = opaque , mode 3 = transparent
    {
        
        switch (mode)
        {
            case "transparent":
                switch (gameObject.name)
                {
                    case "Ramp": gameObject.GetComponent<Renderer>().material = GetMaterialFromName("Transparent_Blue_mat"); break;
                    case "Foundation": gameObject.GetComponent<Renderer>().material = GetMaterialFromName("Transparent_Red_mat"); break;
                    case "WallDoorway": gameObject.GetComponent<Renderer>().material = GetMaterialFromName("Transparent_Green_mat"); break;
                    case "Wall": gameObject.GetComponent<Renderer>().material = GetMaterialFromName("Transparent_Green_mat"); break;
                    
                }
                break;
            case "opaque":
                switch (gameObject.name)
                {
                    case "Ramp": gameObject.GetComponent<Renderer>().material = GetMaterialFromName("Blue_mat"); break;
                    case "Foundation": gameObject.GetComponent<Renderer>().material = GetMaterialFromName("Red_mat"); break;
                    case "WallDoorway": gameObject.GetComponent<Renderer>().material = GetMaterialFromName("Green_mat"); break;
                    case "Wall": gameObject.GetComponent<Renderer>().material = GetMaterialFromName("Green_mat"); break;
                }
                break;
        }
        Debug.Log("Material mode changed to: "+ mode);
        
    }
}
