using System;
using System.Collections;
using Characters.NPC;
using Characters.Players;
using UnityEngine;

namespace Services.Tutorial.Tasks
{
    public class SmashBugTask : AbstractTask
    {
        [SerializeField] private Bug _bug;

        private void OnEnable()
        {
            PlayerScoreHandler.ScoreChanged += OnPlayerScoreChanged;
        }

        private void OnPlayerScoreChanged(Player arg1, int diff, int score)
        {
            if(diff < 0 && _isActive && !_isCompleted) CompleteTask();
            if(diff > 0 && _isActive && !_isCompleted) StartCoroutine(SetActiveAfterDelay());
        }

        private IEnumerator SetActiveAfterDelay()
        {
            yield return new WaitForSeconds(1);
            _bug.gameObject.SetActive(true);
        }

        protected override void OnActive()
        {
            _bug.gameObject.SetActive(true);
        }
        
        private void OnDisable()
        {
            PlayerScoreHandler.ScoreChanged -= OnPlayerScoreChanged;
        }
    }
}