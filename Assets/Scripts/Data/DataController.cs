using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataController:MonoBehaviour  {


    //Min and max for each octave
    public float[,] Octave2D = new float[,] { {16.351f, 30.868f },{32.703f, 61.735f}, 
                                              {65.406f, 123.471f}, {130.813f, 246.942f},
                                              {261.626f, 493.883f}, {523.251f, 987.767f},
                                              {1046.502f, 1975.533f}, {2093.005f, 3951.066f},
                                              {4186.009f, 7902.132f},{8372.018f, 15804.264f} };
    public float[] Loudness = new float[4];


}
