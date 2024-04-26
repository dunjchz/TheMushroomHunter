using System.Collections;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public AnimationCurve jumpAnimationCurve;
    public AnimationCurve knockbackAnimationCurve;
    private Coroutine jumpCoroutine = null;

    public float jumpDistance = 3;
    public float jumpDuration = 0.5f;

    private Rigidbody2D rb;
    public Animator Controller;
    public SpriteRenderer Renderer;

    private bool canBounce = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumpCoroutine is null)
        {
            Vector2 direction = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            float magnitude = direction.magnitude;
            // Jump if there is any input
            if (magnitude != 0)
            {
                Renderer.flipX = direction.x < 0;
                jumpCoroutine = StartCoroutine(AnimateJump(direction, jumpDuration, jumpDistance, jumpAnimationCurve));
                Controller.SetBool("IsMoving", true);

            }
            else
            {
                Controller.SetBool("IsMoving", false);
            }
        }
    }

    IEnumerator AnimateJump(Vector2 direction, float duration, float distance, AnimationCurve animationCurve)
    {
        canBounce = true;
        float elapsed = 0f;
        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsed / duration);
            float curvePercent = animationCurve.Evaluate(percent);
            rb.velocity = direction.normalized * Mathf.LerpUnclamped(0, distance, curvePercent);

            // Check for collisions with screen edges
            CheckScreenEdgeCollisions();

            yield return null;
        }
        rb.velocity = new Vector2(0, 0);
        jumpCoroutine = null;
        canBounce = false;
    }

    void CheckScreenEdgeCollisions()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Check left edge
        if (screenPosition.x <= 0)
        {
            Vector2 knockbackDirection = Vector2.right;
            StartCoroutine(AnimateKnockback(knockbackDirection));
        }
        // Check right edge
        else if (screenPosition.x >= screenWidth)
        {
            Vector2 knockbackDirection = Vector2.left;
            StartCoroutine(AnimateKnockback(knockbackDirection));
        }
        // Check bottom edge
        if (screenPosition.y <= 0)
        {
            Vector2 knockbackDirection = Vector2.up;
            StartCoroutine(AnimateKnockback(knockbackDirection));
        }
        // Check top edge
        else if (screenPosition.y >= screenHeight)
        {
            Vector2 knockbackDirection = Vector2.down;
            StartCoroutine(AnimateKnockback(knockbackDirection));
        }
    }

    IEnumerator AnimateKnockback(Vector2 direction)
    {
        canBounce = false;
        float duration = jumpDuration / 1.5f;
        float knockbackDistance = jumpDistance / 2;
        float elapsed = 0f;

        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            float percent = Mathf.Clamp01(elapsed / duration);
            rb.velocity = direction.normalized * Mathf.LerpUnclamped(0, knockbackDistance, percent);
            yield return null;
        }
        rb.velocity = Vector2.zero;
        if (jumpCoroutine is not null)
        {
            StopCoroutine(jumpCoroutine);
            jumpCoroutine = null;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("hit");

        if (jumpCoroutine is not null && canBounce)
        {
            StopCoroutine(jumpCoroutine);
            jumpCoroutine = null;
            rb.velocity = new Vector2(0, 0);
            Vector2 collisionPosition = new(
                collision.contacts[0].point.x,
                collision.contacts[0].point.y);
            var direction = rb.position - collisionPosition;
            jumpCoroutine = StartCoroutine(AnimateKnockback(direction));
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var lockScript = other.gameObject.GetComponent<EggSceneController>();
        if (lockScript)
        {
            lockScript.ActivatePanel();
        }

        var dialogScript = other.gameObject.GetComponent<DialogObject>();
        if (dialogScript)
        {
            dialogScript.StartDialog();
        }

        var pickupScript = other.gameObject.GetComponent<Pickup>();
        if (pickupScript)
        {
            pickupScript.TriggerPickup();
        }
    }
}
