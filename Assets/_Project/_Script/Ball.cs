using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Launc(Vector3 direction, float impulse)
    {
        direction.z = 0f;
        direction.Normalize();

        _rigidbody.AddForce(direction * impulse, ForceMode.Impulse);
    }

    
}
