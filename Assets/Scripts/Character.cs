using Obi;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected GameObject _container;
    [SerializeField] protected GameObject _answerContainer;
    [SerializeField] protected AnswerView _answerView;
    [SerializeField] protected GameObject _nameContainer;
    [SerializeField] protected NameView _nameViewTemplate;
    [SerializeField] protected CharacterFlagView _flagViewTemplate;
    [SerializeField] private Rigidbody _characterRigidbody;
    [SerializeField] private ObiRope _rope;

    protected int _answer;
    protected string _name;
    protected Sprite _flag;
    private float _placementTextShiftX = 0.35f;
    private float _placementTextShiftY = 0.7f;
    private float _flagImageShiftX = -0.33f;

    public int GetAnswer()
    {
        return _answer;
    }

    public void Drop()
    {
        _answerContainer.gameObject.SetActive(false);
        var ropeAttachments = _rope.GetComponents(typeof(ObiParticleAttachment));
        Destroy(ropeAttachments[ropeAttachments.Length - 1]);
    }

    public void Fall(float fallDistance)
    {
        _characterRigidbody.isKinematic = false;
        //_playerModel.transform.position = new Vector3(_playerModel.transform.position.x, _playerModel.transform.position.y - fallDistance, _playerModel.transform.position.z);
        Debug.Log(_name + " is dropped by " + fallDistance + " cm");
    }

    public void ShowAnswer()
    {
        Transform characterPosition = _container.gameObject.transform.GetChild(_container.gameObject.transform.childCount - 1).transform;
        var answerView = Instantiate(_answerView, new Vector3(characterPosition.position.x + _placementTextShiftX, characterPosition.position.y + _placementTextShiftY, 
            characterPosition.position.z), Quaternion.identity, _answerContainer.transform);
        answerView.Show(_answer);
    }

    public void ShowInfo()
    {
        var nameView = Instantiate(_nameViewTemplate, new Vector3(_nameContainer.transform.position.x, _nameContainer.transform.position.y,
            _nameContainer.transform.position.z), Quaternion.identity, _nameContainer.transform);
        nameView.Show(_name);
        var flagView = Instantiate(_flagViewTemplate, new Vector3(_nameContainer.transform.position.x + _flagImageShiftX, _nameContainer.transform.position.y,
            _nameContainer.transform.position.z), Quaternion.identity, _nameContainer.transform);
        flagView.RenderFlag(_flag);
    }

    protected void Awake()
    {
        _answerContainer.gameObject.SetActive(true);
    }
}
