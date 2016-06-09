using UnityEngine;
using UnityEngine.Events;

public class HitEvent : UnityEvent
{

}

[RequireComponent(typeof(BoxCollider))]
public class HitBoxTrigger : MonoBehaviour
{
    public static HitEvent EventHit;    
    void Awake()
    {
        if (EventHit == null)
            EventHit = new HitEvent();
        EventHit.AddListener(BroadcastHit);
    }

    void BroadcastHit()
    {
        print("hit");
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {            
            if(EventHit != null)
                EventHit.Invoke();
        }
    }
}

