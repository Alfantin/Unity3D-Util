using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collision))]
public class CollisionEvents3D : MonoBehaviour {

    public UnityEvent<Collision> OnEnter;
    public UnityEvent<Collision> OnStay;
    public UnityEvent<Collision> OnExit;

    private void OnCollisionEnter(Collision collision) {
        OnEnter?.Invoke(collision);
    }

    private void OnCollisionStay(Collision collision) {
        OnStay?.Invoke(collision);
    }

    private void OnCollisionExit(Collision collision) {
        OnExit?.Invoke(collision);
    }

}