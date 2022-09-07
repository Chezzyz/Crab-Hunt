using System.Collections.Generic;
using Network;
using Other;
using UnityEngine;

namespace Characters.Players.Shooting
{
    public class ProjectilesPool : AbstractPool<ShootingProjectile>
    {
        [SerializeField] private int _poolSize;
        [SerializeField] private ShootingProjectile _projectilePrefab;
        private List<ShootingProjectile> _pool;

        protected override List<ShootingProjectile> GetPool()
        {
            if (_pool == null)
            {
                _pool = new List<ShootingProjectile>(_poolSize);
            }
            return _pool;
        }

        protected override bool IsAutofilled()
        {
            return true;
        }

        protected override ShootingProjectile GetElementPrefab()
        {
            return _projectilePrefab;
        }

    }
}
