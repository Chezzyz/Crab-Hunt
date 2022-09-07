using System;
using System.Collections;
using Characters.NPC;
using Items.Bonuses.Entities;
using Photon.Pun;
using Services;
using UnityEngine;

namespace Items.Bonuses
{
    public class BonusGenerator : MonoBehaviourPun
    {
        [Header("Crab")] 
        [SerializeField]
        private CrabPool _crabPool;
        [SerializeField]
        private int _crabsMaximum;
        [SerializeField]
        private float _crabCooldown;
        
        [Header("Bug")] 
        [SerializeField]
        private BugPool _bugPool;
        [SerializeField]
        private int _bugsMaximum;
        [SerializeField]
        private float _bugCooldown;
        
        [Header("Coffee Bonus")] 
        [SerializeField]
        private CoffeeBonusPool _coffeeBonusPool;
        [SerializeField]
        private int _coffeeBonusMaximum;
        [SerializeField]
        private float _coffeeBonusCooldown;
        
        [Header("Holidays Bonus")] 
        [SerializeField]
        private HolidaysBonusPool _holidaysBonusPool;
        [SerializeField]
        private int _holidaysBonusMaximum;
        [SerializeField]
        private float _holidaysBonusCooldown;
        
        [Header("Shield Bonus")] 
        [SerializeField]
        private ShieldBonusPool _shieldBonusPool;
        [SerializeField]
        private int _shieldBonusMaximum;
        [SerializeField]
        private float _shieldBonusCooldown;
        
        [Header("Documents Bonus")] 
        [SerializeField]
        private DocumentsBonusPool _documentsBonusPool;
        [SerializeField]
        private int _documentsBonusMaximum;
        [SerializeField]
        private float _documentsBonusCooldown;

        private void OnEnable()
        {
            GameTimerHandler.GameEnded += OnGameEnded;
        }

        private void OnGameEnded()
        {
            StopAllCoroutines();
        }

        private void Start()
        {
            if (!PhotonNetwork.IsMasterClient) return;
            StartCoroutine(BaseBonusLoop(_crabPool, _crabsMaximum, _crabCooldown));
            StartCoroutine(BaseBonusLoop(_bugPool, _bugsMaximum, _bugCooldown));
            StartCoroutine(BaseBonusLoop(_coffeeBonusPool, _coffeeBonusMaximum, _coffeeBonusCooldown));
            StartCoroutine(BaseBonusLoop(_shieldBonusPool, _shieldBonusMaximum, _shieldBonusCooldown));
            StartCoroutine(BaseBonusLoop(_holidaysBonusPool, _holidaysBonusMaximum, _holidaysBonusCooldown));
            StartCoroutine(BaseBonusLoop(_documentsBonusPool, _documentsBonusMaximum, _documentsBonusCooldown));
        }

        private IEnumerator BaseBonusLoop(BaseBonusPool pool, int maximum, float cooldown)
        {
            WaitForSeconds waitCooldown = new(cooldown);
            
            while (true)
            {
                if (pool.GetActiveElementsCount() < maximum)
                {
                    yield return waitCooldown;
                    BaseBonus bonus = pool.GetRandomFreeElement();
                    bonus.gameObject.SetActive(true);
                    photonView.RPC(nameof(SetOnBonusRPC), RpcTarget.AllBuffered, bonus.GetPool().GetViewId(), bonus.GetSelfIndexInPool());
                }

                yield return null;
            }
        }

        [PunRPC]
        private void SetOnBonusRPC(int poolId, int bonusIndex)
        {
            BaseBonusPool pool = PhotonView.Find(poolId).GetComponent<BaseBonusPool>();
            BaseBonus bonus = pool.GetElementAt(bonusIndex);
            bonus.GetComponentInChildren<BonusBody>(true).gameObject.SetActive(true);
            bonus.gameObject.SetActive(true);
        }
         
        private void OnDisable()
        {
            GameTimerHandler.GameEnded -= OnGameEnded;
        }
    }
}