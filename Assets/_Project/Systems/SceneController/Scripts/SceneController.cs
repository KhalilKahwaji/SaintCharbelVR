using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

namespace SaintCharbel
{
    // Lives on the [Scene Director] prefab, next to a PlayableDirector and a Signal Receiver.
    // It plays the scene's Timeline when the scene starts. When the Timeline reaches the
    // End Scene signal, the Signal Receiver calls EndScene() and we go to nextScene.
    [RequireComponent(typeof(PlayableDirector))]
    public class SceneController : MonoBehaviour
    {
        [Tooltip("Name of the scene to open when the Timeline reaches the End Scene signal.")]
        public string nextScene;

        private PlayableDirector director;
        private bool ending;

        private void Awake()
        {
            director = GetComponent<PlayableDirector>();
            director.playOnAwake = false; // we start it ourselves in Start()
        }

        private void Start()
        {
            if (director.playableAsset == null)
            {
                Debug.LogWarning("[Scene Director] has no Timeline. Drag the scene's Timeline into the Playable Director.", this);
                return;
            }

            BindSignalTracksToMe();
            director.Play();
        }

        // Called by the Signal Receiver when the Timeline reaches the End Scene signal.
        public void EndScene()
        {
            if (ending) return; // only once, even if the signal fires twice
            ending = true;

            if (string.IsNullOrEmpty(nextScene))
            {
                Debug.LogWarning("[Scene Director] reached End Scene but nextScene is empty.", this);
                return;
            }

            // TODO: use SceneFlow.GoTo(nextScene) once Group 2's SceneFlow is merged (it fades).
            SceneManager.LoadScene(nextScene);
        }

        // A Signal Track only reaches a Signal Receiver if the track is bound to its GameObject.
        // So that nobody forgets this step, any Signal Track that isn't bound yet is bound to us.
        private void BindSignalTracksToMe()
        {
            foreach (PlayableBinding output in director.playableAsset.outputs)
            {
                if (output.sourceObject is SignalTrack && director.GetGenericBinding(output.sourceObject) == null)
                {
                    director.SetGenericBinding(output.sourceObject, gameObject);
                }
            }
        }
    }
}
