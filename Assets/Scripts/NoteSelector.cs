using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AudioHelm;

public class NoteSelector : MonoBehaviour {
    public int BeatsPerCycle = 64;
    [Range(1, 4)]
    public int OctaveSpan;
    public int minOct,maxOct;
    public int currentOctaveIndex = 4;
    public int rootNote = 12;
    public SequenceGenerator[] sequenceGenerators;
    public DataController dataController;
    double CountDownTime;



    // Use this for initialization
    void Awake ()
    {
        ChangeOctave(minOct,maxOct);
        foreach (var sequenceGenerator in sequenceGenerators)
        {
            sequenceGenerator.minNote = rootNote * OctaveSpan;
        }

        CountDownTime = 60.0 / AudioHelmClock.GetGlobalBpm() * BeatsPerCycle;
    }

    void Start() {
        FindObjectOfType<ResponseController>().loudResponseEvent += UpFifth;
        FindObjectOfType<ResponseController>().quietResponseEvent += DownFourth;
    }
    // Update is called once per frame
    void FixedUpdate ()
    {
        CountDownMacro();
    }

    public void UpFifth() {
        if (rootNote >= 0)
        {
            rootNote += 7;
            //Debug.Log("Up fifth");
        }
    }

    public void DownFourth() {
        if (rootNote >= 5)
        {
            rootNote -= 5;
            //Debug.Log("Down Fourth");
        }

    }

    private void ChangeOctave(int minOct, int maxOct)
    {
        foreach (var sequenceGenerator in sequenceGenerators)
        {
            sequenceGenerator.octaveSpan = Random.Range(1, OctaveSpan);
            currentOctaveIndex = Random.Range(minOct, maxOct);
            sequenceGenerator.minNote = rootNote * currentOctaveIndex;
        }

    }

    private void CountDownMacro()
    {
        CountDownTime -= Time.fixedDeltaTime;
        //Debug.Log(AudioHelmClock.GetGlobalBpm());
        if (CountDownTime <= 0)
        {
            ChangeOctave(minOct,maxOct);
            foreach (var sequenceGenerator in sequenceGenerators)
            {
                sequenceGenerator.Generate();
            }

            CountDownTime = 60.0 / AudioHelmClock.GetGlobalBpm() * BeatsPerCycle;
            //Debug.Log("Current Octave: " + currentOctaveIndex + " Current Min note: " + sequenceGenerator.minNote + " Octave Span:" + sequenceGenerator.octaveSpan);
        }
    }


}
