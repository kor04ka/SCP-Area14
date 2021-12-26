using UnityEngine;

public class WeaponSway : WeaponScriptBase
{
    [SerializeField] private float _intensity;
    [SerializeField] private float _smooth;

    private Quaternion _originRotation;

    private void Awake()
    {
        _originRotation = transform.localRotation;
    }

    private void Update()
    {
        if (_inventoryEnablerDisabler.IsActivated) { return; } 

        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        Quaternion xAngle = Quaternion.AngleAxis(-_intensity * mouseX, Vector3.up);
        Quaternion yAngle = Quaternion.AngleAxis(_intensity * mouseY, Vector3.right);
        Quaternion targetRotation = _originRotation * xAngle * yAngle;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _smooth);
    }
}