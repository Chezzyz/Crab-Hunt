using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField]
        private string _mainMenuScene;
        [SerializeField]
        private float _sceneTransitionDuration;

        public static event System.Action<string> SceneLoaded;
        public static event System.Action<float> SceneLoadStarted;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(LoadSceneWithDelay(sceneName, _sceneTransitionDuration));
        }

        private IEnumerator LoadSceneWithDelay(string sceneName, float delay = -1)
        {
            if(delay == -1)
            {
                delay = _sceneTransitionDuration;
            }
            if (gameObject != null)
            {
                SceneLoadStarted?.Invoke(delay);
            }
            yield return new WaitForSecondsRealtime(delay);
            SceneManager.LoadScene(sceneName);
        }

        public void LoadMainMenu()
        {
            LoadScene(_mainMenuScene);
        }

        public void LoadRoom()
        {
            PhotonNetwork.LoadLevel("Room");
        }
        
        public void LoadLobby()
        {
            PhotonNetwork.LoadLevel("Lobby");
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!gameObject.activeInHierarchy) return;
            PhotonNetwork.IsMessageQueueRunning = true;
            SceneLoaded?.Invoke(scene.name);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}

