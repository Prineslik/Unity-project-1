using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Outline))]

public class Interactable : MonoBehaviour
{
    private Outline outline;
    private Outline outline2;
    private Outline outline3;

    [Header("Дополнительный объект для подсветки")]
    [SerializeField] private GameObject outlineTo;
    [SerializeField] private GameObject outlineTo2;
    [Space]
    [Header("Действия")]
    [SerializeField] private string[] interactions;
    /*
     * винт 3 обл - по х
     * винт 2 обл - по у
     * цилиндр 3 обл - по х
     * цилиндр 2 обл - по у
     * щуп - откр/закр, поднять/отпустить
     */
    [Space]
    [Header("Для scrollbar")]
    [SerializeField] private Slider sliderPref;
    [SerializeField] private Slider sliderPref2;
    [SerializeField] private Slider sliderPref3;
    [SerializeField] private Slider sliderPref4;
    [SerializeField] private Slider sliderPref5;
    [SerializeField] private Button button1;
    [SerializeField] private Text emptyText;
    //[SerializeField] private GameObject menuParent;
    [SerializeField] private Vector3 startPos;

    private ControlMenuManager ControlMM;

    private Slider slider1;
    private Slider slider2;
    private Button button;

    /*private Transform spLShup;
    private Transform spPShup;
    private Transform epLShup;
    private Transform epPShup;*/
    private bool ocShup;

    void Start()
    {
        ocShup = true;

        outline = GetComponent<Outline>();
        outline.OutlineWidth = 0;

        if (outlineTo != null)
        {
            outline2 = outlineTo.GetComponent<Outline>();
            outline2.OutlineWidth = 0;
        }
        if (outlineTo2 != null)
        {
            outline3 = outlineTo2.GetComponent<Outline>();
            outline3.OutlineWidth = 0;
        }

        if (GameObject.Find("ControlMenuManager"))
        {
            ControlMM = GameObject.Find("ControlMenuManager").GetComponent<ControlMenuManager>();
        }
        GameObject parent = GameObject.Find("SlideScroll").transform.GetChild(0).GetChild(0).gameObject;

        CreateSlider(parent);
    }

    private void CreateSlider(GameObject parent)
    {
        if (interactions != null && transform.name != "заглушка")
        {
            char[] trimChar = { ' ', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'К'};
            //emptyText.text = transform.name;
            //Instantiate(emptyText, parent.transform);
            Text text;
            foreach (string item in interactions)
            {
                switch (item)
                {
                    case "rotatOnX":
                        Debug.Log("rotatOnX");
                        slider1 = Instantiate(sliderPref, parent.transform);
                       // slider1.transform.localPosition = startPos;
                        slider1.onValueChanged.AddListener(OnChangeX);
                        text = slider1.transform.GetChild(0).GetComponent<Text>();
                        text.text = "Поврот X " + transform.name.Trim(trimChar);
                        //startPos.y -= 60;
                        break;
                    case "rotatOnY":
                        Debug.Log("rotatOnY");
                        slider1 = Instantiate(sliderPref, parent.transform);
                        //slider1.transform.localPosition = startPos;
                        slider1.onValueChanged.AddListener(OnChangeY);
                        text = slider1.transform.GetChild(0).GetComponent<Text>();
                        text.text = "Поврот Y " + transform.name.Trim(trimChar);
                        //startPos.y -= 60;
                        break;
                    case "UpDownC":
                        Debug.Log("UpDownC");
                        slider2 = Instantiate(sliderPref4, parent.transform);
                        //slider2.transform.localPosition = startPos;
                        slider2.onValueChanged.AddListener(OnCUpDownChange);
                        text = slider2.transform.GetChild(0).GetComponent<Text>();
                        text.text = "Выдвигание " + transform.name.Trim(trimChar);
                        //startPos.y -= 60;
                        break;
                    case "UpDownC2":
                        Debug.Log("UpDownC2");
                        slider2 = Instantiate(sliderPref5, parent.transform);
                        //slider2.transform.localPosition = startPos;
                        slider2.onValueChanged.AddListener(OnCUpDownChange);
                        text = slider2.transform.GetChild(0).GetComponent<Text>();
                        text.text = "Выдвигание " + transform.name.Trim(trimChar);
                        //startPos.y -= 60;
                        break;
                    case "UpDownV":
                        Debug.Log("UpDownV");
                        slider2 = Instantiate(sliderPref2, parent.transform);
                        //slider2.transform.localPosition = startPos;
                        slider2.onValueChanged.AddListener(OnVUpDownChange);
                        text = slider2.transform.GetChild(0).GetComponent<Text>();
                        text.text = "Выдвигание " + transform.name.Trim(trimChar);
                        //startPos.y -= 60;
                        break;
                    case "UpDownV2":
                        Debug.Log("UpDownV2");
                        slider2 = Instantiate(sliderPref3, parent.transform);
                        //slider2.transform.localPosition = startPos;
                        slider2.onValueChanged.AddListener(OnVUpDownChange2);
                        text = slider2.transform.GetChild(0).GetComponent<Text>();
                        text.text = "Выдвигание " + transform.name.Trim(trimChar);
                        //startPos.y -= 60;
                        break;
                    case "OpnCls":
                        Debug.Log("OpnCls");
                        button = Instantiate(button1, parent.transform);
                        button.onClick.AddListener(OnShupClick);

                        //text = button1.transform.GetChild(0).GetComponent<Text>();
                        //text.text = "Открыть/Закрыть щуп";
                        break;
                    //case "Podjem":
                    //    Debug.Log("Podjem");
                    //    button = Instantiate(button1, parent.transform);
                    //    //button.onClick.AddListener(OnShupClick);
                    //    break;
                    default:
                        Debug.Log("EMPTY v "+transform.name);
                        //Instantiate(emptyText, parent.transform);
                        break;
                }
            }
        }
    }
    
    public void OnClickHandler()
    {
        startPos = new Vector3(0, 380, 0);
        Debug.Log("Нажал на - "+transform.name + " parent rotation - "+ this.transform.parent.rotation.eulerAngles);//проверка на нажатие
        ControlMM.ifClose();
        GameObject parent = GameObject.Find("ScrollMenu");
        //назначение действий для объекта
        GameObject target = GameObject.Find("targetRotation");
        target.transform.position = this.transform.position;
        //target.transform.position = this.transform.parent.position;
        /*if (parent.transform.childCount != 0)//очищение меню управления
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Destroy(parent.transform.GetChild(i).gameObject);
            }
        }
        CreateSlider(parent);*/
    }

    public void OnChangeX(float f)
    {
        this.transform.parent.localRotation = Quaternion.Euler(0, 0, slider1.value);
    }

    //public void Podjem()
    //{
    //    Debug.Log("ОБЪЕКТ ПОДНЯТ");
    //}

    public void OnShupClick()
    {
        Debug.Log("OnShupClick");
        GameObject parent = this.transform.parent.gameObject;

        GameObject lShup = parent.transform.GetChild(1).gameObject;
        GameObject pShup = parent.transform.GetChild(2).gameObject;
        if (ocShup)
        {
            StartCoroutine(MoveShup(lShup, pShup, -0.24f));
            ocShup = false;
        }
        else
        {
            StartCoroutine(MoveShup(lShup, pShup, 0.24f));
            ocShup = true;
        }
    }  

    private IEnumerator MoveShup(GameObject lPart, GameObject rPart, float rast)
    {
        //Vector3 startL = new Vector3(lPart.transform.localPosition.x, lPart.transform.localPosition.y, lPart.transform.localPosition.z);
        //Vector3 startR = new Vector3(rPart.transform.localPosition.x, rPart.transform.localPosition.y, rPart.transform.localPosition.z);

        Vector3 targetL = new Vector3(lPart.transform.localPosition.x, lPart.transform.localPosition.y, lPart.transform.localPosition.z + rast);
        Vector3 targetR = new Vector3(rPart.transform.localPosition.x, rPart.transform.localPosition.y, rPart.transform.localPosition.z - rast);

        while (lPart.transform.localPosition != targetL)
        {
            lPart.transform.localPosition = Vector3.MoveTowards(lPart.transform.localPosition, targetL, 0.001f);
            rPart.transform.localPosition = Vector3.MoveTowards(rPart.transform.localPosition, targetR, 0.001f);
            Debug.Log("CORUTINE");
            yield return null;
        }
        Debug.Log("END CORUTINE "+ lPart.transform.localPosition);
    }

    public void OnChangeY(float f)
    {
        if (this.transform.parent.localRotation.z <= 0)
        {
            this.transform.parent.localRotation = Quaternion.Euler(0, slider1.value, -90);
        }
        else
        {
            this.transform.parent.localRotation = Quaternion.Euler(0, slider1.value, 90);
        }
    }

    public void OnVUpDownChange(float f)
    {
        GameObject zag = this.transform.parent.GetChild(1).gameObject;
        zag.transform.localPosition = new Vector3(0, slider2.value / 100, 0);
    }

    public void OnVUpDownChange2(float f)
    {
        GameObject zag = this.transform.parent.GetChild(1).gameObject;
        zag.transform.localPosition = new Vector3(0, slider2.value / 100, 0);
    }

    public void OnCUpDownChange(float f)
    {
        GameObject zag = this.transform.parent.GetChild(1).gameObject;
        zag.transform.localPosition = new Vector3(0, slider2.value / 100, 0);
    }

    public void OnCUpDownChange2(float f)
    {
        GameObject zag = this.transform.parent.GetChild(1).gameObject;
        zag.transform.localPosition = new Vector3(0, slider2.value / 100, 0);
    }

    public void OnHoverEnter()
    {
        outline.OutlineWidth = 5;
        if (outlineTo != null)
        {
            outline2.OutlineWidth = 5;
        }
        if (outlineTo2 != null)
        {
            outline3.OutlineWidth = 5;
        }
    }

    public void OnHoverExit()
    {
        outline.OutlineWidth = 0;
        if (outlineTo != null)
        {
            outline2.OutlineWidth = 0;
        }
        if (outlineTo2 != null)
        {
            outline3.OutlineWidth = 0;
        }
        Input.GetMouseButton(0).ToString();
    }
}
