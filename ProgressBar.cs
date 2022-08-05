using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider _slider;
    private float _currentValue = 0f;
    private float _incomingValue;
    private float _visualDifference;

    public float fillSpeed;

    public int TargetScore { get; set; }

    public GameObject _targetMark;

    private void Awake()
    {
        _slider = gameObject.GetComponent<Slider>();
        Vector3 markPosition = new Vector3 (TargetScore * 1.5f - 75, 0, 0);
        _targetMark.transform.localPosition = markPosition;
    }

    public void IncrementProgress(float currentValue)
    {
        currentValue /= 100;
        _incomingValue = currentValue - _currentValue;
        _currentValue = currentValue;
        if (Mathf.Abs(_incomingValue) < 0.0001) _incomingValue = 0;
    }

    private void Update()
    {
        if (_incomingValue == 0)
        {
            return;
        }
        if (_incomingValue > 0)
        {
            if (_slider.value < _currentValue)
            {
                _slider.value += fillSpeed * Time.deltaTime;
            }
        }
        else if (_incomingValue < 0)
        {
            if (_slider.value > _currentValue)
            {
                _slider.value -= fillSpeed * Time.deltaTime;
            }
        }
    }
}
