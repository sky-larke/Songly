using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource audioSource;

    // singleton stuff
    public static SoundManager Instance;
    void Awake() {
        Instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlaySound(AudioClip sound) {
        audioSource.clip = sound;
        audioSource.Play();
    }
}
