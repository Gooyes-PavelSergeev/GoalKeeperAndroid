using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float ballSpawnChance = 80f;
    private float bombSpawnChance;

    public float maxCoinSpawnTime = 3f;
    public float minCoinSpawnTime = 1f;
    private float _coinSpawnSpeed;
    private float _coinSpawnTime = 0f;

    public float coinSpawnHeight = 2.5f;
    public float coinSpawnWidth = 2f;

    public int gameLength = 60;

    [SerializeField]
    private ThrowableObject _ball, _bomb;

    [SerializeField]
    private Coin _coin;

    [SerializeField]
    private Score _score;

    [SerializeField]
    private Transform _playerTransform;

    public static Game instance;

    private void Awake()
    {
        _coinSpawnSpeed = GetRandomTime();
        bombSpawnChance = 100 - ballSpawnChance;
        instance = this;
    }

    private void Update()
    {
        _coinSpawnTime += Time.deltaTime;
        if (_coinSpawnTime > _coinSpawnSpeed)
        {
            SpawnCoin();
            _coinSpawnTime = 0;
        }
    }

    private float GetRandomTime()
    {
        var time = Random.Range(minCoinSpawnTime, maxCoinSpawnTime);
        return time;
    }

    private void SpawnCoin()
    {
        var coinSpawnPosition = _playerTransform.position;
        coinSpawnPosition.y += coinSpawnHeight;
        coinSpawnPosition.x += Random.Range(-1 * coinSpawnWidth, coinSpawnWidth);
        var coinSpawnRotation = Quaternion.Euler(0, 0, 0);

        Instantiate(_coin, coinSpawnPosition, coinSpawnRotation);
    }

    public static ThrowableObject PrepareSpawnObject()
    {
        ThrowableObjectType objectType = instance.ChooseType();
        ThrowableObject obj = instance.GetObject(objectType);
        return obj;
    }

    public ThrowableObjectType ChooseType()
    {
        var pickedNum = Random.Range(0, 100);
        if (pickedNum <= ballSpawnChance)
            return ThrowableObjectType.Ball;
        return ThrowableObjectType.Bomb;
    }
    private ThrowableObject GetObject(ThrowableObjectType type)
    {
        switch (type)
        {
            case ThrowableObjectType.Ball:
                return _ball;
            case ThrowableObjectType.Bomb:
                return _bomb;
        }
        Debug.LogError($"No object for {type}");
        return _ball;
    }

    public static int GetGameTime()
    {
        return instance.gameLength;
    }
}
