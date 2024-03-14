using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxySphere : MonoBehaviour
{
    public int _bandIndex;
    public float _startScale, _scaleMultiplier;
    public bool _usingBuffer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_usingBuffer)
        {
            transform.localScale = new Vector3(transform.localScale.x, (Audioanalysis._bandBuffer[_bandIndex] * _scaleMultiplier) + _startScale /2, transform.localScale.z);
            transform.localPosition = new Vector3(transform.localPosition.x, (Audioanalysis._bandBuffer[_bandIndex]), transform.localPosition.z); //this offsets the position of the visualizer to where its not 'double-sided'.

        }
        if (!_usingBuffer) //if not using smoothing, use the rough math.
            transform.localScale = new Vector3(transform.localScale.x, (Audioanalysis._bands[_bandIndex] * _scaleMultiplier) + _startScale, transform.localScale.z);
    }
}
