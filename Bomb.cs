using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bomb : ThrowableObject
{
    private bool _isHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !_isHit)
        {
            _isHit = true;
            StartDissolve();
            Score.instance.ModifyScore(objectValue);
            ShowFloatingText(objectValue, floatingTextColor);
        }
    }
}
