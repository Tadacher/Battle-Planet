using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RespawnTimer : MonoBehaviour
{
    public float _timeLeft;
    [SerializeField] private Canvas _canvas;
    private Text _text;
    
    private void Start() => _text = GetComponent<Text>();
    private void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (_canvas.transform as RectTransform, Input.mousePosition, _canvas.worldCamera, out pos);
        
        transform.position = _canvas.transform.TransformPoint(pos);
            
        _timeLeft -= Time.deltaTime;
        _text.text = string.Format("{0:0.0}", _timeLeft);

        if (_timeLeft <= 0) 
            gameObject.SetActive(false);
    }
}
