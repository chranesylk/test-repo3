using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;
using JulianSchoenbaechler.MicDecode;

public class AudioController : MonoBehaviour {
    public float fadeChanceIncrement = 0.005f;
    public NoteSelector noteSelector;
    MicDecode microphone;
    float fadeChance = 0;
    List<HelmSequencer> synths = new List<HelmSequencer>();
    List<AudioSource> audioSources = new List<AudioSource>();


    void Start() {
        FindObjectOfType<ResponseController>().silentResponseEvent += FadeChanceIncrease;
        foreach (var synth in FindObjectsOfType<HelmSequencer>())
        {
            synths.Add(synth);
            audioSources.Add(synth.GetComponent<AudioSource>());
        }
        
    }

    void FadeChanceIncrease()
    {
        if (fadeChance <=1 )
        {
            fadeChance += fadeChanceIncrement;
            //Debug.Log("Current fade chance is: " + fadeChance);
        }
    }

    public void FadeAudioTest() {

        if (Random.Range(0f,1f) < fadeChance && audioSources.Count >= 1)
        {
            fadeChance = 0f;
            int fadeIndex = Random.Range(0, audioSources.Count);
            Debug.Log(fadeIndex);
            StartCoroutine(FadeAudio(audioSources[fadeIndex]));
            synths[fadeIndex].enabled = false;
            audioSources.Remove(audioSources[fadeIndex]);
            synths.Remove(synths[fadeIndex]);
        }
    }


    IEnumerator FadeAudio(AudioSource audioSource) {
        while (audioSource.volume >= 0.001f)
        {
            audioSource.volume *= 0.9f;
            yield return new WaitForSeconds(0.001f);
        }
        audioSource.enabled = false;

    }


}
