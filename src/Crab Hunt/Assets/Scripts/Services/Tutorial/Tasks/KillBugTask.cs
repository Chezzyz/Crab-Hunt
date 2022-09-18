using System;
using Characters.NPC;
using Characters.Players;
using UnityEngine;

namespace Services.Tutorial.Tasks
{
    public class KillBugTask : AbstractTask
    {
        [SerializeField] private Bug _bug;

        private void OnEnable()
        {
            PlayerScoreHandler.ScoreChanged += OnBugKilled;
        }

        private void OnBugKilled(Player arg1, int diff, int score)
        {
            if(_isActive && !_isCompleted) CompleteTask();
        }

        protected override void OnActive()
        {
            _bug.gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            PlayerScoreHandler.ScoreChanged += OnBugKilled;
        }
    }
}