using UnityEngine;

public class OrbitMovement : MonoBehaviour
{
    private Transform _target;

    [Tooltip("Positive speed will rotate clockwise, negative speed will rotate counter-clockwise.")]
    [SerializeField]
    private float _moveSpeed;

    [SerializeField]
    private float _minimumSpeed;
    [SerializeField]
    private float _maximumSpeed;

    private float _distanceToTargetAtFrameStart;

    private void Start()
    {
        _target = FindFirstObjectByType<PlayerMovement>().transform;
    }

    private void Update()
    {
        UpdateDistanceToTarget();
        LookAtTarget();
        OrbitAroundObject();
        MaintainDistanceToTarget();
    }

    // updates the distance to the target each frame
    private void UpdateDistanceToTarget()
    {
        _distanceToTargetAtFrameStart = GetDistanceToTarget().magnitude;
    }

    // makes object look at target
    private void LookAtTarget()
    {
        transform.up = GetDistanceToTarget().normalized;
    }

    private void OrbitAroundObject()
    {
        Vector3 distanceToTarget = GetDistanceToTarget();

        // calculate movement speed value
        float orbitSpeed = _moveSpeed * distanceToTarget.magnitude;

        // clamp speed to minimum and maximum
        orbitSpeed = Mathf.Clamp(orbitSpeed, _minimumSpeed, _maximumSpeed) * Mathf.Sign(_moveSpeed);

        // use -transform.right to rotate clockwise when speed is positive
        Vector3 orbitMovementAxis = Vector3.Cross(Camera.main.transform.forward, transform.up);
        transform.position += orbitMovementAxis * orbitSpeed;
    }

    private void MaintainDistanceToTarget()
    {
        float sqrMagnitudeToTarget = GetDistanceToTarget().magnitude;
        float distanceDifferenceAtEndOfFrame = sqrMagnitudeToTarget - _distanceToTargetAtFrameStart;

        Vector3 correctionMovement = GetDistanceToTarget().normalized * distanceDifferenceAtEndOfFrame;
        transform.position += correctionMovement;
    }

    private Vector3 GetDistanceToTarget()
    {
        return _target.position - transform.position;
    }
}
