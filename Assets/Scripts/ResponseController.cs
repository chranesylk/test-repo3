using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JulianSchoenbaechler.MicDecode;
using AudioHelm;

public class ResponseController : MonoBehaviour {
    public delegate void SoundResponse();
    //public event SoundResponse generalResponseEvent;
    public event SoundResponse loudResponseEvent;
    public event SoundResponse quietResponseEvent;
    public event SoundResponse  silentResponseEvent;
    public DataController dataController;
    MicDecode microphone;
	// Use this for initialization
	void Start () {
        microphone = GetComponent<MicDecode>();
        microphone.StartRecording();
        StartCoroutine(TestEveryBeat());
	}




    void RespondToSound(float volume) {

        if (volume > dataController.Loudness[0]) //Loudest tier
        {
            loudResponseEvent();
        }
        else if (volume < dataController.Loudness[3]) //Quietest tier
        {
            silentResponseEvent();
        }
        else if (volume < dataController.Loudness[2]  )// -4 degree
        {
            quietResponseEvent();
        }
    }


    IEnumerator TestEveryBeat() {
        while (true)
        {
            RespondToSound(microphone.VolumeRMS);
            FindObjectOfType<AudioController>().FadeAudioTest();
            yield return new WaitForSeconds(60.0f / AudioHelmClock.GetGlobalBpm());
        }
    }
}
