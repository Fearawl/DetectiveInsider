using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public string ID;

    private new Rigidbody rigidbody;
    private float angularDrag = 0;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        angularDrag = rigidbody.angularDrag;

        tag = "Pickable";
        gameObject.layer = LayerMask.NameToLayer("Selectable");
    }

    void Start()
    {
        
    }

    public void OnTaken(){
        gameObject.layer = LayerMask.NameToLayer("Picked");
        rigidbody.isKinematic = true;
    }

    public void OnDropped(){
        gameObject.layer = LayerMask.NameToLayer("Selectable");
        rigidbody.angularDrag = angularDrag;
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = false;
    }
}
