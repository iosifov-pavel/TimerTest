using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class LongPressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Button _button;

    public Button Button => _button;

    private Action<float> _callback;
    private bool _isPressed;
    private float _increaseScale;
    private float _timeAmount;

    public void SetCallback( Action<float> callback, bool increaseValue )
    {
        _callback = callback;
        _increaseScale = increaseValue ? 1 : -1;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _timeAmount = Constants.LongPressButtonInitialTime;
        _isPressed = true;
        StartCoroutine(ActionRoutine());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isPressed = false;
    }

    private IEnumerator ActionRoutine()
    {
        var timer = 0f;
        _callback?.Invoke(_increaseScale);
        while (_isPressed)
        {
            timer += Time.deltaTime;
            if( timer >= _timeAmount)
            {
                _callback?.Invoke(_increaseScale);
                _timeAmount = Mathf.Max( Constants.LongPressButtonMinAwaitTime, _timeAmount * Constants.LongPressButtonDecreaseScale);
                timer = 0;
            }
            yield return null;
        }
    }
}
