using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class DominoController : MonoBehaviour
{
    [SerializeField]
    private float m_moveSpeed = 0.0f;
    [SerializeField]
    private float m_turnSpeed = 0.0f;

    private Rigidbody m_rigidbody = null;


    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    [SerializeField]
    private bool m_controlled = true;

    private void Update()
    {
        if (m_controlled)
        {
            ControlObject();
        }
    }

    private void ControlObject()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            Duplication();
        }

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            moveDir += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            moveDir += Vector3.back;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            moveDir += Vector3.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveDir += Vector3.left;
        }

        if (moveDir.sqrMagnitude > Mathf.Epsilon)
        {
            moveDir = moveDir.normalized;
            Turn(moveDir);
            Move(moveDir);
        }
    }

    private void Move(Vector3 i_forward)
    {
        Vector3 delta = i_forward * m_moveSpeed * Time.deltaTime;
        Vector3 targetPos = transform.position + delta;

        m_rigidbody.MovePosition(targetPos);
    }

    private void Turn(Vector3 i_forward)
    {
        Quaternion toRot = Quaternion.LookRotation(i_forward);
        Quaternion fromRot = transform.rotation;

        float delta = m_turnSpeed * Time.deltaTime;
        Quaternion targetRot = Quaternion.RotateTowards(fromRot, toRot, delta);

        m_rigidbody.MoveRotation(targetRot);
    }

    [SerializeField, LayerTypeField]
    private int m_dominoLayer = 0;

    private void Duplication()
    {
        var copiedDomino = Instantiate(this, transform.position, transform.rotation);
        SetDominoParameter(copiedDomino);
    }

    private void SetDominoParameter(DominoController i_domino)
    {
        i_domino.m_controlled = false;
        i_domino.gameObject.layer = m_dominoLayer;
    }
} // class DominoController