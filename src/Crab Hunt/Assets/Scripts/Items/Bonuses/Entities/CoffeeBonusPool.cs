using System.Collections.Generic;
using UnityEngine;

namespace Items.Bonuses.Entities
{
    public class CoffeeBonusPool : BaseBonusPool
    {
        [SerializeField] private string _idPrefix;
        [SerializeField] private List<BaseBonus> _coffeeBonusPool;

        protected override List<BaseBonus> GetPool()
        {
            return _coffeeBonusPool;
        }
        
        protected override string GetIdPrefix()
        {
            return _idPrefix;
        }
    }
}