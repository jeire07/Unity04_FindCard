using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip BGMusic;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.audioSource.clip = BGMusic;
        GameManager.Instance.audioSource.Play();
    }
}
