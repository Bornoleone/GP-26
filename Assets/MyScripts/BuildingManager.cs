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
    [SerializeField] private Quaternion rotation;
    [SerializeField] private float currentRotation = 0f;
    [SerializeField] private Transform hitTransform;
    public object buildObject;
    private Builder tempObject;
    private GameObject tempGameObject;
    ConstructionState state;
    void Start()
    {
        state = ConstructionState.Inactive;
        OnEnable();
    }

    
    void Update()
    {
        if(state == ConstructionState.Active)
        {
            Debug.Log("isBuilding: "+ isBuilding);
        }
        //worldPosition = GetRayCastHitCoordinates();
        if (state == ConstructionState.Active && isBuilding)
            {
            

            if(Input.GetKeyDown(KeyCode.Z)&& state == ConstructionState.Active &&isBuilding|| Input.GetKeyDown(KeyCode.X) && state == ConstructionState.Active && isBuilding || Input.GetKeyDown(KeyCode.C) && state == ConstructionState.Active && isBuilding || Input.GetKeyDown(KeyCode.V) && state == ConstructionState.Active && isBuilding)
            {
                Debug.Log("Off Building State Input Pressed");
                SetInactiveMode();
                Destroy(tempGameObject);
                tempObject = null;
                
            }
            if (Input.GetKeyDown(KeyCode.N) && isBuilding)
            {
                Debug.Log("rotation");
                state = ConstructionState.Rotating;
                tempGameObject.transform.rotation *= Quaternion.Euler(0, 0, -90);
                currentRotation = -90f;
                state = ConstructionState.Active;
            }
            if (Input.GetKeyDown(KeyCode.M) && isBuilding)
            {
                Debug.Log("rotation");
                state = ConstructionState.Rotating;
                tempGameObject.transform.rotation *= Quaternion.Euler(0, 0, 90);
                currentRotation = 90f;
                state = ConstructionState.Active;
            }
            if (Input.GetKeyDown(KeyCode.Mouse1) && state == ConstructionState.Active && isBuilding)
            {
                Debug.Log("tempGameObject.transform.rotation: " + tempGameObject.transform.rotation);
                SetInactiveMode();
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
    }
    private void SetObject()
    {
        Builder builder = new Builder(buildableName, "transparent");
        tempObject = builder;
        tempGameObject = builder.SpawnGameObject(worldPosition,buildableName, Quaternion.Euler(-90, 0, 0), "transparent" );
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
        //tempObject.SpawnGameObject(worldPosition + offset, buildableName, Quaternion.Euler(-90, 0, 0), "transparent");
        currentRotation = 0f;
        state = ConstructionState.Inactive;

    }
    private IEnumerator build(GameObject tempGameObject, Vector3 vector3)
    {

        yield return new WaitForSeconds(0.5f);
        
        isBuilding = false;
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
    public Vector3 GetRayCastHitCoordinates()
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
                    canBuild = true;
                }
                if (buildableName == "Ramp")
                {
                    Vector3 buildCoordinates;
                    canBuild = true;
                }
                if (buildableName == "Wall" || buildableName == "WallDoorway")
                {
                    if (currentRotation >= 0)
                    {
                        Vector3 buildCoordinates;
                        buildCoordinates = new Vector3(hitPoint.x + offset, hitPoint.y, hitPoint.z);
                        return buildCoordinates; 
                    }
                    else
                    {
                        Vector3 buildCoordinates;
                        buildCoordinates = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z + offset);
                        return buildCoordinates;
                    }
                }
                
            }
            return hitPoint;// returns the Vector3
        }
        return Vector3.zero;// Vector3 have to be returned because of the returning type so Vector3.zero is Vector3(0,0,0)
    }
}
