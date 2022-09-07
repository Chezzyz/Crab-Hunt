using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Network;
using Photon.Pun;
using UnityEngine;

namespace Other
{
    public abstract class AbstractPool<T> : MonoBehaviourPun where T : MonoBehaviour
    {
        private void Start()
        {
            if (IsAutofilled())
            {
                FillPool();
            }
        }

        public int GetSize()
        {
            return GetPool().Capacity;
        }

        public int GetActiveElementsCount()
        {
            return GetPool().Count(elem => elem.gameObject.activeInHierarchy);
        }
        
        public virtual T GetNextFreeElement()
        {
            T element = GetPool().First(elem => !elem.gameObject.activeInHierarchy);
            return element;
        }
        
        public virtual T GetRandomFreeElement()
        {
            List<T> actives = GetPool().Where(elem => !elem.gameObject.activeInHierarchy).ToList();
            T element = actives.ElementAt(Random.Range(0, actives.Count()));
            return element;
        }

        protected abstract List<T> GetPool();

        protected abstract bool IsAutofilled();

        protected abstract T GetElementPrefab();

        private void FillPool()
        {
            List<T> pool = GetPool();
            for (int i = 0; i < GetSize(); i++)
            {
                T elem = Instantiate(GetElementPrefab(), transform);
                elem.gameObject.SetActive(false);
                pool.Add(elem);
            }
        }
        
    }
}