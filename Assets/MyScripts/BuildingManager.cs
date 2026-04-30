using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;
public enum ConstructionState { Inactive, Active, Rotating}
public class BuildingManager : MonoBehaviour
{
    [SerializeField] private KeyCode buildRampInput = KeyCode.Z;
    [SerializeField] private KeyCode buildFoundationInput = KeyCode.X;
    [SerializeField] private KeyCode buildWallInput = KeyCode.V;
    [SerializeField] private KeyCode buildWallDoorwayInput = KeyCode.C;
    [SerializeField] private KeyCode buildRoofInput = KeyCode.B;
    [SerializeField] private KeyCode buildRotationInput1 = KeyCode.N;
    [SerializeField] private KeyCode buildRotationInput2 = KeyCode.M;
    [SerializeField] private KeyCode buildInput = KeyCode.Mouse1;
    [SerializeField] private float raycastMaxDistance;//about 100
    [SerializeField] private string buildableName;
    [SerializeField] private Vector3 worldPosition;
    [SerializeField] private bool isBuilding;
    [SerializeField] private float offset = 2f;
    [SerializeField] private float offsetFoundation = 2.5f;
    [SerializeField] private Quaternion rotation;
    [SerializeField] private float currentRotationZ = 0f;
    [SerializeField] private float currentRotationX = 90f;
    [SerializeField] private Transform hitTransform;
    private Builder tempObject;
    private GameObject tempGameObject;
    private ConstructionState state;
    public UnityEvent buildModeEventOn;
    public UnityEvent buildModeEventOff;
    private void Start()
    {
        state = ConstructionState.Inactive;
    }

    
    private void Update()
    {
        
        if (state == ConstructionState.Active && isBuilding)
            {
            if(Input.GetKeyDown(KeyCode.Mouse0) && state == ConstructionState.Active && isBuilding || Input.GetKeyDown(buildRoofInput) && state == ConstructionState.Active && isBuilding || Input.GetKeyDown(buildRampInput) && state == ConstructionState.Active &&isBuilding|| Input.GetKeyDown(buildFoundationInput) && state == ConstructionState.Active && isBuilding || Input.GetKeyDown(buildWallDoorwayInput) && state == ConstructionState.Active && isBuilding || Input.GetKeyDown(buildWallInput) && state == ConstructionState.Active && isBuilding)
            {
                Debug.Log("Off Building State Input Pressed");
                SetInactiveMode();
                Destroy(tempGameObject);
                tempObject = null;
            }
            if (Input.GetKeyDown(buildRotationInput1) && isBuilding)
            {
                state = ConstructionState.Rotating;
                Debug.Log("rotation");
                if ( buildableName != "Roof" && currentRotationZ != -90f)
                {
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, -90);
                    SetRotationVariables(-90f, -90f);
                }
                else if (buildableName == "Roof")
                {
                    tempGameObject.transform.rotation = Quaternion.identity;
                    SetRotationVariables(0f, 0f);
                }
                else if (buildableName == "Wall" || buildableName == "WallDoorway")
                {
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    SetRotationVariables(-90f, 0f);
                }
                else if (buildableName != "Roof")
                {
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    SetRotationVariables(-90f, 0f);
                }
                state = ConstructionState.Active;
            }
            if (Input.GetKeyDown(buildRotationInput2) && isBuilding)
            {
                state = ConstructionState.Rotating;
                Debug.Log("rotation");
                if (buildableName != "Roof" && currentRotationZ != 90f)
                {
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 90);
                    SetRotationVariables(-90f, 90f);
                }
                else if (buildableName == "Roof")
                {
                    tempGameObject.transform.rotation = Quaternion.identity;
                    SetRotationVariables(0f, 0f);
                }
                else if (buildableName == "Wall" || buildableName == "WallDoorway")
                {
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    SetRotationVariables(90f, 0f);
                }
                else if (buildableName != "Roof")
                {
                    Debug.Log("rotation");
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    SetRotationVariables(-90f, 0f);
                }
                state = ConstructionState.Active;
            }
            if (Input.GetKeyDown(buildInput) && state == ConstructionState.Active && isBuilding)
            {
                //Debug.Log("tempGameObject.transform.rotation: " + tempGameObject.transform.rotation);
                
                BuildGameObject();
            }
            else if (state == ConstructionState.Active && isBuilding)
            {
                worldPosition = GetRayCastHitCoordinates();
                tempGameObject.transform.position = worldPosition;
            }
            else { return; }
        }
        if (Input.GetKeyDown(buildRampInput) && !isBuilding)
        {
            Debug.Log("buildRampInput pressed");
            SetBuildableName("Ramp");
            SetActiveMode();
        }
        if (Input.GetKeyDown(buildFoundationInput) && !isBuilding)
        {
            Debug.Log("buildFoundationInput pressed");
            SetBuildableName("Foundation");
            SetActiveMode();
        }
        if (Input.GetKeyDown(buildWallDoorwayInput) && !isBuilding)
        {
            Debug.Log("buildWallDoorwayInput pressed");
            SetBuildableName("WallDoorway");
            SetActiveMode();
        }
        if (Input.GetKeyDown(buildWallInput) && !isBuilding)
        {
            Debug.Log("buildWallInput pressed");
            SetBuildableName("Wall");
            SetActiveMode();
        }
        if (Input.GetKeyDown(buildRoofInput) && !isBuilding)
        {
            Debug.Log("buildRoofInput pressed");
            SetBuildableName("Roof");
            SetActiveMode();
        }
    }
    private void SetBuildableName(string name)
    {
        buildableName = name;
    }
    private void SetRotationVariables(float x, float z)
    {
        currentRotationX = x;
        currentRotationZ = z;
    }
    private void SetObject()
    {
        
        tempObject = null;
        tempGameObject = null;
        if(buildableName == "Roof")
        {
            tempObject = new Builder(buildableName);
            tempGameObject = tempObject.SpawnGameObject(worldPosition, buildableName, Quaternion.identity);
            //tempObject.SetColliderOff();
        }
        else if(buildableName != "Roof")
        {
            tempObject = new Builder(buildableName);
            tempGameObject = tempObject.SpawnGameObject(worldPosition, buildableName, Quaternion.Euler(-90, 0, currentRotationZ));
            //tempObject.SetColliderOff();
        }
    }
    private void SetActiveMode()
    {
        if (state == ConstructionState.Inactive)
        {
            SetObject();
            isBuilding = true;
            state = ConstructionState.Active;
            buildModeEventOn.Invoke();
        }
    }
    private void SetInactiveMode()
    {
        state = ConstructionState.Inactive;
        isBuilding = false;
        buildModeEventOff.Invoke();
    }
    private void BuildGameObject()
    {
        //tempObject.SetColliderOn();
        //tempObject.ChangeMaterial(tempGameObject, "transparent");
        tempObject.SetMeshColliderChildActive();
        tempGameObject.transform.position = worldPosition;
        SetObject();
    }
    
    
    private Vector3 GetRayCastHitCoordinates()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * raycastMaxDistance, Color.red);

        if (Physics.Raycast(ray, out hit, raycastMaxDistance))// if raycast hits inside raycastMaxDistance
        {
            Vector3 hitPoint = tempObject.GridSnap(hit.point); //puts hit.point in Vector3 HitPoint variable
            //Debug.Log("Hit at: " + hitPoint);
            //Debug.Log("X: " + hitPoint.x + " Y: " + hitPoint.y + " Z: " + hitPoint.z);// hit coordinates
            //hit.collider.tag //maybe use
            hitTransform = hit.transform;
            if (isBuilding)
            {
                Vector3 buildCoordinates;
                if (buildableName == "Foundation")
                {
                    buildCoordinates = new Vector3(hitPoint.x, hitPoint.y - offsetFoundation, hitPoint.z);
                    return buildCoordinates;
                }
                if (buildableName == "Ramp")
                {
                    buildCoordinates = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z);
                    return buildCoordinates;
                }
                if (buildableName == "Roof")
                {
                    buildCoordinates = new Vector3(hitPoint.x, hitPoint.y + offset, hitPoint.z);
                    return buildCoordinates;
                }
                if (buildableName == "Wall" || buildableName == "WallDoorway")
                {
                    if (currentRotationZ == -90f)
                    {
                        buildCoordinates = new Vector3(hitPoint.x - offset, hitPoint.y, hitPoint.z);
                        return buildCoordinates;
                    }
                    if(currentRotationZ == 90f)
                    {
                        buildCoordinates = new Vector3(hitPoint.x + offset, hitPoint.y, hitPoint.z);
                        return buildCoordinates;
                    }
                    if (currentRotationZ == 0f && currentRotationX == -90f)
                    {
                        buildCoordinates = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z + offset);
                        return buildCoordinates;
                    }
                    if (currentRotationZ == 0f && currentRotationX == 90f)
                    {
                        buildCoordinates = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z - offset);
                        return buildCoordinates;
                    }
                }
                else
                {
                    return hitPoint;
                }
            }
        }
        return Vector3.zero;// Vector3 have to be returned because of the returning type so Vector3.zero is Vector3(0,0,0)
    }
    private IEnumerator Stall()
    {
        yield return new WaitForSeconds(1f);
    }
}
