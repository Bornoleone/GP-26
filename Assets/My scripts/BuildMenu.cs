using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class BuildMenu : MonoBehaviour
{
    [SerializeField] private KeyCode openBuildMenuKey;
    [SerializeField] private KeyCode closeBuildMenuKey;
    private GameObject player;
    public UnityEvent OnBuildMenuOpen;
    public UnityEvent OnBuildMenuClose;
    //[SerializeField] private GameObject buildMenu;
    private bool buildMenuIsOpen = false;
    [SerializeField] private GameObject[] buildPrefabs;
    [SerializeField] private float raycastMaxDistance;
    private Vector3 currentBuildLocation;
    private Transform prefabSpawnTransform;
    private Camera mainCamera;
    private Color selectedGameObjectColor = Color.red;
    private Vector3 selectedGameObjectScale = Vector3.one;
    private string selectedGameObjectShape = "ball";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {   

        player = GameObject.FindGameObjectWithTag("Player"); // finding player
        //buildMenu = GameObject.FindGameObjectWithTag("BuildMenu");// find build menu UI from scene using BuildMenu tag, have to use if game object is inactive
        mainCamera = Camera.main;
        if (mainCamera == null)
            Debug.LogError("No camera with MainCamera tag found!");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            BuildGameObject();
        }
        GetRayCastHitCoordinates();
        HandleInput();
    }
    public void BuildGameObject()
    {
        currentBuildLocation = GetRayCastHitCoordinates();// gets raycast hit coordinates and assigning it to currentBuildLocation
        if (selectedGameObjectShape == "ball")
        {
            Ball ball = new Ball(selectedGameObjectScale, selectedGameObjectColor); // constructs ball object with scale and color parameters
            ball.SpawnGameObject(currentBuildLocation); // uses ball class SpawnGameObject method to spawn Game Object to currentBuildLocation Vector3 
        }
        if(selectedGameObjectShape == "cube")
        {
            Cube cube = new Cube(selectedGameObjectScale, selectedGameObjectColor); // constructs cube object with scale and color parameters
            cube.SpawnGameObject(currentBuildLocation); // uses cube class SpawnGameObject method to spawn Game Object to currentBuildLocation Vector3 
        }
    }
  
    public void HandleInput()
    {
        if (Input.GetKeyDown(openBuildMenuKey))// if buildmenu key pressed then build menu opens
        {
            OpenBuildMenu();
        }
        if (Input.GetKeyDown(closeBuildMenuKey))
        {
            CloseBuildMenu();
        }
    }
    public Vector3 GetRayCastHitCoordinates()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if (Physics.Raycast(ray, out hit, 100))
        {
            Vector3 hitPoint = hit.point;
            Debug.Log("Hit at: " + hitPoint);
            Debug.Log("X: " + hitPoint.x + " Y: " + hitPoint.y + " Z: " + hitPoint.z);
            return hitPoint;
        }
        return Vector3.zero;
    }
    public void OnDropDownColorChanged(int index)
    {
        switch(index)
        {
            case 0: selectedGameObjectColor = Color.red; break;
            case 1: selectedGameObjectColor = Color.green; break;
            
        } 
    }
    public void OnDropDownShapeChanged(int index)
    {
        switch (index)
        {
            case 0: selectedGameObjectShape = "ball"; break;
            case 1: selectedGameObjectShape = "cube"; break;
        }
    }
    public void OnDropDownScaleChanged(int index)
    {
        switch (index)
        {
            case 0: selectedGameObjectScale = Vector3.one; break;
            case 1: selectedGameObjectScale = new Vector3(2,2,2); break;
            case 2: selectedGameObjectScale = new Vector3(3, 3, 3); break;
        }
    }
    public void OpenBuildMenu()
    {
        FreezeTime();
        ShowMouse();
        Debug.Log("build menu open");
        buildMenuIsOpen = true;
        OnBuildMenuOpen.Invoke();
        
        
    }
    public void CloseBuildMenu()
    {
        UnFreezeTime();
        HideMouse();
        Debug.Log("build menu closed");
        OnBuildMenuClose.Invoke();
        buildMenuIsOpen = false;
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
