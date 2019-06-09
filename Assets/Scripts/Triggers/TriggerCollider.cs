using UnityEngine;

namespace Triggers
{
    [RequireComponent(typeof(Collider))]
    public class TriggerCollider : TriggerObjectBase
    {
        private void OnTriggerEnter(Collider other)
        {
            Ontrigger(other.gameObject);
        }
    }
}