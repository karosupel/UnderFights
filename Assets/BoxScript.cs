using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField] SpriteRenderer sprite;
    [SerializeField] BoxCollider2D left_collider;
    [SerializeField] BoxCollider2D right_collider;
    [SerializeField] BoxCollider2D down_collider;
    [SerializeField] BoxCollider2D up_collider;

    [SerializeField] float thickness;

    void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        //left_collider.offset = new Vector2(left_collider.offset.x + sprite.size.x/2, left_collider.offset.y + sprite.size.y/2);
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
