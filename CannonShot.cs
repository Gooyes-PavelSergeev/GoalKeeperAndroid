using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShot : MonoBehaviour
{
    [SerializeField]
    private Transform _objectSpawnPoint;

    [SerializeField]
    private Transform _gate;

    public float shootSpeed = 1;
    public float shootForce = 5;
    private float _shootTime = 0;

    private float _radianRangeHor;
    private float _radianRangeVer;

    private void Start()
    {
        gameObject.transform.LookAt(_gate);
        _radianRangeHor = CalculateAimHor();
        _radianRangeVer = CalculateAimVer();
    }

    private void Update()
    {
        _shootTime += Time.deltaTime;
        if (_shootTime > shootSpeed)
        {
            Throw();
            _shootTime = 0;
        }
    }

    float CalculateAimHor()
    {
        var horGateSize = _gate.localScale.x;

        var gatePoint = _gate.transform.position;
        var gatePointZ = gatePoint.z;
        var gatePointY = gatePoint.y;
        var gatePointX = gatePoint.x;

        Vector3 gateRightBorder = new Vector3(gatePointX + horGateSize / 2, gameObject.transform.position.y, gatePointZ);
        Vector3 gateLeftBorder = new Vector3(gatePointX - horGateSize / 2, gameObject.transform.position.y, gatePointZ);

        float distanceRight = Vector3.Distance(gameObject.transform.position, gateRightBorder);
        float distanceLeft = Vector3.Distance(gameObject.transform.position, gateLeftBorder);

        var cosA = (distanceRight * distanceRight + distanceLeft * distanceLeft - horGateSize * horGateSize) / (2 * distanceRight * distanceLeft);

        return Mathf.Acos(cosA);
    }

    float CalculateAimVer()
    {
        var verGateSize = _gate.localScale.y;

        var gatePoint = _gate.transform.position;
        var gatePointZ = gatePoint.z;
        var gatePointY = gatePoint.y;
        var gatePointX = gatePoint.x;

        Vector3 gateDownBorder = new Vector3(gatePointX, gatePointY - verGateSize / 2, gatePointZ);
        Vector3 gateUpBorder = new Vector3(gatePointX, gatePointY + verGateSize / 2, gatePointZ);

        float distanceDown = Vector3.Distance(gameObject.transform.position, gateDownBorder);
        float distanceUp = Vector3.Distance(gameObject.transform.position, gateUpBorder);

        var cosB = (distanceUp * distanceUp + distanceDown * distanceDown - verGateSize * verGateSize) / (2 * distanceUp * distanceDown);

        return Mathf.Acos(cosB);
    }

    void Throw()
    {
        Quaternion throwRotation = _objectSpawnPoint.rotation;
        throwRotation.x += Random.Range(-1 * _radianRangeHor, _radianRangeHor);
        throwRotation.y += Random.Range(-1 * _radianRangeVer / 2, _radianRangeVer / 2);
        var objectToThrow = Game.PrepareSpawnObject();
        var thrownObject = Instantiate(objectToThrow, _objectSpawnPoint.position, throwRotation);
        thrownObject.GetComponent<Rigidbody>().velocity = thrownObject.transform.forward * shootForce;
    }
}
