using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatAttacksScript : MonoBehaviour
{
    [SerializeField] public GameObject warningZonePrefab;

    [Header("First Attack")]
    [SerializeField] public GameObject SpiningGameObject; //virus will have a toxic effect on the player, making them take damage over time (player turning green)
    [SerializeField] float spinningSpeed;
    [SerializeField] int spins;
    private bool coroutineRunning = false;
    
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(SpinningAttack());
        }
    }

    IEnumerator SpinCoroutine(float speed, int spins)
    {
        coroutineRunning = true;
        Vector2 spawn_location = MainManagerScript.Instance.box.transform.position;
        GameObject spinningObject =
            Instantiate(SpiningGameObject, spawn_location, Quaternion.identity);

        float rotated = 0f;
        float targetRotation = spins * 360f;

        if(speed > 0)
        {
            while (rotated < targetRotation)
            {
                float step = speed * Time.deltaTime;

                spinningObject.transform.Rotate(0, 0, step);

                rotated += step;

                yield return null; // czekaj do następnej klatki
            }
        }
        else if (speed < 0)
        {
            targetRotation = -targetRotation;
             while (rotated > targetRotation)
            {
                float step = speed * Time.deltaTime;

                spinningObject.transform.Rotate(0, 0, step);

                rotated += step;

                yield return null; // czekaj do następnej klatki
            }
        }
        coroutineRunning = false;
        Destroy(spinningObject);
    }

    void ChangeSpinDirection()
    {
        spinningSpeed = -spinningSpeed;
    }

    IEnumerator SpinningAttack()
    {
        StartCoroutine(SpinCoroutine(spinningSpeed, 1));
        yield return new WaitUntil(() => !coroutineRunning);
        ChangeSpinDirection();
        StartCoroutine(SpinCoroutine(spinningSpeed, 1));
    }
}
