using System;
using Network;
using Other;
using Photon.Pun;

namespace Items.Bonuses
{
    public abstract class BaseBonusPool : AbstractPool<BaseBonus>
    {
        private void Awake()
        {
            for (int i = 0; i < GetPool().Count; i++)
            {
                BaseBonus bonus = GetElementAt(i);
                BonusBody body = bonus.GetComponentInChildren<BonusBody>();
                body.SetId($"{GetIdPrefix()}_{i}");
            }
        }

        public override BaseBonus GetRandomFreeElement()
        {
            BaseBonus bonus = base.GetRandomFreeElement();
            return bonus;
        }

        public BaseBonus GetElementAt(int index)
        {
            return GetPool()[index];
        }

        public int GetIndexOfElement(BaseBonus bonus)
        {
            return GetPool().FindIndex(elem => elem == bonus);
        }

        public int GetViewId()
        {
            return photonView.ViewID;
        }

        protected abstract string GetIdPrefix();

        protected override BaseBonus GetElementPrefab()
        {
            throw new NotImplementedException();
        }

        protected override bool IsAutofilled()
        {
            return false;
        }
    }
}