using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform Player;
    [SerializeField] private float smoothFollowTime;

    private Vector3 minusVector;

    private void Start()
    {
        minusVector = Player.position - transform.position;
    }

    void Update()
    {
        // Set Camera Position
        Vector3 playerVector = Player.position;
        Vector3 targetVector = new Vector3(playerVector.x - minusVector.x, playerVector.y - minusVector.y, playerVector.z - minusVector.z);
        transform.position = Vector3.Lerp(transform.position, targetVector, smoothFollowTime * Time.deltaTime);
    }
}
