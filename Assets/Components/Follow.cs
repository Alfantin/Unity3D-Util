using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform target;
    public float smooth = 0f;
    public float lookAt = 0f;

    public bool lockX;
    public bool lockY;
    public bool lockZ;

    public Vector3 minFallow;
    public Vector3 maxFallow;

    private Vector3 distance;
    private Vector3 startPos;

    private void Awake() {
        startPos = transform.position;
        distance = target.position - startPos;
        if (lookAt > 0f) {
            transform.LookAt(target);
        }
    }

    private void LateUpdate() {

        if (lookAt > 0f) {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target.position - transform.position), lookAt * Time.deltaTime);
        }

        var pos = target.position - distance;

        if (lockX) pos.x = transform.position.x;
        if (lockY) pos.y = transform.position.y;
        if (lockZ) pos.z = transform.position.z;

        if (minFallow.x != 0) pos.x = Mathf.Max(pos.x, minFallow.x);
        if (minFallow.y != 0) pos.y = Mathf.Max(pos.y, minFallow.y);
        if (minFallow.z != 0) pos.z = Mathf.Max(pos.z, minFallow.z);

        if (maxFallow.x != 0) pos.x = Mathf.Min(pos.x, maxFallow.x);
        if (maxFallow.y != 0) pos.y = Mathf.Min(pos.y, maxFallow.y);
        if (maxFallow.z != 0) pos.z = Mathf.Min(pos.z, maxFallow.z);

        if (smooth == 0f) {
            transform.position = pos;
        }
        else {
            transform.position = Vector3.Lerp(transform.position, pos, smooth * Time.deltaTime);
        }

    }

}