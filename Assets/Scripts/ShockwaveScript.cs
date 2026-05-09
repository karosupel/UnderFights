using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour
{
    public Material mat;
    public float speed;
    public float thickness;
    public float maxRadius;
    public float minRadius;

    public float attack;

    public float lifeTime = 5f;
    private GameObject player;
    private Collider2D playerCollider;

    private bool canDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        mat.SetFloat("_Radius", 0);
        mat.SetFloat("_Thickness", thickness);

        player = MainManagerScript.Instance.player;
        playerCollider = player.GetComponent<BoxCollider2D>();

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        float r = mat.GetFloat("_Radius");
        r += speed * Time.deltaTime;

        if (r > maxRadius)
            r = minRadius;

        mat.SetFloat("_Radius", r);


        if(IsPlayerHit() && canDamage)
        {
            MainManagerScript.Instance.player.GetComponent<HealthScript>().TakeDamage(attack);
            canDamage = false;
        }
    }

    bool IsPlayerHit()
    {
        Bounds bounds = playerCollider.bounds;

        // punkty hitboxa w world space
        Vector2[] points =
        {
            bounds.center,

            new Vector2(bounds.min.x, bounds.center.y),
            new Vector2(bounds.max.x, bounds.center.y),

            new Vector2(bounds.center.x, bounds.min.y),
            new Vector2(bounds.center.x, bounds.max.y),

            bounds.min,
            bounds.max,

            new Vector2(bounds.min.x, bounds.max.y),
            new Vector2(bounds.max.x, bounds.min.y)
        };

        float radius = mat.GetFloat("_Radius");
        float thickness = mat.GetFloat("_Thickness");

        float holeStart = mat.GetFloat("_HoleStart");
        float holeEnd = mat.GetFloat("_HoleEnd");

        foreach (Vector2 point in points)
        {
            Vector2 p = transform.InverseTransformPoint(point);

            float dist = p.magnitude;

            bool inRing = Mathf.Abs(dist - radius) < thickness;
            bool inHalf = p.y > 0;

            float angle = Mathf.Atan2(p.y, p.x);

            bool inHole = angle > holeStart && angle < holeEnd;

            if (inRing && inHalf && !inHole)
                return true;
        }

        return false;
    }
}
