using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private CameraMovement _cameraBehaviour;
    [SerializeField] private GameController _gameController;
    [SerializeField] private Rigidbody _rigidBody;
    [SerializeField] private float _verticalSpeed;
    public event Action HitTheWall;
    public Animation anim;

    private void Awake()
    {
        DragableObstacle.ChangeSpeed += SpeedChange;
        BreakableObstacle.ChangeSpeed += SpeedChange;
        anim.Play("Idle");
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(_gameController.gameStarted)
        {
            anim.Play("Run");
            Vector3 direction = new Vector3(_rigidBody.velocity.x, _rigidBody.velocity.y, _verticalSpeed);
            _rigidBody.velocity = direction;
        }
    }

    public void SpeedChange(float changeAmount)
    {
        _verticalSpeed += changeAmount;
        _cameraBehaviour.shakeAmount += 0.05f * changeAmount;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.Play("Idle");
            _rigidBody.velocity = Vector3.zero;
            HitTheWall?.Invoke();
        }
    }
}