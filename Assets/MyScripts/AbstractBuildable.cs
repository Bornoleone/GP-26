using UnityEngine;

abstract class AbstractBuildable
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

    public virtual void PreviewBuildable() { }
    public virtual void BuildBuildable() { }
    public virtual void UpdateBuildable() { }
    protected abstract void SetupBuildable();
    //protected abstract void SelectBuildable(string name);
    protected abstract GameObject GetBuildableFromName(string name);
    protected abstract GameObject ChangeMaterial(GameObject gameObject, int mode);

}
