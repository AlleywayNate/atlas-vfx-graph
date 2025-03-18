using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band; // The frequency band to use for scaling and color
    public float _startScale = 1f; // Base scale of the cube
    public float _maxScale = 10f; // Maximum scale multiplier
    public bool _useBuffer; // Whether to use the buffered frequency band values

    Material _material; // Material for emission color

    // Use this for initialization
    void Start()
    {
        _material = GetComponent<MeshRenderer>().materials[0]; // Get the material of the cube
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the band index is within the valid range
        if (_band >= 0 && _band < AudioPeer._audioBand.Length)
        {
            float scale;
            Color color;

            if (_useBuffer)
            {
                // Use the buffered normalized value
                scale = (AudioPeer._audioBandBuffer[_band] * _maxScale) + _startScale;
                color = new Color(AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band], AudioPeer._audioBandBuffer[_band]);
            }
            else
            {
                // Use the raw normalized value
                scale = (AudioPeer._audioBand[_band] * _maxScale) + _startScale;
                color = new Color(AudioPeer._audioBand[_band], AudioPeer._audioBand[_band], AudioPeer._audioBand[_band]);
            }

            // Apply the scale to the cube
            transform.localScale = new Vector3(transform.localScale.x, scale, transform.localScale.z);

            // Set the emission color of the material
            _material.SetColor("_EmissionColor", color);
        }
    }
}
