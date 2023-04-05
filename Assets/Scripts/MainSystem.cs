using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class MainSystem : MonoBehaviour
{
    [SerializeField] private GameObject[] sekiller;
    [SerializeField] private Transform canvasRoot;
    [SerializeField] private Slider slider;
    [SerializeField] TextMeshProUGUI dogruText;
    [SerializeField] TextMeshProUGUI yanlisText;
    [SerializeField] private GameObject[] sekilBantlari;
    private int dogruSayisi = 0;
    private int yanlisSayisi = 0;
    private bool isCollected = false;
    private GameObject selectedShape;

    void Start()
    {
        Globals.speed = PlayerPrefs.GetFloat("Hiz",5);
        slider.value = Globals.speed;
        StartCoroutine(SpawnShapes());
    }

    // Update is called once per frame
    void Update()
    {
        
        if((Input.GetMouseButtonDown(1) || (Application.platform == RuntimePlatform.Android && Input.GetTouch(0).phase == TouchPhase.Began)) && !isCollected)
        {
            ShapeController[] _shapes = FindObjectsOfType<ShapeController>();
            foreach(ShapeController _shape in _shapes)
            {
                Vector3 _input = Application.platform == RuntimePlatform.Android ? Input.GetTouch(0).position : Input.mousePosition;
                if (RectTransformUtility.RectangleContainsScreenPoint(_shape.rectTransform, _input))
                {
                    _shape.gameObject.SetActive(false);
                    selectedShape = _shape.gameObject;
                    isCollected = true;
                    break;
                }
            } 
        }
        else if((Input.GetMouseButtonDown(1) || (Application.platform == RuntimePlatform.Android && Input.GetTouch(0).phase == TouchPhase.Began)) && isCollected)
        {
            Vector3 _input = Application.platform == RuntimePlatform.Android ? Input.GetTouch(0).position : Input.mousePosition;
            selectedShape.transform.position = _input;
            selectedShape.SetActive(true);
            isCollected = false;
            RectTransform _rect = sekilBantlari[(int)selectedShape.GetComponent<ShapeController>().sekil].GetComponent<RectTransform>();

            Vector2 mousePos = _input;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_rect, mousePos, null, out Vector2 localPos);
            localPos.x = 0;
            float distance = Vector2.Distance(localPos, _rect.rect.center);
            Debug.Log("Distance: " + distance);


            if(distance < 30)
            {
                dogruSayisi++;
            }
            else
            {
                yanlisSayisi++;
            }
            
            dogruText.text = $"{dogruSayisi}";
            yanlisText.text = $"{yanlisSayisi}";
        }

    }

    public void ChangeSpeed(float _speed)
    {
        Globals.speed = _speed;
        PlayerPrefs.SetFloat("Hiz",_speed);
    }

    IEnumerator SpawnShapes()
    {
        while(true)
        {
            int _rnd = Random.Range(0,3);
            GameObject _clone = Instantiate(sekiller[_rnd]);
            _clone.transform.position = sekiller[Random.Range(0,3)].transform.position;
            _clone.transform.SetParent(canvasRoot);
            _clone.SetActive(true);
            _clone.transform.localScale = Vector3.one;
            yield return new WaitForSeconds(3 - (Globals.speed / 75f));
        }
    }
}
