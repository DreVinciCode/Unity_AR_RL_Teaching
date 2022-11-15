using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TargetController : MonoBehaviour
{

    [SerializeField] private float _moveSpeed = 1f;

    public InputAction PlayerActions;


    private void OnEnable()
    {
        PlayerActions.Enable();
    }

    private void OnDisable()
    {
        PlayerActions.Disable();
    }

    private void Update()
    {
        var moveY = PlayerActions.ReadValue<Vector3>().y;
        var moveX = PlayerActions.ReadValue<Vector3>().x;
        var moveZ = PlayerActions.ReadValue<Vector3>().z;

        transform.localPosition += new Vector3(moveX, moveY, moveZ) * Time.deltaTime * _moveSpeed;

    }
}
