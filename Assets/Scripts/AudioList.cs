using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioList : MonoBehaviour
{
    [Serializable]
    public class AudioWithID
    {
        public string ID;
        public AudioClip clip;
    }

    public AudioWithID[] audios = new AudioWithID[0];

    private static AudioList Instance;

    public static AudioClip Get(string id)
    {
        foreach (AudioWithID audio in Instance.audios)
            if (audio.ID == id) return audio.clip;
        throw new Exception("Audio with id " + id + " not found");
    }

    void Awake(){
        Instance = this;
    }

    void OnDestroy(){
        Instance = null;
    }
}
