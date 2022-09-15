using ArcadeIdle.Helpers;
using ArcadeIdle.Helpers.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArcadeIdle.Core
{
    public class GameLoadManager : Singleton<GameLoadManager>
    {
        [SerializeField] private SceneReferance sceneReferance;
        [SerializeField] private GameEvent onGameStarted;
        private void Start()
        {
            Application.targetFrameRate = 60;
            LoadSceneAdditive();
        }

        private void LoadSceneAdditive()
        {
            for (int i = 0; i < sceneReferance.beginScenes.Count; i++)
            {
                SceneManager.LoadScene((string) sceneReferance.beginScenes[i].name, LoadSceneMode.Additive);
            }
            
            onGameStarted.Invoke();
        }
    }
}