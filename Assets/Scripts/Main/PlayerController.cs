using UnityEngine;
using UnityEngine.SceneManagement;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NPC"))
        {
            SceneManager.LoadScene("MiniGameScene");
        }
    }
}
