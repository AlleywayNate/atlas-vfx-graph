using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    AudioSource _audioSource;
    public static float[] _samples = new float[512];
    float[] _freqBands = new float[8]; // Array to store frequency bands
    float[] _bandBuffer = new float[8]; // Array to store frequency band buffer
    float[] _bufferDecrease = new float[8]; // Array to store buffer decrease values
    
    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];
    // Use this for initialization
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void BandBuffer()
    {
        for (int g = 0; g < 8; g++)
        {
            // If the current frequency band value is greater than the buffer value
            if (_freqBands[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBands[g]; // Update the buffer to match the frequency band
                _bufferDecrease[g] = 0.005f; // Reset the decrease rate
            }

            // If the current frequency band value is less than the buffer value
            if (_freqBands[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g]; // Gradually decrease the buffer value
                _bufferDecrease[g] *= 1.2f; // Increase the decrease rate for a smoother falloff
            }
        }
    }

    void MakeFrequencyBands()
    {
        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;

            if (i == 7)
            {
                sampleCount += 2; // Include remaining samples in the last band
            }

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= sampleCount;
            _freqBands[i] = average * 10; // Scale the average for better visualization
        }
    }
}
