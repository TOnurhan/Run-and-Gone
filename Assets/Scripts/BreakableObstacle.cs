using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class BreakableObstacle : MonoBehaviour
{
    [SerializeField] private int _obstacleHealth;
    [SerializeField] MeshRenderer _meshRenderer;
    [SerializeField] MeshFilter _meshFilter;
    [SerializeField] Mesh secondVersion;
    [SerializeField] Material _selectionMaterial;
    private bool _inDestroyArea = false;
    public static event Action<float> ChangeSpeed;

    public void Initialize()
    {
        EventSystem.PointerDowned += Downed;
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
            _meshFilter.mesh = secondVersion;
            transform.localScale = Vector3.one * 0.8f;
            _obstacleHealth--;
            if (_obstacleHealth == 0)
            {
                ChangeSpeed?.Invoke(1);
                ObstacleDisabled();
            }
        }
    }

    private void ObstacleDisabled()
    {
        gameObject.SetActive(false);
        EventSystem.PointerDowned -= Downed;
    }

    private void OnTriggerEnter(Collider other)
    {
        ChangeSpeed?.Invoke(2);
        ObstacleDisabled();
    }
}
