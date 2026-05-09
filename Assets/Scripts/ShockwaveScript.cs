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

    private bool canDamage = true;
    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<SpriteRenderer>().material;
        mat.SetFloat("_Radius", 0);
        mat.SetFloat("_Thickness", thickness);

        player = MainManagerScript.Instance.player;

        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        float r = mat.GetFloat("_Radius");
        r += speed * Time.deltaTime;

        if (r > maxRadius)
            r = minRadius;

        mat.SetFloat("_Radius", r);


        if(IsPlayerHit(player.transform.position, transform.position) && canDamage)
        {
            MainManagerScript.Instance.player.GetComponent<HealthScript>().TakeDamage(attack);
            canDamage = false;
        }
    }

    bool IsPlayerHit(Vector2 playerPos, Vector2 center)
    {
        Vector2 p = transform.InverseTransformPoint(playerPos);

        float dist = p.magnitude;

        float radius = mat.GetFloat("_Radius");
        float thickness = mat.GetFloat("_Thickness");

        // pierścień
        bool inRing = Mathf.Abs(dist - radius) < thickness;

        // półokrąg
        bool inHalf = p.y > 0;

        // kąt
        float angle = Mathf.Atan2(p.y, p.x);

        float holeStart = mat.GetFloat("_HoleStart");
        float holeEnd = mat.GetFloat("_HoleEnd");

        bool inHole = angle > holeStart && angle < holeEnd;
        if(inRing && inHalf && !inHole)
        {
            Debug.Log("Player is hit");
        }

        return inRing && inHalf && !inHole;
    }
}
