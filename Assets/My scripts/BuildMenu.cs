using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] private KeyCode openBuildMenuKey;
    [SerializeField] private KeyCode closeBuildMenuKey;
    [SerializeField] private float raycastMaxDistance;
    private GameObject player;
    public UnityEvent OnBuildMenuOpen;
    public UnityEvent OnBuildMenuClose;
    private bool buildMenuIsOpen = false;
    private Vector3 currentBuildLocation;
    private Transform prefabSpawnTransform;
    private Camera mainCamera;
    private Color selectedGameObjectColor = Color.red;
    private Vector3 selectedGameObjectScale = Vector3.one;
    private string selectedGameObjectShape = "ball";

    void Awake()
    {   

        player = GameObject.FindGameObjectWithTag("Player"); // finding player soon as class is awaken
        mainCamera = Camera.main; // assign main camera for mainCamera variable
        if (mainCamera == null) // if there is no cameras inside of the scene this will tell me
            Debug.LogError("No camera with MainCamera tag found!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))//monitoring input every frame for building a Game Object
        {
            BuildGameObject();// Build Game object method call
        }
        GetRayCastHitCoordinates();// getting raycast coordinates every frame
        HandleInput(); // handeling input every frame
    }
    public void BuildGameObject()
    {
        currentBuildLocation = GetRayCastHitCoordinates();// gets raycast hit coordinates and assigning it to currentBuildLocation
        if (selectedGameObjectShape == "ball")//if selected game object is ball then iterates this code block
        {
            Ball ball = new Ball(selectedGameObjectScale, selectedGameObjectColor); // constructs ball object with scale and color parameters
            ball.SpawnGameObject(currentBuildLocation); // uses ball class SpawnGameObject method to spawn Game Object to currentBuildLocation Vector3 
        }
        if(selectedGameObjectShape == "cube")//if selected game object is cube then iterates this code block
        {
            Cube cube = new Cube(selectedGameObjectScale, selectedGameObjectColor); // constructs cube object with scale and color parameters
            cube.SpawnGameObject(currentBuildLocation); // uses cube class SpawnGameObject method to spawn Game Object to currentBuildLocation Vector3 
        }
    }
  
    public void HandleInput()
    {
        if (Input.GetKeyDown(openBuildMenuKey))// if buildmenu key pressed then build menu opens
        {
            OpenBuildMenu(); //opens build menu
        }
        if (Input.GetKeyDown(closeBuildMenuKey))
        {
            CloseBuildMenu(); // closes buildmenu
        }
    }
    public Vector3 GetRayCastHitCoordinates()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * raycastMaxDistance, Color.red);

        if (Physics.Raycast(ray, out hit, raycastMaxDistance))// if raycast hits inside raycastMaxDistance
        {
            Vector3 hitPoint = hit.point; //puts hit.point in Vector3 HitPoint variable
            Debug.Log("Hit at: " + hitPoint);
            Debug.Log("X: " + hitPoint.x + " Y: " + hitPoint.y + " Z: " + hitPoint.z);// hit coordinates
            return hitPoint;// returns the Vector3
        }
        return Vector3.zero;// Vector3 have to be returned because of the returning type so Vector3.zero is Vector3(0,0,0)
    }
    public void OnDropDownColorChanged(int index)//selecting Buildable game object color
    {
        switch(index) //switch case for dropdown return index from Build menu UI
        {
            case 0: selectedGameObjectColor = Color.red; break;
            case 1: selectedGameObjectColor = Color.green; break;
            
        } 
    }
    public void OnDropDownShapeChanged(int index)//selecting Buildable game object shape(cube or ball)
    {
        switch (index)//switch case for dropdown return index from Build menu UI
        {
            case 0: selectedGameObjectShape = "ball"; break;
            case 1: selectedGameObjectShape = "cube"; break;
        }
    }
    public void OnDropDownScaleChanged(int index)//selecting Buildable game object scale
    {
        switch (index)//switch case for dropdown return index from Build menu UI
        {
            case 0: selectedGameObjectScale = Vector3.one; break;
            case 1: selectedGameObjectScale = new Vector3(2,2,2); break;
            case 2: selectedGameObjectScale = new Vector3(3, 3, 3); break;
        }
    }
    public void OpenBuildMenu()
    {
        FreezeTime(); //Freezes time
        ShowMouse(); //shows mouse
        Debug.Log("build menu open");
        buildMenuIsOpen = true;// marking state
        OnBuildMenuOpen.Invoke();// invokes OnBuildMenuOpen event


    }
    public void CloseBuildMenu()
    {
        UnFreezeTime(); //Un Freezes time
        HideMouse();//hides mouse
        Debug.Log("build menu closed");
        OnBuildMenuClose.Invoke();// invokes OnBuildMenuClose event
        buildMenuIsOpen = false;// marking state
    }
    public void ShowMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void HideMouse()
    {
        Cursor.lockState= CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void FreezeTime()
        { Time.timeScale = 0; }
    public void UnFreezeTime()
        { Time.timeScale = 1; }
}
