using UnityEngine;

public class MovementKinematic : MonoBehaviour
{
    public Kinematic characterKinematic;  
    public float velocidadMovimiento;  
    public float velocidadRotacion;  

    private Animator anim;  
    private float x;        
    private float y;        

    void Start()
    {
        velocidadMovimiento = 5.0f;  
        velocidadRotacion = 200.0f;  
        anim = GetComponent<Animator>();  
    }

    void Update()
    {
        x = Input.GetAxis("Horizontal"); 
        y = Input.GetAxis("Vertical");   

        characterKinematic.orientation += x * Time.deltaTime * velocidadRotacion;

        Vector3 direction = new Vector3(Mathf.Sin(characterKinematic.orientation * Mathf.Deg2Rad), 0, Mathf.Cos(characterKinematic.orientation * Mathf.Deg2Rad));

        characterKinematic.position += direction * y * Time.deltaTime * velocidadMovimiento;

        characterKinematic.velocity = direction * y * velocidadMovimiento;

        characterKinematic.rotation = x * velocidadRotacion;

        anim.SetFloat("Velx", x);
        anim.SetFloat("Vely", y);
    }
}