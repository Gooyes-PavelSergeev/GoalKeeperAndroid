using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour
{
    public float lifeTime = 1f;
    public float fadeTime = 1f;
    public float scaleIncreaseSpeed = 1f;
    public float targetScale = 1f;
    public float startingScale = 0.2f;

    private float _currentScale;

    private TextMesh _text;
    private Color _color;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
        _text = gameObject.GetComponent<TextMesh>();
        _color = _text.color;
    }

    private void Update()
    {
        _color.a -= fadeTime * Time.deltaTime;
        _text.color = _color;
    }
}
