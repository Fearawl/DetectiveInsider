using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(AudioSource))]
public class PickableItem : MonoBehaviour
{
    public string ID;

    private new Rigidbody rigidbody;
    private AudioSource audioSource;
    private float angularDrag = 0;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        angularDrag = rigidbody.angularDrag;

        tag = "Pickable";
        gameObject.layer = LayerMask.NameToLayer("Selectable");
        audioSource.minDistance = 0.1f;
        audioSource.maxDistance = 2f;
    }

    void Start()
    {
        
    }

    public void OnTaken(){
        gameObject.layer = LayerMask.NameToLayer("Picked");
        audioSource.PlayOneShot(AudioList.Get("item_taken"));
        rigidbody.isKinematic = true;
    }

    public void OnDropped(){
        gameObject.layer = LayerMask.NameToLayer("Selectable");
        rigidbody.angularDrag = angularDrag;
        rigidbody.velocity = Vector3.zero;
        rigidbody.isKinematic = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Untagged")
            audioSource.PlayOneShot(AudioList.Get("item_dropped"));
    }
}
