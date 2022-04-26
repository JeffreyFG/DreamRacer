using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class TitleScreenTransition : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource mySound;
    void Start()
    {
        Console.WriteLine("loaded");
        var audioClip = Resources.Load<AudioClip>("TitleSong");  //Load the AudioClip from the Resources Folder
        mySound.clip = audioClip;  
        mySound.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
           SceneManager.LoadScene(1);
        }  
    }
}
