using UnityEngine;

public class Launcher : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _aimPivot;
    [SerializeField] private Transform _muzzle;
    [SerializeField] private Ball _ballPrefab;

    [Header("Aim")]
    [SerializeField, Range(0f, 89f)] private float _maxAimAngle = 80f;

    [Header("Launch")]
    [SerializeField] private float _launchImpulse = 10f;

    private Vector3 _shootDirection = Vector3.down;
    private bool _canShoot = true;

    private void OnEnable()
    {
        ShotEvents.ShotEnded += OnShotEnded;
    }

    private void OnDisable()
    {
        ShotEvents.ShotEnded -= OnShotEnded;
    }

    private void Update()
    {
        UpdateAim();

        if (_canShoot && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void UpdateAim()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Plane boardPlane = new Plane(Vector3.forward, _muzzle.position);

        if (!boardPlane.Raycast(ray, out float distance)) return;

        Vector3 target = ray.GetPoint(distance);
        Vector3 rawDirection = target - _muzzle.position;

        rawDirection.z = 0f;

        if (rawDirection.sqrMagnitude <= 0.001f) return;

        Vector2 direction2D = new Vector2(rawDirection.x, rawDirection.y).normalized;
        float angle = Vector2.SignedAngle(Vector2.down, direction2D);
        angle = Mathf.Clamp(angle, -_maxAimAngle, _maxAimAngle);

        _shootDirection = Quaternion.AngleAxis(angle, Vector3.forward)* Vector3.down;
        _aimPivot.rotation = Quaternion.FromToRotation(Vector3.down, _shootDirection);
    }

    private void Shoot()
    {
        _canShoot = false;

        Ball ball = Instantiate(_ballPrefab, _muzzle.position, Quaternion.identity);

        ball.Launch(_shootDirection, _launchImpulse);
    }

    private void OnShotEnded(bool freeBall)
    {
        _canShoot = true;
    }
}
