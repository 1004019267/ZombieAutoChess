using UnityEngine;

namespace Triggers
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerCollider2D : TriggerObjectBase
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            Ontrigger(other.gameObject);
        }
    }
}