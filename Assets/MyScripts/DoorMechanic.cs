using UnityEngine;

public class DoorMechanic : MonoBehaviour
{
    private float smooth = 1f;
    private float doorOpenAngle = -90f;
    private float doorClosedAngle = 0f;
    private bool doorClosed;
    private bool inArea;
    private bool switchD;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!doorClosed && inArea)
        {
            //var target = Quaternion.Euler(0, 0, doorOpenAngle);
            //transform.localRotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5 * smooth);
            //transform.Translate(0.01f, 0, 0);
        }
        else if(doorClosed && !inArea)
        {
            //var target = Quaternion.Euler(0, 0,doorClosedAngle);
            //transform.localRotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 5 * smooth);
            //transform.Translate(-0.01f, 0, 0);
        }

    }
    private void OpenDoor()
    {
        Vector3 vector3 = new Vector3(2, 0, 0);
        transform.Translate(vector3);
        doorClosed = false;
    }
    private void CloseDoor()
    {
        Vector3 vector3 = new Vector3(-2, 0, 0);
        transform.Translate(vector3);
        doorClosed =true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !switchD)
        {
            switchD = true;
            inArea = true;
            Debug.Log("Door Triggered");
            OpenDoor();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && switchD)
        {
            switchD= false;
            inArea=false;
            CloseDoor();
        }
    }
}
