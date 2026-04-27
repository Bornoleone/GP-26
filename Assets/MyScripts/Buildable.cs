using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

internal class Buildable : AbstractBuildable
{
    
    protected Buildable(string name/*, string mode*/)
    {
        SetupBuildable();
        GetPrefabFromName(name);
        //ChangeColor(color);
        //GetMaterialFromName(mode);
    }
    private void SetupBuildable()
    {
        buildableGameObjects = Resources.LoadAll<GameObject>("BuildPrefabs");
        buildableMaterials = Resources.LoadAll<Material>("MyMaterials");
        if (buildableGameObjects.Length == 0) { Debug.Log("Resources folder missing prefabs"); }
        if (buildableMaterials.Length == 0) { Debug.Log("Resources folder missing materials"); }

        
    }
    public override GameObject SpawnGameObject(Vector3 position, string name, Quaternion quaternion)
    {
        GameObject spawnedObject = Object.Instantiate(GetPrefabFromName(name), position, quaternion);//Quaternion.Euler(-90f, 0, 0)
        return spawnedObject;
    }
    protected override GameObject GetPrefabFromName(string name)
    {
        foreach (GameObject gameObject in buildableGameObjects)
        {
            if (gameObject.name == name)return gameObject;
        }
        return null;
    }
    protected override Material GetMaterialFromName(string name)// not in use, for future
    {
        foreach (Material material in buildableMaterials)
        {
            Debug.Log("Material: "+ material.name + " Loaded");
            if (material.name == name) return material;
        }
        return null;
    }
    /*public void SetColliderOff() // not in use, for future
    {
        //childBoxCollider = buildable.GetComponentInChildren<BoxCollider>();
        //if (childBoxCollider != null) { Debug.Log("couldn't find buildable's child box collider"); }
        buildableCollider = buildable.GetComponent<MeshCollider>();
        if (buildableCollider != null) { Debug.Log("couldn't find parent's collider"); }

        //childBoxCollider.enabled = false;
        buildableCollider.enabled = false;
        colliderOff = true;
    }
    public void SetColliderOn()
    {

        //childBoxCollider.enabled = true;
        buildableCollider.enabled = true;
        colliderOff = false;
    }
    private void SetBuildableToGameObject(GameObject gameObject)
    {
        buildable = gameObject;
    */
    private void ChangeColor(Color color)//Not in use, for future
    {
        buildable.GetComponent<Renderer>().material.color = color;
        buildableColor = color;
    }
    private void ChangeMaterial(GameObject gameObject, string mode)// mode 0 = opaque , mode 3 = transparent // not in use!, for future
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
