using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Static characterStatic;
    public float velocidadMovimiento = 5.0f;
    public float velocidadRotacion = 200.0f;
    private Animator anim;
    public float x, y;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        // Calcula la nueva orientación y posición
        characterStatic.orientation += x * Time.deltaTime * velocidadRotacion; 
        characterStatic.position += transform.forward * y * Time.deltaTime * velocidadMovimiento;

        // Actualiza los parámetros del animador
        anim.SetFloat("Velx", x);
        anim.SetFloat("Vely", y);
    }
}
