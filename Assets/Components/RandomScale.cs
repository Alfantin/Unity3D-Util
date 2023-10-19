using UnityEngine;

public class RandomScale : MonoBehaviour {

    public Vector3 Min = new Vector3(0.75f, 0.75f, 0.75f);
    public Vector3 Max = new Vector3(1.25f, 1.25f, 1.25f);
    public bool seperated;

    private void Start() {
        if (seperated) {
            transform.localScale = new Vector3(
                Random.Range(Min.x, Max.x),
                Random.Range(Min.y, Max.y),
                Random.Range(Min.z, Max.z)
            );
        }
        else {
            var random = Random.value;
            transform.localScale = new Vector3(
                remap(random, 0f, Min.x, 1f, Max.x),
                remap(random, 0f, Min.y, 1f, Max.y),
                remap(random, 0f, Min.z, 1f, Max.z)
            );
        }
    }

    private float remap(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}