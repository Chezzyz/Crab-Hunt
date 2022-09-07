using System.Collections.Generic;
using UnityEngine;

namespace Items.Bonuses.Entities
{
    public class DocumentsBonusPool : BaseBonusPool
    {
        [SerializeField] private List<BaseBonus> _documentsBonusPool;
        
        protected override List<BaseBonus> GetPool()
        {
            return _documentsBonusPool;
        }
    }
}