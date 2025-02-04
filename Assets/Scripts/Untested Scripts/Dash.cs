using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 15f;
    [SerializeField] private float dashDuration = 0.2f;
    [SerializeField] private float dashCooldown = 1f;
    private Rigidbody2D body;
    private PlayerMovement playerMovement;
    private Animator anim;
    private bool isDashing;
    private float cooldownTimer;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && cooldownTimer <= 0 && !isDashing)
        {
            StartCoroutine(PerformDash());
        }
        cooldownTimer -= Time.deltaTime;
    }

    private IEnumerator PerformDash()
    {
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0;
        body.linearVelocity = new Vector2(transform.localScale.x * dashSpeed, 0);
        anim.SetBool("run", true);
        yield return new WaitForSeconds(dashDuration);
        body.gravityScale = originalGravity;
        isDashing = false;
        cooldownTimer = dashCooldown;
    }
}