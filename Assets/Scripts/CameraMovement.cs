using System.Collections;
using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private Transform _lookToMap;

    [SerializeField] private float _shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    [SerializeField] private float _decreaseFactor = 1f;

    private Vector3 _currentVelocity;
    [SerializeField] private float _smoothDampValue;
    [SerializeField] private Vector3 _distanceToTarget;
    private bool _isFollowing = true;

    private void Awake()
    {
        _playerBehaviour.HitTheWall += ChangeCameraPosition;
    }
    void FixedUpdate()
    {
        if (_isFollowing)
        {
            FollowPlayer();
        }
    }

    private void FollowPlayer()
    {
        Vector3 targetPos = _playerBehaviour.transform.position + _distanceToTarget;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _currentVelocity, _smoothDampValue);
    }

    private void ChangeCameraPosition()
    {
        StartCoroutine(CameraShake());
        _playerBehaviour.HitTheWall -= ChangeCameraPosition;
    }
    private IEnumerator CameraShake()
    {
        Vector3 originalPos = transform.localPosition;

        while (_shakeDuration > 0)
        {
            transform.localPosition = originalPos + UnityEngine.Random.insideUnitSphere * shakeAmount;
            _shakeDuration -= Time.deltaTime * _decreaseFactor;
            yield return null;
        }

        _shakeDuration = 0f;
        transform.localPosition = originalPos;
        _isFollowing = false;

        StartCoroutine(ShowArea());
    }

    private IEnumerator ShowArea()
    {
        yield return new WaitForSeconds(0.75f);
        transform.LeanMove(_lookToMap.position, 0.5f);
        transform.LeanRotate(new Vector3(_lookToMap.eulerAngles.x, _lookToMap.eulerAngles.y, _lookToMap.eulerAngles.z), 0.5f);
    }
}

