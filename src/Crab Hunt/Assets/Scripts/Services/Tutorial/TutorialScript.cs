using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Players;
using Services.Tutorial.Tasks;
using UnityEngine;

namespace Services.Tutorial
{
    public class TutorialScript : MonoBehaviour
    {
        [SerializeField] private TutorialWindow _tutorialWindow;
        [SerializeField] private float _completeTaskDelay;
        [SerializeField] private Player _player;

        [Header("Tasks")] 
        [SerializeField] private AbstractTask _moveTask;
        [SerializeField] private AbstractTask _pickupCrabTask;
        [SerializeField] private AbstractTask _killBugTask;
        [SerializeField] private AbstractTask _smashBugTask;
        [SerializeField] private AbstractTask _stunEnemyTask;
        [SerializeField] private AbstractTask _pickupBonusesTask;
        
        private int _currentIndex;
        
        private List<State> _statesList;

        public static event Action<Player> TutorialStarted;

        private void Start()
        {
            InitializeStates();
            SetState(0);
            
        }

        private void InitializeStates()
        {
            _statesList = new ()
            {
                new(
                    @"Добро пожаловать в игру Crab Hunt!
В игре нужно зарабатывать очки соревнуясь с другими игроками.
Для передвижения используются клавиши WASD. Попробуйте!", "Походить во все стороны с помощью WASD", _moveTask),
                new (@"Отлично!
Чтобы получать очки можно подбирать крабов.
Попробуйте подобрать этого краба!", "Подберите краба, наступая на него", _pickupCrabTask),
                new (@"Замечательно!
Так же, чтобы получать очки нужно уничтожать баги.
Для того, чтобы кидать в них бумажку нажмите пробел", "Уничтожьте баг, кинув в него бумажку, с помощью пробела", _killBugTask),
                new (@"Прямо в яблочко!
Но будьте осторожны - если баг в вас врежется, вы временно будете оглушены и потеряете немного очков.
Попробуйте это ощутить.", "Врежьтесь в баг", _smashBugTask),
                new (@"Довольно неприятно, этого стоит избегать.
Так же стреляя бумажками в других игроков вы будете их оглушать.
Попробуйте кинуть бумажку в этого игрока.", "Киньте в игрока бумажку, с помощью пробела", _stunEnemyTask),
                new (@"Хорошо!
Кроме этого на карте будут появляться бонусы в пузырьках.
Кофе (коричневый) временно вас ускорит.
Импортозамещение (синий) временно защитит вас от оглушения.
Отпуск (желтый) оглушит всех соперников на карте.
Документы (бирюзовый) дают много очков, но временно вас замедлят.
Опробуйте все бонусы!", "Попробуйте эффекты всех бонусов, наступая на них", _pickupBonusesTask),
                new (@"На этом все!
Вы готовы играть и побеждать!
Зовите коллег и играйте онлайн, выбрав в меню Онлайн игра.
Если хотите повторить обучение намите кнопку повтора сверху слева.
Если все понятно, нажмите кнопку выхода в меню.", "Зовите коллег и играйте онлайн!", _pickupCrabTask)
            };
        }

        private void OnEnable()
        {
            AbstractTask.TaskCompleted += OnTaskCompleted;
        }

        private void SetState(int index)
        {
            State state = _statesList[index];
            
            _tutorialWindow.SetText(state.Text);
            _tutorialWindow.SetTask(state.TaskDescription);
            state.Task.SetActive(true);
            
            _tutorialWindow.ShowWindow();
        }

        private void OnTaskCompleted()
        {
            if(_currentIndex == 0) TutorialStarted?.Invoke(_player);
                StartCoroutine(CompleteTaskWithDelay(_completeTaskDelay));
        }

        private IEnumerator CompleteTaskWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            _currentIndex++;
            SetState(_currentIndex);
        }

        private void OnDisable()
        {
            AbstractTask.TaskCompleted -= OnTaskCompleted;
        }
    }
}