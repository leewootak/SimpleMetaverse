using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    Rigidbody2D rb;
    float h;
    float v;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();            
    }

    void Start()
    {
        
    }

    void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(h, v).normalized * moveSpeed;
    }

    // ¿Ãµø
    private void Movement()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");
    }
}
