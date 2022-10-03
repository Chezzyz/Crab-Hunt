using System.Collections.Generic;
using UnityEngine;

namespace Items.Bonuses.Entities
{
    public class HolidaysBonusPool : BaseBonusPool
    {
        [SerializeField] private string _idPrefix;
        [SerializeField] private List<BaseBonus> _holidaysBonusPool;

        protected override List<BaseBonus> GetPool()
        {
            return _holidaysBonusPool;
        }

        protected override string GetIdPrefix()
        {
            return _idPrefix;
        }
    }
}