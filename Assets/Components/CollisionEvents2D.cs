using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collision2D))]
public class CollisionEvents2D : MonoBehaviour {

    public UnityEvent<Collision2D> OnEnter;
    public UnityEvent<Collision2D> OnStay;
    public UnityEvent<Collision2D> OnExit;

    private void OnCollisionEnter2D(Collision2D collision) {
        OnEnter?.Invoke(collision);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        OnStay?.Invoke(collision);
    }

    private void OnCollisionExit2D(Collision2D collision) {
        OnExit?.Invoke(collision);
    }

}