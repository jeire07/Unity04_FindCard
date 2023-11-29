using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip bgmusic;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.I.audioSource.clip = bgmusic;
        GameManager.I.audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
