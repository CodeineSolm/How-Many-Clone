using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Water : MonoBehaviour
{
    [SerializeField] private GameObject _splashEffect;

    public event UnityAction<GameObject> PlayerDropped;

    private void OnCollisionEnter(Collision collision)
    {
        Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        var splashEffect = Instantiate(_splashEffect, collision.GetContact(0).point, Quaternion.identity);
        StartCoroutine(DestroySplashEffect(splashEffect));
        PlayerDropped?.Invoke(collision.gameObject);
    }

    private IEnumerator DestroySplashEffect(GameObject splashEffect)
    {
        yield return new WaitForSeconds(2f);
        Destroy(splashEffect);
    }
}
