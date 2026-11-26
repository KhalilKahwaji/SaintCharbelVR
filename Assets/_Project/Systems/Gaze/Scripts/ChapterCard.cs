using UnityEngine;
using UnityEngine.SceneManagement;

namespace SaintCharbel
{
    // One card of the chapter menu (01_Menu).
    // It knows its chapter number and the first scene of that chapter.
    // Connect GazeTarget's On Selected () to Open() in the Inspector.
    public class ChapterCard : MonoBehaviour
    {
        [Tooltip("The chapter this card opens: 1, 2, 3 or 4.")]
        public int chapterNumber = 1;

        [Tooltip("The exact name of the chapter's first scene, for example C1_S01_TheMountain. Empty = only a message in the Console.")]
        public string firstScene;

        // Opens this card's chapter.
        public void Open()
        {
            Debug.Log("Chapter card: opening chapter " + chapterNumber);

            // TODO: AppState.CurrentChapter = chapterNumber; (when Christy's AppState is merged)

            if (string.IsNullOrEmpty(firstScene))
            {
                Debug.LogWarning(name + ": no First Scene set in the Inspector, so nothing is loaded.");
                return;
            }

            // TODO: use SceneFlow.GoTo(firstScene) when Christy's fader is merged (it fades out first).
            SceneManager.LoadScene(firstScene);
        }
    }
}
