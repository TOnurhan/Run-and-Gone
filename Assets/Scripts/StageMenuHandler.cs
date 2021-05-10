using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StageMenuHandler : MonoBehaviour
{
    private float _firstMousePosX, _lastMousePosX;
    private bool _dragOnce;
    public int stageIndex;
    public List<Image> stageImages;
    public List<RectTransform> imagePoints;

    private void OnEnable()
    {
        EventSystem.PointerDowned += OnPointerDown;
        EventSystem.PointerDragged += ChangeChapter;
    }
    private void OnDisable()
    {
        EventSystem.PointerDowned -= OnPointerDown;
        EventSystem.PointerDragged -= ChangeChapter;
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _firstMousePosX = pointerEventData.position.x;
        _dragOnce = true;
    }

    void ChangeChapter(PointerEventData pointerEventData)
    {
        _lastMousePosX = pointerEventData.position.x;

        if (_lastMousePosX - _firstMousePosX > 100f && _dragOnce)
        {
            if(stageIndex == 0)
            {
                return;
            }
            _dragOnce = false;
            Debug.Log("Önceki");


            stageImages[stageIndex - 1].rectTransform.position = imagePoints[0].position;
            stageImages[stageIndex - 1].gameObject.SetActive(true);
            LeanTween.moveX(stageImages[stageIndex - 1].rectTransform, imagePoints[1].position.x, 0.5f);
            LeanTween.moveX(stageImages[stageIndex].rectTransform, imagePoints[2].localPosition.x, 0.5f).setOnComplete(() => 
            { 
                stageImages[stageIndex].gameObject.SetActive(false);
                stageIndex--;
            });

        }

        else if (_lastMousePosX - _firstMousePosX < -100f && _dragOnce)
        {
            if (stageIndex == stageImages.Count-1)
            {
                return;
            }
            _dragOnce = false;

            stageImages[stageIndex + 1].rectTransform.position = imagePoints[2].position;
            stageImages[stageIndex + 1].gameObject.SetActive(true);
            LeanTween.moveX(stageImages[stageIndex + 1].rectTransform, imagePoints[1].position.x, 0.5f);
            LeanTween.moveX(stageImages[stageIndex].rectTransform, imagePoints[0].localPosition.x, 0.5f).setOnComplete(() =>
            {
                Debug.Log("An");
                stageImages[stageIndex].gameObject.SetActive(false);
                stageIndex++;
            });
            Debug.Log("Sonra");


        }
    }
}
