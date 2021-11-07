using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject _ropePartPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _container;
    [SerializeField] private Transform _spawnPoint;

    private int _lenght = 1;
    private float _partDistance = 0.25f;
    private float _playerDistance = 0.5f;

    private void Start()
    {
        Spawn();
    }

    private void Update()
    {

    }

    private void Spawn()
    {
        int count = (int)(_lenght / _partDistance);

        for (int i = 0; i < count; i++)
        {
            GameObject temp = Instantiate(_ropePartPrefab, new Vector3(_spawnPoint.transform.position.x, _spawnPoint.transform.position.y - _partDistance * (i + 1),
                _spawnPoint.transform.position.z), Quaternion.identity, _container.transform);
            temp.name = _container.transform.childCount.ToString();

            if (i == 0)
            {
                Destroy(temp.GetComponent<CharacterJoint>());
                temp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
                temp.GetComponent<CharacterJoint>().connectedBody = _container.transform.Find((_container.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();

            if (i == count - 1)
            {
                GameObject playerTemp = Instantiate(_enemyPrefab, new Vector3(_spawnPoint.transform.position.x, _spawnPoint.transform.position.y - _playerDistance * (i + 1),
                _spawnPoint.transform.position.z), Quaternion.Euler(new Vector3(0, 180, 0)), _container.transform);
                playerTemp.GetComponent<CharacterJoint>().connectedBody = _container.transform.Find((_container.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
    }
}
