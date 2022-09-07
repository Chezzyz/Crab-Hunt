using System.Collections.Generic;
using UnityEngine;

namespace Items.Bonuses.Entities
{
    public class HolidaysBonusPool : BaseBonusPool
    {
        [SerializeField] private List<BaseBonus> _holidaysBonusPool;

        protected override List<BaseBonus> GetPool()
        {
            return _holidaysBonusPool;
        }

    }
}