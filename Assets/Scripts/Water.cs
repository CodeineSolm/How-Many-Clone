using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject _splashEffect;

    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        Instantiate(_splashEffect, collision.GetContact(0).point, Quaternion.identity);
    }
}
