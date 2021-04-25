using UnityEngine;

[CreateAssetMenu(menuName = "Speech Item")]
public class Speech : ScriptableObject
{
    public string SubtitlesEng, SubtitlesRus;
    public string SourceID;
    public float Duration;
    public AudioClip Sound;
    public Speech Next;
}
