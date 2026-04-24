using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] BoxCollider2D left_collider;
    [SerializeField] BoxCollider2D right_collider;
    [SerializeField] BoxCollider2D down_collider;
    [SerializeField] BoxCollider2D up_collider;

    [SerializeField] float thickness;
    float final_pos_x;
    float final_pos_y;
    float final_size_x;
    float final_size_y;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        UpdateColliders();
    }
    void Update()
    {
       
    }


    public void SmoothResize(Vector2 finalPosition, Vector2 finalSize, float duration)
    {
        StartCoroutine(SmoothResizeCoroutine(finalPosition, finalSize, duration));
    }

    IEnumerator SmoothResizeCoroutine(Vector2 finalPosition, Vector2 finalSize, float duration)
    {
        Vector2 startPosition = transform.position;
        Vector2 startSize = sprite.size;

        float time = 0f;

        while (time < duration)
        {
            float t = time / duration;

            //t = Mathf.SmoothStep(0f, 1f, t);
            t = 1 - Mathf.Pow(1 - t, 3);

            transform.position = Vector2.Lerp(startPosition, finalPosition, t);
            sprite.size = Vector2.Lerp(startSize, finalSize, t);

            UpdateColliders();

            time += Time.deltaTime;
            yield return null;
        }

        // final one to avoid floats mistakes
        transform.position = finalPosition;
        sprite.size = finalSize;
        UpdateColliders();
    }

    void UpdateColliders()
    {
        //taking care of colliders to match the size of the box
        left_collider.size = new Vector2(thickness, sprite.size.y);
        left_collider.offset = new Vector2(-sprite.size.x / 2 + thickness / 2, 0);

        right_collider.size = new Vector2(thickness, sprite.size.y);
        right_collider.offset = new Vector2(sprite.size.x / 2 - thickness / 2, 0);

        up_collider.size = new Vector2(sprite.size.x, thickness);
        up_collider.offset = new Vector2(0, sprite.size.y / 2 - thickness / 2);

        down_collider.size = new Vector2(sprite.size.x, thickness);
        down_collider.offset = new Vector2(0, -sprite.size.y / 2 + thickness / 2);
    }
}
