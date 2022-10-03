using System;
using System.Collections.Generic;
using Characters.Players;
using Items.Bonuses;
using UnityEngine;
using UnityEngine.UI;

namespace Services.Tutorial.Tasks
{
    public class PickupBonusesTask : AbstractTask
    {
        [SerializeField] private GameObject _bonusPools;
        [SerializeField] private GameObject _bonusesTable;

        private bool _coffeeBonusPickedUp;
        private bool _shieldBonusPickedUp;
        private bool _holidaysBonusPickedUp;
        private bool _documentsBonusPickedUp;

        private void OnEnable()
        {
            CoffeeBonus.CoffeeBonusStateChanged += OnCoffeeBonusPickedUp;
            ShieldBonus.ShieldBonusStateChanged += OnShieldBonusPickedUp;
            HolidaysBonus.HolidaysBonusPickedUp += OnHolidaysBonusPickedUp;
            DocumentsBonus.DocumentsBonusStateChanged += OnDocumentsBonusPickedUp;
        }

        private void OnDocumentsBonusPickedUp(Player arg1, bool state)
        {
            if (state && _isActive && !_isCompleted)
            {
                _documentsBonusPickedUp = true;
                if (CheckForCondition()) CompleteTask();
            }
        }

        private void OnHolidaysBonusPickedUp()
        {
            if (_isActive && !_isCompleted)
            {
                _holidaysBonusPickedUp = true;
                if (CheckForCondition()) CompleteTask();
            }
        }

        private void OnShieldBonusPickedUp(Player arg1, bool state)
        {
            if (state && _isActive && !_isCompleted)
            {
                _shieldBonusPickedUp = true;
                if (CheckForCondition()) CompleteTask();
            }
        }

        private void OnCoffeeBonusPickedUp(Player arg1, bool state)
        {
            if (state && _isActive && !_isCompleted)
            {
                _coffeeBonusPickedUp = true;
                if (CheckForCondition()) CompleteTask();
            }
        }

        private bool CheckForCondition()
        {
            return _coffeeBonusPickedUp && _shieldBonusPickedUp && _holidaysBonusPickedUp && _documentsBonusPickedUp;
        }

        protected override void OnActive()
        {
            _bonusPools.SetActive(true);    
            _bonusesTable.SetActive(true);
        }

        private void OnDisable()
        {
            CoffeeBonus.CoffeeBonusStateChanged -= OnCoffeeBonusPickedUp;
            ShieldBonus.ShieldBonusStateChanged -= OnShieldBonusPickedUp;
            HolidaysBonus.HolidaysBonusPickedUp -= OnHolidaysBonusPickedUp;
            DocumentsBonus.DocumentsBonusStateChanged -= OnDocumentsBonusPickedUp;
        }
    }
}