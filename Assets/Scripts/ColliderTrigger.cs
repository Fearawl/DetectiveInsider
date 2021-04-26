using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ColliderTrigger : MonoBehaviour
{
    public ItemInteractionTarget.Interaction interaction;

    void OnTriggerEnter()
    {
        interaction?.OnActivated();
    }

}
