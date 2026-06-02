using UnityEngine;

public class Music: MonoBehaviour
{
    public static Music Instance;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip normalBGM;
    [SerializeField] private AudioClip finalClueBGM;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayNormalBGM();
    }

    public void PlayNormalBGM()
    {
        audioSource.clip = normalBGM;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayFinalBookBGM()
    {
        if (audioSource.clip == finalClueBGM) return;

        audioSource.clip = finalClueBGM;
        audioSource.loop = true;
        audioSource.Play();
    }
}