using UnityEngine;

public class MyInterfaces : MonoBehaviour
{
    
}

public interface IPickUp
{
    bool inPickUpArea { get; set; }
    bool isPickedUp { get; set; }
    void AddItemToInventory();
}