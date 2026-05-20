using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class OpeningScene : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "GameScene";
    [SerializeField] private PlayableDirector fadeOutTimeline;

    private void Start()
    {
        fadeOutTimeline.stopped += OnFadeOutFinished;
    }

    public void ContinueGame()
    {
        fadeOutTimeline.Play();
    }

    private void OnFadeOutFinished(PlayableDirector director)
    {
        SceneManager.LoadScene(gameSceneName);
    }

    private void OnDestroy()
    {
        fadeOutTimeline.stopped -= OnFadeOutFinished;
    }
}