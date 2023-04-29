using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningDoors : MonoBehaviour
{
    [SerializeField]
    private Collider _collider;

    [SerializeField]
    private Door _door;
    
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (_collider.Raycast(ray, out var hitInfo, 5f))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _door.SwitchDoorState();
            }
        }
    }
}
