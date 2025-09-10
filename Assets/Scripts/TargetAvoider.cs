using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TargetAvoider : MonoBehaviour
{
    private Transform _target;

    private CircleCollider2D _collider;

    [SerializeField]
    private float _moveAwayFromTargetSpeed;

    private bool _targetInRange;

    void Start()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.isTrigger = true;

        _target = FindFirstObjectByType<PlayerMovement>().transform;
    }

    private void Update()
    {
        if (_targetInRange)
        {
            MoveAwayFromTarget();
        }
    }

    private void MoveAwayFromTarget()
    {
        Vector3 moveAwayForce = (transform.position - _target.position).normalized * _moveAwayFromTargetSpeed;

        transform.position += moveAwayForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == _target)
        {
            _targetInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform == _target)
        {
            _targetInRange = false;
        }
    }
}
