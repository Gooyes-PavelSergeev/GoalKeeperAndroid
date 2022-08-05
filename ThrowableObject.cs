using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject _floatingTextPrefab;

    [SerializeField]
    private Renderer _bodyRenderer = new Renderer();

    private float _targetDissolveValue = 1f;
    protected float _currentDissolveValue = -1f;
    private bool _dissolveStarted;

    private static ThrowableObject _throwableObject;

    public float dissolveSpeed = 2f;
    public float lifeTime = 3f;
    public int objectValue = 20;
    public Color floatingTextColor = Color.yellow;

    void Awake()
    {
        _throwableObject = this;
        var bodyMats = _bodyRenderer.sharedMaterials;
        for (int i = 0; i < bodyMats.Length; i++)
        {
            bodyMats[i] = new Material(bodyMats[i]);
        }
        _bodyRenderer.sharedMaterials = bodyMats;

        Destroy(gameObject, lifeTime);
    }

    protected void OnTriggerEnter(Collider triggerCollider)
    {
        StartDissolve();
    }

    protected void Update()
    {
        if (_dissolveStarted)
        {
            _currentDissolveValue = Mathf.Lerp(_currentDissolveValue, _targetDissolveValue, dissolveSpeed * Time.deltaTime);
            foreach (var material in _bodyRenderer.sharedMaterials)
            {
                material.SetFloat("_Starting", _currentDissolveValue);
            }
            if (_currentDissolveValue >= 0.7)
            {
                Destroy(gameObject);
            }
        }
    }

    protected void StartDissolve()
    {
        _dissolveStarted = true;
        Destroy(gameObject.GetComponent<Rigidbody>());
        Destroy(gameObject.GetComponent<Collider>());
    }

    protected void ShowFloatingText(float value, Color color)
    {
        var textItem = Instantiate(_floatingTextPrefab, transform.position, Quaternion.identity);
        Vector3 normalPosition = textItem.transform.position;
        normalPosition.y += 0.5f;
        textItem.transform.position = normalPosition;

        var text = textItem.GetComponent<FloatingText>().GetComponent<TextMesh>();
        text.text = value.ToString();
        text.color = color;
    }

    public static string GetText()
    {
        return _throwableObject.objectValue.ToString();
    }

    public static Color GetTextColor()
    {
        return _throwableObject.floatingTextColor;
    }
}
