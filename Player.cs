using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxDistance = 100.0f;

    public float spring = 50.0f;
    public float damper = 5.0f;
    public float drag = 10.0f;
    public float angularDrag = 5.0f;
    public float distance = 0.01f;
    public bool attachToCenterOfMass = false;

    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private GameObject _bodyRoot;

    private SpringJoint _springJoint;
    private float _ZCoord;
    private Vector3 _originPosition;

    private void Start()
    {
        _originPosition = _bodyRoot.transform.position;
        _ZCoord = _originPosition.z;
    }

    void Drag()
    {
        RaycastHit hit;
        if (!Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, maxDistance))
            return;
        if (!hit.rigidbody || hit.rigidbody.isKinematic)
            return;

        if (!_springJoint)
        {
            GameObject go = new GameObject("Rigidbody dragger");
            Rigidbody body = go.AddComponent<Rigidbody>();
            body.isKinematic = true;
            _springJoint = go.AddComponent<SpringJoint>();
        }

        _springJoint.transform.position = hit.point;
        if (attachToCenterOfMass)
        {
            Vector3 anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
            anchor = _springJoint.transform.InverseTransformPoint(anchor);
            _springJoint.anchor = anchor;
        }
        else
        {
            _springJoint.anchor = Vector3.zero;
        }

        _springJoint.spring = spring;
        _springJoint.damper = damper;
        _springJoint.maxDistance = distance;
        _springJoint.connectedBody = hit.rigidbody;

        StartCoroutine(DragObject(hit.distance));
    }

    IEnumerator DragObject(float distance)
    {
        float oldDrag = _springJoint.connectedBody.drag;
        float oldAngularDrag = _springJoint.connectedBody.angularDrag;
        _springJoint.connectedBody.drag = this.drag;
        _springJoint.connectedBody.angularDrag = this.angularDrag;

        while (Input.GetMouseButton(0) && CheckRestrictionArea())
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            Vector3 pointToDrag = ray.GetPoint(distance);
            pointToDrag.z = _ZCoord;
            _springJoint.transform.position = pointToDrag;
            yield return null;
        }

        if (_springJoint.connectedBody)
        {
            _springJoint.connectedBody.drag = oldDrag;
            _springJoint.connectedBody.angularDrag = oldAngularDrag;
            _springJoint.connectedBody = null;
        }
    }

    private bool CheckRestrictionArea()
    {
        if (Math.Abs(_bodyRoot.transform.position.x) > 2)
            return false;
        if (_bodyRoot.transform.position.y > 4 || _bodyRoot.transform.position.y < 0)
            return false;
        return true;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            Drag();
    } 
}
