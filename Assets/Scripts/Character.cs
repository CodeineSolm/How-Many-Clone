using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected GameObject _ropePartPrefab;
    [SerializeField] protected GameObject _characterPrefab;
    [SerializeField] protected GameObject _container;
    [SerializeField] protected Transform _spawnPoint;
    [SerializeField] protected GameObject _answerPosition;
    [SerializeField] protected AnswerView _answerView;


    protected int _answer;
    protected int _lenght = 1;
    protected float _partDistance = 0.25f;
    protected float _playerDistance = 0.5f;

    public int GetAnswer()
    {
        return _answer;
    }

    public void Drop()
    {
        Destroy(_container.transform.GetChild(1).GetComponent<CharacterJoint>());
        _container.transform.GetChild(1).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
        _answerPosition.gameObject.SetActive(false);
    }

    public void ShowAnswer()
    {
        var answerView = Instantiate(_answerView, new Vector3(_answerPosition.transform.position.x, _answerPosition.transform.position.y, _answerPosition.transform.position.z), 
            Quaternion.identity, _answerPosition.transform);
        answerView.Show(_answer);
    }

    protected void Start()
    {
        Spawn();
        _answerPosition.gameObject.SetActive(true);
    }

    protected void Spawn()
    {
        int count = (int)(_lenght / _partDistance);

        for (int i = 0; i < count; i++)
        {
            GameObject ropeTemp = Instantiate(_ropePartPrefab, new Vector3(_spawnPoint.transform.position.x, _spawnPoint.transform.position.y - _partDistance * (i + 1), 
                _spawnPoint.transform.position.z), Quaternion.identity, _container.transform);
            ropeTemp.name = _container.transform.childCount.ToString();

            if (i == 0)
            {
                Destroy(ropeTemp.GetComponent<CharacterJoint>());
                ropeTemp.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else
                ropeTemp.GetComponent<CharacterJoint>().connectedBody = _container.transform.Find((_container.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();

            if (i == count - 1)
            {
                GameObject characterTemp = Instantiate(_characterPrefab, new Vector3(_spawnPoint.transform.position.x, _spawnPoint.transform.position.y - _playerDistance * (i + 1),
                _spawnPoint.transform.position.z), Quaternion.Euler(new Vector3(0, 180, 0)), _container.transform);
                characterTemp.GetComponent<CharacterJoint>().connectedBody = _container.transform.Find((_container.transform.childCount - 1).ToString()).GetComponent<Rigidbody>();
            }
        }
    }
}
