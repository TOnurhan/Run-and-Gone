using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using System;

public class GameController : MonoBehaviour
{
    [SerializeField] private CameraMovement _camera;
    [SerializeField] private PlayerBehaviour _playerBehaviour;
    [SerializeField] private PointCount _pointCounter;
    [SerializeField] private Text _pointText;
    [SerializeField] private Text _touchToStartText;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private List<Transform> obstaclePoints;
    [SerializeField] private Button _skinMenuButton;
    [SerializeField] private Button _stageMenuButton;
    [SerializeField] private Button _returnButton;
    [SerializeField] private Image _menuBackground;
    [SerializeField] private GameObject _skinMenuTab;
    [SerializeField] private GameObject _stageMenuTab;

    [SerializeField] private float _waitTime;
    private float _totalPoint;
    private float _point;
    public bool gameStarted;

    public event Action GameStarted;

    void Initialize()
    {
        _playerBehaviour.HitTheWall += Finished;
        _pointCounter.PointCollected += UpdatePointCounter;
        EventSystem.PointerDowned += LevelStart;
    }

    private void Awake()
    {
        Initialize();
        GetLevel();
        LeanTween.alphaText(_touchToStartText.rectTransform, 0f, 1f).setEaseInCubic().setLoopPingPong();
    }

    private void UpdatePointCounter()
    {
        _point++;
        _pointText.text = "Point : " + _point;
    }

    private void GetLevel()
    {
        for(int i = 0; i < obstaclePoints.Count; i++)
        {
            _objectPool.SpawnPooledObject(obstaclePoints[i].position);
        }
    }

    private void LevelStart(PointerEventData pointerEventData)
    {
        _touchToStartText.gameObject.SetActive(false);
        _stageMenuButton.gameObject.SetActive(false);
        _skinMenuButton.gameObject.SetActive(false);
        gameStarted = true;
        GameStarted?.Invoke();

        EventSystem.PointerDowned -= LevelStart;
    }

    void Finished()
    {
        StartCoroutine(Finish());
    }

    IEnumerator Finish()
    {
        _pointText.gameObject.SetActive(true);
        gameStarted = false;
        yield return new WaitForSeconds(_waitTime);
        _totalPoint += _point;
        _menuBackground.gameObject.SetActive(true);
        Debug.Log("End Menu");
    }

    public void Menu(int menuIndex)
    {
        _touchToStartText.gameObject.SetActive(false);
        _stageMenuButton.gameObject.SetActive(false);
        _skinMenuButton.gameObject.SetActive(false);
        _menuBackground.gameObject.SetActive(true);
        _returnButton.gameObject.SetActive(true);
        if (menuIndex == 0)
        {
            _skinMenuTab.SetActive(true);
        }
        else if (menuIndex == 1)
        {
            _stageMenuTab.SetActive(true);
        }
        EventSystem.PointerDowned -= LevelStart;
    }

    public void BackToMenu()
    {
        _touchToStartText.gameObject.SetActive(true);
        _skinMenuTab.SetActive(false);
        _stageMenuTab.SetActive(false);
        _returnButton.gameObject.SetActive(false);
        _menuBackground.gameObject.SetActive(false);
        _stageMenuButton.gameObject.SetActive(true);
        _skinMenuButton.gameObject.SetActive(true);
        EventSystem.PointerDowned += LevelStart;
    }
}

//Rigidbody, gravity script ile değiş.???
//Kamera yer değiştikten sonra menü.
//Toplam puan
//Timer for point section.
//Curved movement