using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanager : MonoBehaviour
{
    public static soundmanager instance;

    AudioSource myAudio;

    public AudioClip btnclick;
    public AudioClip shotgun;
    public AudioClip playerdeath;
    public AudioClip enemydeath;
    public AudioClip jump;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        myAudio=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void btnsound()
    {
        myAudio.PlayOneShot(btnclick);
    }

    public void gunsound()
    {
        myAudio.PlayOneShot(shotgun);
    }

    public void pdeath()
    {
        myAudio.PlayOneShot(playerdeath);
    }

    public void edeath()
    {
        myAudio.PlayOneShot(enemydeath);
    }

    public void jumpsound()
    {
        myAudio.PlayOneShot(jump);
    }
}
