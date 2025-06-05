using System;
using UnityEngine;
using UnityEngine.UI;

public class EntityUIHealthBar : MonoBehaviour
{
    [SerializeField] private Image image;

    public Action<float> onHealthChanged;

    private void OnEnable()
    {
        onHealthChanged += OnHealthChanged;    
    }
    private void OnDisable()
    {
        onHealthChanged -= OnHealthChanged;
    }
    private void LateUpdate()
    {
        var cam = Camera.main;
        if (cam == null) return;

        transform.LookAt(transform.position + cam.transform.rotation * Vector3.back,
                         cam.transform.rotation * Vector3.up);
    }
    private void OnHealthChanged(float value)
    {
        image.fillAmount = value;
    }
}
