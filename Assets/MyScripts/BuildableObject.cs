using Unity.FPS.Game;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

abstract class BuildableObject
{
    protected Vector3 objectPosition;
    protected Quaternion objectRotation;
    protected Vector3 objectScale;
    protected string buildableObjectName;
    protected Color objectColor;
    protected Renderer objectRenderer;
    protected MeshFilter objectMeshFilter;
    protected Rigidbody rb;
    protected Health objectHealth;
    protected Material objectMaterial;
    protected GameObject buildableGameObject;
    protected float gridSize = 1f;
    //

    protected BuildableObject()
    {
        Debug.Log("BuildableObject created");
    }
    
    public abstract GameObject SpawnGameObject(Vector3 position);
    public virtual Vector3 GridSnap(Vector3 position) { return Vector3.zero; }
    public virtual void SetGridSize(float grid){ gridSize = grid; }


}
