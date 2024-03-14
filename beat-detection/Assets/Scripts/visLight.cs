using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visLight : MonoBehaviour
{
    public int _bandIndex;
    public float _minBrightness, _maxBrightness;
    Light _light;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        _light.intensity = (Audioanalysis._bandBuffer[_bandIndex] * (_maxBrightness-_minBrightness)) + _minBrightness;
    }
}
