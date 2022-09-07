using System.Collections.Generic;
using UnityEngine;

namespace Other
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class DisableWhenCollidedWith<T> : MonoBehaviour where T : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.GetComponentInParent(typeof(T)) != null)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
