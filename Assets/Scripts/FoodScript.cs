using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodScript : MonoBehaviour
{   
    GameObject player;
    Vector3 direction;

    [SerializeField] float speed;
    [SerializeField] float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        direction = player.transform.position - transform.position;

        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + direction.normalized * speed * Time.deltaTime;
    }


}
