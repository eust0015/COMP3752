using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Utilities
{
    public class LoadSceneUtility : Utility
    {
        [SerializeField] private string sceneName;

        public string SceneName
        {
            get => sceneName;
            set => sceneName = value;
        }

        public override void DoUtility()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}