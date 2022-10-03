using System.Collections.Generic;
using Items.Bonuses;
using UnityEngine;

namespace Characters.NPC
{
    public class CrabPool : BaseBonusPool
    {
        [SerializeField] private string _idPrefix;
        [SerializeField] private List<BaseBonus> _crabPool;

        protected override List<BaseBonus> GetPool()
        {
            return _crabPool;
        }
        
        protected override string GetIdPrefix()
        {
            return _idPrefix;
        }
    }
}