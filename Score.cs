using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    private int _currentScore;
    private float _coinAmount;

    public static Score instance;

    [SerializeField]
    private ProgressBar _progressBar;

    [SerializeField]
    private CoinUI _coinUI;

    public int targetScore;

    private void Awake()
    {
        instance = this;
        _coinAmount = 0;
        _currentScore = 0;
        _progressBar.TargetScore = targetScore;
    }

    public void ModifyScore(int weight)
    {
        var newScoreValue = _currentScore + weight;
        if (newScoreValue > 0)
        {
            if (newScoreValue <= 100)
            {
                _currentScore += weight;
            }
            else
            {
                _currentScore = 100;
            }
        }
        else
        {
            _currentScore = 0;
        }
        _progressBar.IncrementProgress(_currentScore);
    }

    public void AddCoin(float coinValue)
    {

        _coinAmount += coinValue;
        _coinUI.IncrementCoinAmount((int)coinValue);
    }
}
