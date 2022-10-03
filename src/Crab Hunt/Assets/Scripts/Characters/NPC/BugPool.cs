using System.Collections.Generic;
using Items.Bonuses;
using UnityEngine;

namespace Characters.NPC
{
    public class BugPool : BaseBonusPool
    {
        [SerializeField] private string _idPrefix;
        [SerializeField] private List<BaseBonus> _bugPool;
        
        protected override List<BaseBonus> GetPool()
        {
            return _bugPool;
        }
        
        protected override string GetIdPrefix()
        {
            return _idPrefix;
        }
    }
}