using UnityEngine;
using UnityEngine.UIElements;

abstract class BuildableObject
{
    protected Vector3 objectPosition;
    protected Vector3 objectRotation;
    protected Vector3 objectScale;
    protected string objectName;
    protected Color objectColor;
    protected Renderer objectRenderer;
    protected Rigidbody rb;
    protected Material objectMaterial;
    protected GameObject buildableGameObject;

    protected BuildableObject()
    {
        Debug.Log("BuildableObject created");
    }
    
    public abstract GameObject SpawnGameObject(Vector3 position);


    //protected virtual void Start() { } 
    //protected virtual void Update() { }

}
