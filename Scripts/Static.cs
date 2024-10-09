using UnityEngine;
using System;

// Clase Static que maneja la posición y orientación básica
public class Static : MonoBehaviour
{
    // Variables internas de posición y orientación
    public Vector3 _position;
    public float _orientation;

    // Propiedad pública para la posición con un evento asociado
    public Vector3 position
    {
        get => _position;
        set
        {
            _position = value;
            OnPositionChanged?.Invoke(_position);
        }
    }

    // Propiedad pública para la orientación con un evento asociado
    public float orientation
    {
        get => _orientation;
        set
        {
            _orientation = value;
            OnOrientationChanged?.Invoke(_orientation);
        }
    }

    // Eventos para detectar cambios en la posición y orientación
    public event Action<Vector3> OnPositionChanged;
    public event Action<float> OnOrientationChanged;

    // Inicialización de las variables y suscriptores a eventos
    protected virtual void Start()
    {
        _position = transform.position;
        _orientation = transform.eulerAngles.y;

        // Suscripción a eventos para actualizar la posición y orientación
        OnPositionChanged += UpdateTransformPosition;
        OnOrientationChanged += UpdateTransformOrientation;
    }

    // Actualización cada frame para verificar cambios en el transform de Unity
    protected virtual void Update()
    {
        if (transform.position != _position)
        {
            position = transform.position;
        }

        if (Mathf.Abs(transform.eulerAngles.y - _orientation) > 0.01f)
        {
            orientation = transform.eulerAngles.y;
        }
    }

    // Método para actualizar la posición del transform
    private void UpdateTransformPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    // Método para actualizar la orientación del transform
    private void UpdateTransformOrientation(float newOrientation)
    {
        transform.eulerAngles = new Vector3(0, newOrientation, 0);
    }
}