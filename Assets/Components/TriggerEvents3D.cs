using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collision))]
public class TriggerEvents3D : MonoBehaviour {

    public UnityEvent<Collider> OnEnter;
    public UnityEvent<Collider> OnStay;
    public UnityEvent<Collider> OnExit;

    private void OnTriggerEnter(Collider collision) {
        OnEnter?.Invoke(collision);
    }

    private void OnTriggerStay(Collider collision) {
        OnStay?.Invoke(collision);
    }

    private void OnTriggerExit(Collider collision) {
        OnExit?.Invoke(collision);

    }

}