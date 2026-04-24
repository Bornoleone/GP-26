using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public enum ConstructionState { Inactive, Active, Rotating}
public class BuildingManager : MonoBehaviour
{
    public InputAction leftClick;
    public InputAction rightClick;
    public InputAction cancelInput;
    [SerializeField] private InputAction zKeyInput;

    [SerializeField] private float raycastMaxDistance;//about 100
    [SerializeField] private string buildableName;
    [SerializeField] private Vector3 worldPosition;
    [SerializeField] private bool isBuilding;
    [SerializeField] private bool canBuild;
    [SerializeField] private float offset = 2f;
    [SerializeField] private float offsetFoundation;
    [SerializeField] private Quaternion rotation;
    [SerializeField] private float currentRotationZ = 0f;
    [SerializeField] private float currentRotationX = 90f;
    [SerializeField] private Transform hitTransform;
    private Builder tempObject;
    private GameObject tempGameObject;
    private ConstructionState state;
    private void Start()
    {
        state = ConstructionState.Inactive;
        OnEnable();
    }

    
    private void Update()
    {
        if(state == ConstructionState.Active)
        {
            Debug.Log("isBuilding: "+ isBuilding);
        }
        if (state == ConstructionState.Active && isBuilding)
            {
            

            if(Input.GetKeyDown(KeyCode.B) && state == ConstructionState.Active && isBuilding || Input.GetKeyDown(KeyCode.Z)&& state == ConstructionState.Active &&isBuilding|| Input.GetKeyDown(KeyCode.X) && state == ConstructionState.Active && isBuilding || Input.GetKeyDown(KeyCode.C) && state == ConstructionState.Active && isBuilding || Input.GetKeyDown(KeyCode.V) && state == ConstructionState.Active && isBuilding)
            {
                Debug.Log("Off Building State Input Pressed");
                SetInactiveMode();
                Destroy(tempGameObject);
                tempObject = null;
                
            }
            if (Input.GetKeyDown(KeyCode.N) && isBuilding)
            {
                if ( buildableName != "Roof" && currentRotationZ != -90f)
                {
                    Debug.Log("rotation");
                    state = ConstructionState.Rotating;
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, -90);
                    currentRotationZ = -90f;
                    currentRotationX = -90f;
                    state = ConstructionState.Active; 
                }
                else if (buildableName == "Roof")
                {
                    Debug.Log("rotation");
                    state = ConstructionState.Rotating;
                    tempGameObject.transform.rotation = Quaternion.identity;
                    currentRotationZ = 0f;
                    currentRotationX = 0f;
                    state = ConstructionState.Active;
                }
                else if (buildableName == "Wall" || buildableName == "WallDoorway")
                {
                    Debug.Log("rotation");
                    state = ConstructionState.Rotating;
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    currentRotationZ = 0f;
                    currentRotationX = -90f;
                    state = ConstructionState.Active;
                }
                else if (buildableName != "Roof")
                {
                    Debug.Log("rotation");
                    state = ConstructionState.Rotating;
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    currentRotationZ = 0f;
                    currentRotationX = -90f;
                    state = ConstructionState.Active;
                    
                }
            }
            if (Input.GetKeyDown(KeyCode.M) && isBuilding)
            {
                if (buildableName != "Roof" && currentRotationZ != 90f)
                {
                    Debug.Log("rotation");
                    state = ConstructionState.Rotating;
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 90);
                    currentRotationZ = 90f;
                    currentRotationX = -90f;
                    state = ConstructionState.Active; 
                }
                else if (buildableName == "Roof")
                {
                    Debug.Log("rotation");
                    state = ConstructionState.Rotating;
                    tempGameObject.transform.rotation = Quaternion.identity;
                    currentRotationZ = 0f;
                    currentRotationX = 0f;
                    state = ConstructionState.Active;
                }
                else if (buildableName == "Wall" || buildableName == "WallDoorway")
                {
                    Debug.Log("rotation");
                    state = ConstructionState.Rotating;
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    currentRotationZ = 0f;
                    currentRotationX = 90f;
                    state = ConstructionState.Active;
                }
                else if (buildableName != "Roof")
                {
                    Debug.Log("rotation");
                    state = ConstructionState.Rotating;
                    tempGameObject.transform.rotation = Quaternion.Euler(-90, 0, 0);
                    currentRotationZ = 0f;
                    currentRotationX = -90f;
                    state = ConstructionState.Active;
                    
                }
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && state == ConstructionState.Active && isBuilding)
            {
                Debug.Log("tempGameObject.transform.rotation: " + tempGameObject.transform.rotation);
                BuildGameObject();
            }
            
            else if (state == ConstructionState.Active && isBuilding)
            {
                worldPosition = GetRayCastHitCoordinates();
                tempGameObject.transform.position = worldPosition;
            }
            else { return; }
        }
        if (Input.GetKeyDown(KeyCode.Z) && !isBuilding)
        {
            Debug.Log("Z pressed");
            buildableName = "Ramp";
            SetActiveMode();
        }
        if (Input.GetKeyDown(KeyCode.X) && !isBuilding)
        {
            Debug.Log("X pressed");
            buildableName = "Foundation";
            SetActiveMode();
        }
        if (Input.GetKeyDown(KeyCode.C) && !isBuilding)
        {
            Debug.Log("C pressed");
            buildableName = "WallDoorway";
            SetActiveMode();
        }
        if (Input.GetKeyDown(KeyCode.V) && !isBuilding)
        {
            Debug.Log("V pressed");
            buildableName = "Wall";
            SetActiveMode();
        }
        if (Input.GetKeyDown(KeyCode.B) && !isBuilding)
        {
            Debug.Log("B pressed");
            buildableName = "Roof";
            SetActiveMode();
        }
    }
    private void SetObject()
    {
        if(buildableName == "Roof")
        {
            Builder builder = new Builder(buildableName, "transparent");
            tempObject = builder;
            tempGameObject = builder.SpawnGameObject(worldPosition, buildableName, Quaternion.identity, "transparent");
        }
        else if(buildableName != "Roof")
        {
            Builder builder = new Builder(buildableName, "transparent");
            tempObject = builder;
            tempGameObject = builder.SpawnGameObject(worldPosition, buildableName, Quaternion.Euler(-90, 0, currentRotationZ), "transparent");
        }
    }
    private void SetActiveMode()
    {
        if (state == ConstructionState.Inactive)
        {
            SetObject();
            isBuilding = true;
            state = ConstructionState.Active;
        }
    }
    private void SetInactiveMode()
    {
        state = ConstructionState.Inactive;
        isBuilding = false;
    }
    private void BuildGameObject()
    {
        tempGameObject.transform.position = worldPosition;
        SetObject();
    }
    
    private void OnEnable()
    {
        leftClick.Enable();
        rightClick.Enable();
        cancelInput.Enable();
        zKeyInput.Enable();
    }
    private void OnDisable()
    {
        leftClick.Disable();
        rightClick.Disable();
        cancelInput.Disable();
        zKeyInput.Disable();
    }
    private Vector3 GetRayCastHitCoordinates()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * raycastMaxDistance, Color.red);

        if (Physics.Raycast(ray, out hit, raycastMaxDistance))// if raycast hits inside raycastMaxDistance
        {
            Vector3 hitPoint = tempObject.GridSnap(hit.point); //puts hit.point in Vector3 HitPoint variable
            Debug.Log("Hit at: " + hitPoint);
            Debug.Log("X: " + hitPoint.x + " Y: " + hitPoint.y + " Z: " + hitPoint.z);// hit coordinates
            //hit.collider.tag //maybe use
            hitTransform = hit.transform;
            if (isBuilding)
            {
                if (buildableName == "Foundation")
                {
                    Vector3 buildCoordinates;
                    buildCoordinates = new Vector3(hitPoint.x, hitPoint.y - 2.5f, hitPoint.z);
                    canBuild = true;
                    return buildCoordinates;
                }
                if (buildableName == "Ramp")
                {
                    Vector3 buildCoordinates;
                    buildCoordinates = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z);
                    canBuild = true;
                    return buildCoordinates;
                }
                if (buildableName == "Roof")
                {
                    Vector3 buildCoordinates;
                    buildCoordinates = new Vector3(hitPoint.x, hitPoint.y + offset, hitPoint.z);
                    canBuild = true;
                    return buildCoordinates;
                }
                if (buildableName == "Wall" || buildableName == "WallDoorway")
                {
                    if (currentRotationZ == -90)
                    {
                        Vector3 buildCoordinates;
                        buildCoordinates = new Vector3(hitPoint.x - offset, hitPoint.y, hitPoint.z);
                        return buildCoordinates;
                    }
                    if(currentRotationZ == 90)
                    {
                        Vector3 buildCoordinates;
                        buildCoordinates = new Vector3(hitPoint.x + offset, hitPoint.y, hitPoint.z);
                        return buildCoordinates;
                    }
                    if (currentRotationZ == 0 && currentRotationX == -90)
                    {
                        Vector3 buildCoordinates;
                        buildCoordinates = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z + offset);
                        return buildCoordinates;
                    }
                    if (currentRotationZ == 0 && currentRotationX == 90)
                    {
                        Vector3 buildCoordinates;
                        buildCoordinates = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z - offset);
                        return buildCoordinates;
                    }
                }
                else
                {
                    Vector3 buildCoordinates;
                    buildCoordinates = hitPoint;
                }
            }
        }
        return Vector3.zero;// Vector3 have to be returned because of the returning type so Vector3.zero is Vector3(0,0,0)
    }
}
