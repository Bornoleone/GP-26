using UnityEngine;

public class RaycastBuildLocation : MonoBehaviour
{
    public static RaycastBuildLocation instance;
    [SerializeField] private float raycastMaxDistance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
