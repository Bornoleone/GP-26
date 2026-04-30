using System;
using UnityEngine;

abstract class AbstractBuildable
{
    protected Vector3 buildablePosition;
    protected Vector3 buildableRotation;
    protected Vector3 buildableScale;
    protected string buildableName;
    protected Color buildableColor;
    protected Renderer buildableRenderer;
    internal bool colliderOff;
    protected MeshCollider buildableCollider;
    protected MeshCollider[] buildableColliders;
    protected BoxCollider childBoxCollider;
    protected Rigidbody buildableRigidBody;
    protected Mesh buildableMesh;
    protected Material buildableMaterial;
    protected Material[] buildableMaterials;
    protected GameObject buildable;
    protected GameObject[] buildableGameObjects;
    protected GameObject buildableInstance;
    public virtual void PreviewBuildable() { }
    public virtual void BuildBuildable() { }
    public virtual void UpdateBuildable() { }
    protected virtual void EnableCollider() { }
    public virtual void DisableCollider() { }
    public virtual Vector3 GridSnap(Vector3 position) { return Vector3.zero; }
    public virtual GameObject SpawnGameObject(Vector3 position, string name, Quaternion quaternion) { return buildable; }
    //protected abstract void SelectBuildable(string name);
    protected abstract GameObject GetPrefabFromName(string name);
    protected abstract Material GetMaterialFromName(string name);

}
