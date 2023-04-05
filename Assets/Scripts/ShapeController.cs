using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    public RectTransform rectTransform;
    public float speed;
    public Sekil sekil;
    public float width;
    void Start()
    {
        width = Screen.width;
    }

    void Update()
    {
        if(rectTransform.anchoredPosition.x > 1500)
        {
            Destroy(gameObject);
        }

       
    }

    void FixedUpdate() 
    {
        rectTransform.Translate(Vector3.right * Globals.speed * Time.deltaTime * (width / 500f));
    }
}

public enum Sekil
{
    Daire,
    Ucgen,
    Kare
}
