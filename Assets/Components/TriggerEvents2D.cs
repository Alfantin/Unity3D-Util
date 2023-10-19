using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collision2D))]
public class TriggerEvents2D : MonoBehaviour {

    public UnityEvent<Collider2D> OnEnter;
    public UnityEvent<Collider2D> OnStay;
    public UnityEvent<Collider2D> OnExit;

    private void OnTriggerEnter2D(Collider2D collision) {
        OnEnter?.Invoke(collision);
    }

    private void OnTriggerStay2D(Collider2D collision) {
        OnStay?.Invoke(collision);
    }

    private void OnTriggerExit2D(Collider2D collision) {
        OnExit?.Invoke(collision);
    }

}