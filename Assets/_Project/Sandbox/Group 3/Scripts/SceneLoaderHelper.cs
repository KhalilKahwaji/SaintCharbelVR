using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaintCharbel
{
    // Loads a scene by name. A Signal Receiver calls LoadTargetScene("TestB")
    // when the Timeline reaches the Signal at the end.
    public class SceneLoaderHelper : MonoBehaviour
    {
        public void LoadTargetScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
