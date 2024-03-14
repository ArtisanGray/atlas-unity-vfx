using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioanalysis : MonoBehaviour
{
    AudioSource _music;
    float[] _samples = new float[512];
    public static float[] _bands = new float[8];
    public static float[] _bandBuffer = new float[8];
    public static float[] _bufferDecay = new float[8];

    //float[] _highband = new float[8];
    //public static float[] _audioBuffer = new float[8];
    //public static float[] _audioBand = new float[8];
    // Start is called before the first frame update
    void Start()
    {
        _music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        makeFreqBands();
        BandDecay();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
            //createBands();
        }
    void GetSpectrumAudioSource() //grabs spectrum data from whatever is playing.
    {
        _music.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }
    /*void createBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_bands[i] > _highband[i])
            {
                _highband[1] = _bands[i];

            }
            if (_highband[i] != 0)
            {
                _audioBand[i] = (_bands[i] / _highband[i]);
                _audioBuffer[i] = (_bandBuffer[i] / _highband[i]);
            }
        }

        this smooths out the bands and makes them more homogenous in use for different systems across Unity -- in other words, cuts values down to a value between 0 and 1.
    }*/
    void makeFreqBands() //creates the respective bands used in the visualizer as well as their value.
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)(Mathf.Pow(2, i) * 2);

            if (i == 7)
                sampleCount += 2;

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }
            average /= count;
            _bands[i] = average * 10;
        }
    }
    void BandDecay() //provides a small blending to the movement of the visualizer -- making it less jerky.
    {
        for (int g = 0; g < 8; g++)
        {
            if (_bands[g] > _bandBuffer[g]) 
            {
                _bandBuffer[g] = _bands[g];
                _bufferDecay[g] = 0.005f;
            }
            if (_bands[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecay[g];
                _bufferDecay[g] *= 1.2f;
            }
        }
    }
}
