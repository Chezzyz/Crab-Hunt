using System.Collections.Generic;
using UnityEngine;

namespace Items.Bonuses.Entities
{
    public class ShieldBonusPool : BaseBonusPool
    {
        [SerializeField] private List<BaseBonus> _shieldBonusPool;
        
        protected override List<BaseBonus> GetPool()
        {
            return _shieldBonusPool;
        }
        
    }
}