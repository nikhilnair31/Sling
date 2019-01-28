using UnityEngine;
using System.Collections;

    public class SoundManager : MonoBehaviour 
    {
        public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
        public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
        public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.               
        
        //Check if there is already an instance of SoundManager
        //if not, set it to this.
        //If instance already exists:
        //Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
        //Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
        void Awake ()
        {
            /*if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
            
            DontDestroyOnLoad (gameObject);*/
            instance = this;
        }

        public void PlaySingle(AudioClip clip)
        {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource.clip = clip;
            efxSource.Play ();
        }
    }