using UnityEngine;

public class RandomRotate : MonoBehaviour {

    public Vector3 Min = new Vector3(0f, 0f, 0f);
    public Vector3 Max = new Vector3(360f, 360f, 360f);
    public Vector3 pivot;

    private void Start() {
        var pos = transform.position + pivot;
        transform.RotateAround(pos, Vector3.right, Random.Range(Min.x, Max.x));
        transform.RotateAround(pos, Vector3.up, Random.Range(Min.y, Max.y));
        transform.RotateAround(pos, Vector3.forward, Random.Range(Min.z, Max.z));
    }

}