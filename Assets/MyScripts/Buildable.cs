using System.ComponentModel;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

internal class Buildable : AbstractBuildable
{
    
    protected Buildable(string name/*, string mode*/)
    {
        SetupBuildable();
        buildable = GetPrefabFromName(name);
        //ChangeColor(color);
        //GetMaterialFromName(mode);

        /*if (buildableCollider != null)
        {
            buildableCollider.enabled = false;
        }
        else if (buildableCollider == null)
        {
            Debug.Log("buildable collider is null");
            buildableCollider = buildable.gameObject.AddComponent<MeshCollider>();
            buildableCollider.enabled = false;
        }*/
        SetMeshColliderChildInactive();
    }
    
    private void SetupBuildable()
    {
        //buildableMesh = (Mesh)Resources.Load("Mesh/Ramp"); // for mesh collider, not in use
        buildableGameObjects = Resources.LoadAll<GameObject>("BuildPrefabs");
        buildableMaterials = Resources.LoadAll<Material>("MyMaterials");
        if (buildableGameObjects.Length == 0) { Debug.Log("Resources folder missing prefabs"); }
        if (buildableMaterials.Length == 0) { Debug.Log("Resources folder missing materials"); }
        
        

    }
    public void SetMeshColliderChildActive()
    {
        buildableInstance.transform.GetChild(0).gameObject.SetActive(true);
        buildableInstance.transform.GetChild(1).gameObject.SetActive(true);
        Debug.Log("SetMeshColliderChildActive() Ran");
    }
    public void SetMeshColliderChildInactive()
    {
        buildable.transform.GetChild(0).gameObject.SetActive(false);
        buildable.transform.GetChild(1).gameObject.SetActive(false);
    }
    private void SetCheckColliders() // not in use, this as for the mesh colliders
    {
        buildableColliders = buildable.gameObject.GetComponents<MeshCollider>();
        if (buildableColliders.Length != 0)
        {

            foreach (MeshCollider col in buildableColliders)
            {
                UnityEngine.Object.DestroyImmediate(col.gameObject, true);
            }
        }
        Debug.Log("buildable colliders count after destroy: " + buildableColliders.Length);
    }
    public override GameObject SpawnGameObject(Vector3 position, string name, Quaternion quaternion)
    {
        buildableInstance = Object.Instantiate(buildable, position, quaternion);//Quaternion.Euler(-90f, 0, 0)
        return buildableInstance;
    }
    
    protected override void EnableCollider() // used hours to realize that i need to define mesh collider values to enable the collider, but not gonna use this because i can't destroy stuff in prefabs without errors
    {
        buildableCollider = buildable.gameObject.AddComponent<MeshCollider>();
        buildableCollider.sharedMesh = buildableMesh;
        buildableCollider.convex = true;
        buildableCollider.cookingOptions = MeshColliderCookingOptions.CookForFasterSimulation | MeshColliderCookingOptions.EnableMeshCleaning;
        buildableCollider.enabled = true;
        Debug.Log("EnableCollider() Ran");
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
