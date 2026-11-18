using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.Timeline;

namespace SaintCharbel
{
    // Lives on the [Scene Director] prefab, next to a PlayableDirector and a Signal Receiver.
    // It plays the scene's Timeline when the scene starts.
    // At a Wait signal it pauses the Timeline and shows a hint, until the visitor does the action
    // (InteractionDone) or waitSeconds pass (then the action happens by itself).
    // At the End Scene signal it goes to nextScene.
    [RequireComponent(typeof(PlayableDirector))]
    public class SceneController : MonoBehaviour
    {
        // One entry per Wait signal on the Timeline, in the same order.
        [System.Serializable]
        public class WaitStep
        {
            [Tooltip("Only for you: what the visitor has to do, e.g. 'Raise the lamp'.")]
            public string label;

            [Tooltip("Called when the Timeline stops here, e.g. RaiseTrigger.ShowHint().")]
            public UnityEvent showHint;

            [Tooltip("Called if the visitor does nothing for waitSeconds, e.g. RaiseTrigger.ForceComplete().")]
            public UnityEvent forceComplete;
        }

        [Tooltip("Name of the scene to open when the Timeline reaches the End Scene signal.")]
        public string nextScene;

        [Tooltip("How long to wait for the visitor before the action happens by itself.")]
        public float waitSeconds = 20f;

        [Tooltip("One entry per Wait signal on the Timeline, in order.")]
        public List<WaitStep> waitSteps = new List<WaitStep>();

        private PlayableDirector director;
        private bool ending;
        private bool waiting;
        private int nextWaitIndex;
        private Coroutine timeout;

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

        // Called by the Signal Receiver when the Timeline reaches a Wait signal.
        public void Wait()
        {
            if (waiting || ending) return;

            if (nextWaitIndex >= waitSteps.Count)
            {
                Debug.LogWarning("[Scene Director] reached a Wait signal, but Wait Steps has no entry for it. Add one in the Inspector.", this);
                return; // don't pause: nothing could ever continue the Timeline
            }

            WaitStep step = waitSteps[nextWaitIndex];
            nextWaitIndex++;

            waiting = true;
            SetTimelineSpeed(0); // freeze the Timeline exactly where it is
            step.showHint.Invoke();
            timeout = StartCoroutine(WaitThenForce(step));
        }

        // Call this when the visitor has done the action (e.g. from RaiseTrigger.OnCompleted).
        // The Timeline continues. If we aren't waiting, it does nothing.
        public void InteractionDone()
        {
            if (!waiting) return;
            waiting = false;

            if (timeout != null)
            {
                StopCoroutine(timeout);
                timeout = null;
            }

            SetTimelineSpeed(1);
        }

        private IEnumerator WaitThenForce(WaitStep step)
        {
            yield return new WaitForSeconds(waitSeconds);
            timeout = null;

            // The action happens by itself. ForceComplete usually fires OnCompleted, which calls
            // InteractionDone(); we call it too in case it isn't connected (a second call does nothing).
            step.forceComplete.Invoke();
            InteractionDone();
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

        // We pause by setting the speed to 0 instead of director.Pause(): Pause() stops evaluating
        // the Timeline, so animated objects can jump back to their start pose. Speed 0 keeps
        // everything exactly where it is.
        private void SetTimelineSpeed(double speed)
        {
            if (!director.playableGraph.IsValid()) return;
            director.playableGraph.GetRootPlayable(0).SetSpeed(speed);
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
