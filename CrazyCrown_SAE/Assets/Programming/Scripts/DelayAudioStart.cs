using UnityEngine;

public class DelayedAudioStart : MonoBehaviour
{
    public AudioSource audioSource;
    public float delayInSeconds = 5f;

    private bool hasStarted = false;

    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            Invoke("StartAudio", delayInSeconds);
        }
        else
        {
            Debug.LogWarning("AudioSource is not assigned!");
        }
    }

    void StartAudio()
    {
        if (!hasStarted)
        {
            audioSource.Play();
            hasStarted = true;
        }
    }
}
