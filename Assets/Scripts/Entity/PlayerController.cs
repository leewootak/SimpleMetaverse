using UnityEngine;

public class PlayerController : BaseController
{
    protected override void Start()
    {
        base.Start();
    }

    // ¿Ãµø
    protected override void Action()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(horizontal, vertical).normalized;
    }
}
