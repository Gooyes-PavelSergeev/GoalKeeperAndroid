using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : ThrowableObject
{
    public int ballHitValue = 10;
    public Color ballHitColor = Color.green;

    private bool _isHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && !_isHit)
        {
            _isHit = true;
            Score.instance.ModifyScore(ballHitValue);
            ShowFloatingText(ballHitValue, ballHitColor);
        }
    }

    private new void OnTriggerEnter(Collider triggerCollider)
    {
        Score.instance.ModifyScore(objectValue);
        ShowFloatingText(objectValue, floatingTextColor);
        base.OnTriggerEnter(triggerCollider);
    }
}
