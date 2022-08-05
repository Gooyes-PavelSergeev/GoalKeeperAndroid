using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ThrowableObject
{
    public float fallSpeed = 1f;
    public float rotateSpeedInDegrees = 50f;
    public float pickupDistance = 1f;

    private bool _isDissolving;

    private new void Update()
    {
        base.Update();
        if (!_isDissolving)
        {
            Move();
            CheckCollision();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * fallSpeed);
        transform.Rotate(0, rotateSpeedInDegrees * Time.deltaTime, 0);
    }

    private void CheckCollision()
    {
        var hitColliders = Physics.OverlapSphere(transform.position, pickupDistance);
        foreach (var collider in hitColliders)
        {
            if (collider.gameObject.tag == "Player")
            {
                _isDissolving = true;
                Score.instance.AddCoin(objectValue);
                ShowFloatingText(objectValue, floatingTextColor);
                StartDissolve();
            }
        }
    }
}
