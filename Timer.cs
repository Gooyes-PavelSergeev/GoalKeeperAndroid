using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshProUGUI _timerText;

    private float _timeLeft;
    private float _timeFull;
    private bool _timerOn;

    private Slider _slider;

    private void Start()
    {
        _slider = gameObject.GetComponent<Slider>();
        _timeLeft = Game.GetGameTime();
        _timeFull = _timeLeft;
        _timerOn = true;
    }

    private void Update()
    {
        if (_timerOn)
        {
            if (_timeLeft > 0)
            {
                _timeLeft -= Time.deltaTime;
                updateTimer(_timeLeft);
            }
            else
            {
                Debug.Log("Time is UP!");
                _timeLeft = 0;
                _timerOn = false;
            }
        }

        _slider.value = (_timeFull - _timeLeft) / _timeFull;
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        _timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
