using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChefRayCast : MonoBehaviour
{
    private Vector2 rayDirection = Vector2.left;

    private Vector3 rayOffset = new(0, -0.2f, 0);

    public bool allowLeft = false;
    public bool allowRight = false;
    public bool allowDown = false;

    public Sprite leftSprite;
    public Sprite rightSprite;
    public Sprite downSprite;

    private List<Action> turnMethods = new();

    private SpriteRenderer spriteRenderer;

    float timeSinceTurn;
    const float interval = 2.0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (allowLeft) turnMethods.Add(LookLeft);
        if (allowRight) turnMethods.Add(LookRight);
        if (allowDown) turnMethods.Add(LookDown);
    }

    void Update()
    {
        timeSinceTurn += Time.deltaTime;
        if (timeSinceTurn >= interval)
        {
            timeSinceTurn -= interval;
            LookRandom();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + rayOffset, rayDirection);
        if (hit.collider)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Debug.Log("ray hit");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    void LookRandom()
    {
        if (!turnMethods.Any()) return;


        var chosenTurn = turnMethods
            .Select(m => Tuple.Create(UnityEngine.Random.value, m))
            .OrderBy(t => t.Item1)
            .Select(t => t.Item2)
            .First();

        chosenTurn.Invoke();
    }

    void LookLeft()
    {
        spriteRenderer.sprite = leftSprite;
        rayDirection = Vector2.left;
    }

    void LookDown()
    {
        spriteRenderer.sprite = downSprite;
        rayDirection = Vector2.down;
    }

    void LookRight()
    {
        spriteRenderer.sprite = rightSprite;
        rayDirection = Vector2.right;
    }
}
