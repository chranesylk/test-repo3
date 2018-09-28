using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JulianSchoenbaechler.MicDecode;

public class PitchText : MonoBehaviour {
    public MicDecode microphone;
    Text pitchText;

	// Use this for initialization
	void Start () {
        pitchText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        pitchText.text = microphone.Frequency.ToString("F0") + " Hz" + "\n" + microphone.VolumeRMS + " db";
	}
}
