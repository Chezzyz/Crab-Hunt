using System.Collections.Generic;
using UnityEngine;

namespace Items.Bonuses.Entities
{
    public class CoffeeBonusPool : BaseBonusPool
    {
        
        [SerializeField] private List<BaseBonus> _coffeeBonusPool;

        protected override List<BaseBonus> GetPool()
        {
            return _coffeeBonusPool;
        }

        
    }
}