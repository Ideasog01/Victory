using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private Vector3 rotationSpeed;

    private void Update()
    {
        this.transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}
