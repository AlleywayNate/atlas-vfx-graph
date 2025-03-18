using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightOnAudio : MonoBehaviour
{
    public int _band; // The frequency band to use for controlling light intensity
    public float _minIntensity = 0f; // Minimum light intensity
    public float _maxIntensity = 1f; // Maximum light intensity

    Light _light; // Reference to the Light component

    // Use this for initialization
    void Start()
    {
        _light = GetComponent<Light>(); // Get the Light component
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the band index is within the valid range
        if (_band >= 0 && _band < AudioPeer._audioBandBuffer.Length)
        {
            // Set the light intensity based on the buffered frequency band value
            _light.intensity = (AudioPeer._audioBandBuffer[_band] * (_maxIntensity - _minIntensity)) + _minIntensity;
        }
    }
}
