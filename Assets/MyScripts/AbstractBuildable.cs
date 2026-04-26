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
    protected BoxCollider childBoxCollider;
    protected Rigidbody buildableRigidBody;
    protected Material buildableMaterial;
    protected Material[] buildableMaterials;
    protected GameObject buildable;
    protected GameObject[] buildableGameObjects;

    public virtual void PreviewBuildable() { }
    public virtual void BuildBuildable() { }
    public virtual void UpdateBuildable() { }
    public virtual Vector3 GridSnap(Vector3 position) { return Vector3.zero; }
    //protected abstract void SelectBuildable(string name);
    protected abstract GameObject GetPrefabFromName(string name);
    protected abstract Material GetMaterialFromName(string name);

}
