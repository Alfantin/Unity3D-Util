using UnityEngine;

public class RandomPosition : MonoBehaviour {

    public Vector3 Min = new Vector3(0.75f, 0.75f, 0.75f);
    public Vector3 Max = new Vector3(1.25f, 1.25f, 1.25f);

    private void Start() {
        transform.position += new Vector3(
            Random.Range(Min.x, Max.x),
            Random.Range(Min.y, Max.y),
            Random.Range(Min.z, Max.z)
        );
    }

}