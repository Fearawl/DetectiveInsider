using UnityEngine;
using UnityEngine.Events;

public class ItemInteractionTarget : MonoBehaviour
{
    [System.Serializable]
    public class Interaction {
        public string ItemID;
        public UnityEvent Event;
        public bool OneTime;
        private bool activated;
        public bool Active => !OneTime || !activated;
        public void OnActivated(){
            if (!Active) return;
            Event.Invoke();
            if (OneTime) activated = true;
        }
    }

    public Interaction[] interactions = new Interaction[0];

    public void Start() {}

    public void TryInsertItem(PickableItem item){
        foreach (var interaction in interactions){
            if (interaction.ItemID == item.ID && interaction.Active){
                interaction.OnActivated();
                break;
            }
        }
        foreach (var interaction in interactions)
            if (interaction.Active) return;
        enabled = false;
    }
}
