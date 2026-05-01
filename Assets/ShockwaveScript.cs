using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockwaveScript : MonoBehaviour
{
    public Material mat;
    public float speed = 1f;
    public float thickness = 0.1f;
    public float maxRadius = 5f;
    public float minRadius = 0f;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        mat.SetFloat("_HoleStart", 0.2f);
        mat.SetFloat("_HoleEnd", 1f);
        mat.SetFloat("_Radius", 0);
        mat.SetFloat("_Thickness", thickness);
    }

    void Update()
    {
        float r = mat.GetFloat("_Radius");
        r += speed * Time.deltaTime;

        if (r > maxRadius)
            r = minRadius;

        mat.SetFloat("_Radius", r);

        IsPlayerHit(player.transform.position, transform.position);
        Debug.DrawLine(transform.position, player.transform.position, Color.red);
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
        // if (inHole)
        // {
        //     Debug.Log("Player is in hole");
        // }
        // if(inHalf)
        // {
        //     Debug.Log("Player is in half");
        // }
        if(inRing && inHalf && !inHole)
        {
            Debug.Log("Player is hit");
        }

        return inRing && inHalf && !inHole;
    }
}
