using System.Collections;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject _splashEffect;

    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        var splashEffect = Instantiate(_splashEffect, collision.GetContact(0).point, Quaternion.identity);
        StartCoroutine(DestroySplashEffect(splashEffect));
    }

    private IEnumerator DestroySplashEffect(GameObject splashEffect)
    {
        yield return new WaitForSeconds(2f);
        Destroy(splashEffect);
    }
}
