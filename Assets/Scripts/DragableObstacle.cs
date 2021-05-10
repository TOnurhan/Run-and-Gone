using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragableObstacle : MonoBehaviour
{
    [SerializeField] private float _slideAmount;
    [SerializeField] private float _slideTime;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] Material _selectionMaterial;
    private float _firstMousePosX, _lastMousePosX;
    private bool dragOnce;
    private bool _inDestroyArea = false;
    public static event Action<float> ChangeSpeed;

    public void Initialize()
    {
        EventSystem.PointerDowned += Downed;
        EventSystem.PointerDragged += Dragged;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, 6f))
        {
            _meshRenderer.material = _selectionMaterial;
            _inDestroyArea = true;
        }
    }

    private void Downed(PointerEventData pointerEventData)
    {
        if (_inDestroyArea)
        {
            _firstMousePosX = pointerEventData.position.x;
            dragOnce = true;
        }
    }

    private void Dragged(PointerEventData pointerEventData)
    {
        _lastMousePosX = pointerEventData.position.x;

        if(_lastMousePosX - _firstMousePosX > 100f && dragOnce)
        {
            ChangeSpeed?.Invoke(1);
            dragOnce = false;
            transform.LeanMoveX(transform.position.x + _slideAmount, _slideTime).setOnComplete(() =>
            {
                LeanTween.delayedCall(0.5f, () =>
                {
                    ObstacleDisabled();
                });
            });
        }
    }

    private void ObstacleDisabled()
    {
        gameObject.SetActive(false);
        EventSystem.PointerDowned -= Downed;
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeSpeed?.Invoke(1);
        ObstacleDisabled();
    }
}