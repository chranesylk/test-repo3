// Copyright 2017 Matt Tytel

using UnityEngine;
using System.Collections.Generic;

namespace AudioHelm
{
    [AddComponentMenu("")]
    public class SequenceGenerator : MonoBehaviour
    {
        public enum TypeOfNote : int  {sixteenth = 1, eighth, quater, half, whole};
        public TypeOfNote typeOfSequence;
        public HelmSequencer sequencer;
        public int[] scale = { 0, 2, 4, 7, 9 };

        public int minNote = 24;
        public int octaveSpan = 2;
        public float minDensity = 0.5f;
        public float maxDensity = 1.0f;

        void GenerateRhythm()
        {
        }

        void GenerateMelody()
        {
        }

        void Start()
        {
            Generate();
        }

        int GetKeyFromRandomWalk(int note)
        {
            int octave = note / scale.Length;
            int scalePosition = note % scale.Length;
            return minNote + octave * Utils.kNotesPerOctave + scale[scalePosition];
        }

        int GetNextNote(int current, int max)
        {
            int next = current + Random.Range(-3, 3);

            if (next > max)
                return 2 * max - next;
            if (next < 0)
                return Mathf.Abs(next);

            return next;
        }

        public void Generate()
        {
            sequencer.Clear();

            int maxNote = scale.Length * octaveSpan;
            int currentNote = Random.Range(0, maxNote);
            Note lastNote = sequencer.AddNote(GetKeyFromRandomWalk(currentNote), 0, (int)typeOfSequence);

            for (int i = (int)typeOfSequence; i < sequencer.length; i += (int)typeOfSequence)
            {
                float density = Random.Range(minDensity, maxDensity);

                if (Random.Range(0.0f, 1.0f) < density)
                {
                    currentNote = GetNextNote(currentNote, maxNote);
                    lastNote = sequencer.AddNote(GetKeyFromRandomWalk(currentNote), i, i + (int)typeOfSequence);
                }
                else
                    lastNote.end = i + (int)typeOfSequence;
            }
        }
    }
}
