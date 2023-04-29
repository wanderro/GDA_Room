using UnityEngine;

public class TakeItem : MonoBehaviour
{
    [SerializeField]
    private Transform _inventoryHolder;
    
    private GameObject _heldItem;

    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo, 2f))
        {
           var observedItem = hitInfo.collider.GetComponent<InteractableItem>();
           observedItem.SetFocus();
            if (Input.GetKeyDown(KeyCode.E))
            {
                var item = observedItem;
                if (item != null)
                {
                    // Если нет предмета в руках
                    if (_heldItem == null)
                    {
                        _heldItem = item.gameObject;
                        
                        _heldItem.transform.SetParent(_inventoryHolder);
                
                        _heldItem.transform.localPosition = Vector3.zero;
                        item.RemoveFocus();
                    }
                    // Если держим предмет в руках
                    else
                    {
                        var oldItem = _heldItem;
                        _heldItem = item.gameObject;
                        _heldItem.transform.SetParent(_inventoryHolder);
                        _heldItem.transform.localPosition = Vector3.zero;
                        item.RemoveFocus();
                        DropItem(oldItem);
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && _heldItem != null)
        {
            ThrowItem();
        }
    }

    private void ThrowItem()
    {
        _heldItem.transform.SetParent(null);
        var rigidbody = _heldItem.GetComponent<Rigidbody>();
        rigidbody.isKinematic = false;
        rigidbody.AddForce(Camera.main.transform.forward * 500f); // Добавляем силу для броска вперед
        _heldItem = null;
    }

    private void DropItem(GameObject item)
    {
        item.transform.SetParent(null);
        item.GetComponent<Rigidbody>().isKinematic = false;
        item.GetComponent<InteractableItem>().SetFocus();
    }
}


