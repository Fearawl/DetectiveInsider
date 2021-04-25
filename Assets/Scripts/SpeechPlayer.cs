using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeechPlayer : MonoBehaviour
{
    public static SpeechPlayer Instance { get; private set; }

    public TextMeshProUGUI subtitlesText;
    Dictionary<string, AudioSource> sources = new Dictionary<string, AudioSource>();

    void Awake(){
        Instance = this;
        foreach (var source in FindObjectsOfType<AudioSource>()){
            sources[source.GetComponent<Tag>().ID] = source;
        }
    }

    public void Play(Speech speech){
        if (speech.Sound != null && sources.ContainsKey(speech.SourceID))
            sources[speech.SourceID].PlayOneShot(speech.Sound);
        
        if (PlayerPrefs.GetString("language") == "ru")
            subtitlesText.text = speech.SubtitlesRus;
        else
            subtitlesText.text = speech.SubtitlesEng;
        
        StopAllCoroutines();
        StartCoroutine(OnSpeechEnd(speech));
    }

    IEnumerator OnSpeechEnd(Speech speech){
        yield return new WaitForSeconds(speech.Duration);
        if (speech.Next != null) Play(speech.Next);
    }

    void OnDestroy(){
        Instance = null;
    }
}
