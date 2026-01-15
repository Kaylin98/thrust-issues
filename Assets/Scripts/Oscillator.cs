using System;
using Unity.Mathematics;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed = 2f;
    Vector3 startingPos;
    Vector3 endPos;
    float movementFactor; // 0 for not moved, 1 for fully moved away

    void Start()
    {
        startingPos = transform.position;
        endPos = startingPos + movementVector;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(startingPos, endPos, CalculateMovementFactor());
    }

    private float CalculateMovementFactor()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        return movementFactor;
    }
}
