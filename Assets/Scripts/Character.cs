using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected GameObject _characterPrefab;
    [SerializeField] protected GameObject _container;
    [SerializeField] protected GameObject _answerContainer;
    [SerializeField] protected AnswerView _answerView;
    [SerializeField] protected GameObject _nameContainer;
    [SerializeField] protected NameView _nameView;

    protected int _answer;
    protected string _name;
    private float _placementTextShiftX = 0.5f;
    private float _placementTextShiftY = 1.2f;
    private CharacterJoint _characterJoint;

    public int GetAnswer()
    {
        return _answer;
    }

    public void Drop()
    {
        _answerContainer.gameObject.SetActive(false);
        Destroy(_characterJoint);
    }

    public void ShowAnswer()
    {
        Transform characterPosition = _container.gameObject.transform.GetChild(_container.gameObject.transform.childCount - 1).transform;
        var answerView = Instantiate(_answerView, new Vector3(characterPosition.position.x + _placementTextShiftX, characterPosition.position.y + _placementTextShiftY, 
            characterPosition.position.z), Quaternion.identity, _answerContainer.transform);
        answerView.Show(_answer);
    }

    public void ShowName()
    {
        var nameView = Instantiate(_nameView, new Vector3(_nameContainer.transform.position.x, _nameContainer.transform.position.y,
            _nameContainer.transform.position.z), Quaternion.identity, _nameContainer.transform);
        //Debug.Log(_name);
        nameView.Show(_name);
    }

    protected void Awake()
    {
        Spawn();
        _answerContainer.gameObject.SetActive(true);
    }

    protected void Spawn()
    {
        var characterTemp = Instantiate(_characterPrefab, new Vector3(_container.transform.position.x, _container.transform.position.y,
            _container.transform.position.z), Quaternion.Euler(new Vector3(0, 180, 0)), _container.transform);
        _characterJoint = characterTemp.gameObject.transform.GetChild(0).gameObject.GetComponent<CharacterJoint>();
    }
}
