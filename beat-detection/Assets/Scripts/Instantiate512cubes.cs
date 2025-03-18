using UnityEngine;

public class Instantiate512Cubes : MonoBehaviour
{
    public GameObject _sampleCubePrefab; // Prefab for the cubes
    public float _maxScale = 100f; // Maximum scale for the cubes
    public float _radius = 100f; // Radius of the circle for cube placement

    GameObject[] _sampleCubes = new GameObject[512]; // Array to store the cubes

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate 512 cubes in a circular formation
        for (int i = 0; i < 512; i++)
        {
            // Calculate the angle for each cube
            float angle = i * Mathf.PI * 2 / 512;

            // Calculate the position of the cube in a circle
            Vector3 position = new Vector3(Mathf.Cos(angle) * _radius, 0, Mathf.Sin(angle) * _radius);

            // Instantiate the cube
            GameObject instanceSampleCube = Instantiate(_sampleCubePrefab, position, Quaternion.identity);

            // Set the cube's parent to this object
            instanceSampleCube.transform.parent = this.transform;

            // Name the cube for easier debugging
            instanceSampleCube.name = "SampleCube" + i;

            // Store the cube in the array
            _sampleCubes[i] = instanceSampleCube;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Scale cubes based on audio spectrum data
        for (int i = 0; i < 512; i++)
        {
            if (_sampleCubes[i] != null && AudioPeer._samples != null && i < AudioPeer._samples.Length)
            {
                // Scale the cube based on the audio spectrum data
                float scale = (AudioPeer._samples[i] * _maxScale) + 2;
                _sampleCubes[i].transform.localScale = new Vector3(1, scale, 1);
            }
        }
    }
}