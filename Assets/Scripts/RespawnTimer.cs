using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnTimer : MonoBehaviour
{
    public float timeLeft;
    [SerializeField]
    Canvas myCanvas;
    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }
    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle
            (myCanvas.transform as RectTransform, Input.mousePosition, myCanvas.worldCamera, out pos);
        
        transform.position = myCanvas.transform.TransformPoint(pos);
        
        
        
        timeLeft -= Time.deltaTime;
        text.text = string.Format("{0:0.0}", timeLeft);

        if (timeLeft <= 0) gameObject.SetActive(false);


    }
}
