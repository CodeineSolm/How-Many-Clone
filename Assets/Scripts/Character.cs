using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected GameObject _ropePartPrefab;
    [SerializeField] protected GameObject _characterPrefab;
    [SerializeField] protected GameObject _container;
    [SerializeField] protected Transform _spawnPoint;


    protected int _lenght = 1;
    protected float _partDistance = 0.25f;
    protected float _playerDistance = 0.5f;

    protected void Start()
    {
        Spawn();
    }

    protected void Spawn()
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
                GameObject characterTemp = Instantiate(_characterPrefab, new Vector3(_spawnPoint.transform.position.x, _spawnPoint.transform.position.y - _playerDistance * (i + 1),
                _spawnPoint.transform.position.z), Quaternion.Euler(new Vector3(0, 180, 0)), _container.transform);
                characterTemp.GetComponent<CharacterJoint>().connectedBody = _container.transform.Find((_container.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
    }
}
