using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;

public class ShemaManager : MonoBehaviour
{
    [Header("Перемещение сцены")]
    [SerializeField] private Vector3 targetPos;
    [SerializeField] private GameObject target;
    [SerializeField] private Vector3 cameraTargetPos;
    [SerializeField] private GameObject camera;
    [SerializeField] private float speed;
    private Vector3 startTartget;
    private Vector3 cameraStartTarget;

    [Space]
    [Header("Звенья")]
    [SerializeField] private GameObject v2;
    [SerializeField] private GameObject z2;
    [SerializeField] private GameObject c2;
    [SerializeField] private GameObject con;

    [Header("Звенья после крутилки")]
    [SerializeField] private GameObject v3;
    [SerializeField] private GameObject z3;
    [SerializeField] private GameObject c3;
    [Header("Кнопки в панели")]
    [SerializeField] private List<GameObject> createButtons;
    [Space]
    [Header("Выбранный слот")]
    [SerializeField] public GameObject choseSlot;
    [Space]
    [Header("Слоты")]
    [SerializeField] private List<GameObject> slotList;

    [SerializeField] private Transform slotPar;
    private Transform preSlotPar;
    [SerializeField] private Transform robotElemPar;
    [SerializeField] private Transform startContainer;
    [SerializeField] private GameObject Content;
    [SerializeField] private GameObject excPref;

    [SerializeField] private List<GameObject> slotsPref;
    [SerializeField] private GameObject activeSlots;
    [Space]
    [Header("Менеджеры")]
    [SerializeField] private GameObject listManager;

    [SerializeField] private GameObject CMM;
    


    private void Start()
    {
        CreateSlots(0);
        cameraStart.localPosition = new Vector3(0,1,-10);
        excPref.SetActive(false);
    }

    bool cameraMove;
    bool cameraChek;

    private Transform cameraStart;

    private void Update()
    {
        if (cameraMove)
        {
            camera.transform.position = Vector3.MoveTowards(camera.transform.position, cameraTargetPos, speed * Time.deltaTime);
            if (camera.transform.position == cameraTargetPos)
            {
                cameraMove = false;
                cameraChek = true;
            }
        }
        if (cameraChek == true)
        {
            camera.GetComponent<CameraRotateAround>().enabled = true;
            cameraChek = false;
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
            camera.GetComponent<CameraRotateAround>().enabled = false;
            target.SetActive(true);
            camera.transform.localPosition = new Vector3(0, 1, -10);
            camera.transform.localRotation = new Quaternion(0,0,0,0);
            Destroy(startContainer.GetChild(1).GetChild(0).gameObject);
            robotElemPar = startContainer;
            Debug.Log("11111111");
            //controlMenuManager.closeOpenMenu();
            CMM.GetComponent<ControlMenuManager>().ifOpen();

            //Content
            for (int i = 0; i < Content.transform.childCount; i++)
            {
                Destroy(Content.transform.GetChild(i).gameObject);
            }
        }
    }

    public void ClearSheme()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void HideExc()
    {
        excPref.SetActive(false);
    }

    public void CreateSlots(int variant)// 0 - UP; 1 - LEFT; 2 - RIGHT
    {
        var connectorPos = slotPar.GetChild(0).position;
        slotList = slotsPref[variant].GetComponent<SlotsManager>().slots;
        Debug.Log("Size delta - "+ slotPar.GetComponent<RectTransform>().sizeDelta.y);
        activeSlots = Instantiate(slotsPref[variant], connectorPos, new Quaternion(0,0,0,0), slotPar);

        foreach (var item in slotList)
        {
            item.GetComponent<Slot>().shemaManager = gameObject;
        }
    }

    public void AddButton(GameObject button)
    {
        createButtons.Add(button);
    }

    public void DeleteLastElement()
    {
        var shema = listManager.GetComponent<ListManager>();
        shema.DeleteLastNode();
        Destroy(slotPar.gameObject);
        slotPar = preSlotPar;
        preSlotPar = slotPar.parent.transform;
        CreateSlots(0);
    }

    public void CreateElement(Image element, GameObject robotElement)
    {
        var shema = listManager.GetComponent<ListManager>();
        var connectorPos = slotPar.GetChild(0).position;

        Debug.Log("kol-vo" + shema.shema.Count);
        Debug.Log(element.name);

        if (shema.shema.Count > 0 && shema.lastElement().element.name.Contains("Щуп"))
        {
            Debug.Log("СТОИТ ЩУП!!!");
            excPref.SetActive(true);
            excPref.transform.GetChild(0).GetComponent<Text>().text = "После щупа нельзя ставить элемент";
            return;
        }
        else
        {
            if (shema.shema.Count > 0 && shema.lastElement().element.name.Contains("Вращательн") && element.name.Contains("Вращательн"))
            {
                Debug.Log("СТОИТ КРУТИЛКА!!!");
                excPref.SetActive(true);
                excPref.transform.GetChild(0).GetComponent<Text>().text = "Нельзя ставить два плоских шарнирных соединения подряд";

                return;
            }
            else
            {
                if (element.name.Contains("Вращательн"))
                {
                    if (shema.shema.Count == 0)
                    {
                        excPref.SetActive(true);
                        excPref.transform.GetChild(0).GetComponent<Text>().text = "Нельзя ставить плоское шарнирное соединение после основания";
                    }
                    shema.AddNode(robotElement, shema.lastElemRotation());//Quaternion.Euler(0, 0, 0));
                    preSlotPar = slotPar;
                    slotPar = Instantiate(element, connectorPos, Quaternion.Euler(0, 0, 0), slotPar).transform;
                }
                else
                if (element.name.Contains("Звено") || element.name.Contains("Винтовое") || element.name.Contains("вдоль"))
                {
                    if (shema.shema.Count > 0)
                    {
                        if (shema.lastElement().element.name.Contains("Вращательн"))
                        {
                            Debug.Log("VARIANT 1");

                            if (element.name.Contains("Звено"))
                            {
                                shema.AddNode(z3, choseSlot.transform.rotation);
                            }
                            if (element.name.Contains("Винтовое"))
                            {
                                shema.AddNode(v3, choseSlot.transform.rotation);
                            }
                            if (element.name.Contains("вдоль"))
                            {
                                shema.AddNode(c3, choseSlot.transform.rotation);
                            }
                            preSlotPar = slotPar;
                            slotPar = Instantiate(element, connectorPos, choseSlot.transform.rotation, slotPar).transform;
                        }
                        else
                        {
                            Debug.Log("VARIANT 2");
                            if (element.name.Contains("Звено"))
                            {
                                if (choseSlot.name == "Slot")
                                {
                                    shema.AddNode(z2, choseSlot.transform.rotation);
                                }
                                else
                                {
                                    shema.AddNode(robotElement, choseSlot.transform.rotation);
                                }
                            }
                            if (element.name.Contains("Винтовое"))
                            {
                                if (choseSlot.name == "Slot")
                                {
                                    shema.AddNode(robotElement, choseSlot.transform.rotation);
                                }
                                else
                                {
                                    shema.AddNode(v2, choseSlot.transform.rotation);
                                }
                            }
                            if (element.name.Contains("вдоль"))
                            {
                                if (choseSlot.name == "Slot")
                                {
                                    shema.AddNode(robotElement, choseSlot.transform.rotation);
                                }
                                else
                                {
                                    shema.AddNode(c2, choseSlot.transform.rotation);
                                }
                            }
                            preSlotPar = slotPar;
                            slotPar = Instantiate(element, connectorPos, choseSlot.transform.rotation, slotPar).transform;
                        }
                    }
                    else
                    {
                        Debug.Log("VARIANT 3");
                        if (element.name.Contains("Звено"))
                        {
                            shema.AddNode(z2, choseSlot.transform.rotation);
                        }
                        else
                        {
                            shema.AddNode(robotElement, choseSlot.transform.rotation);
                        }
                        preSlotPar = slotPar;
                        slotPar = Instantiate(element, connectorPos, choseSlot.transform.rotation, slotPar).transform;
                    }
                }
                else
                {
                    Debug.Log("VARIANT 4");
                    shema.AddNode(robotElement, choseSlot.transform.rotation);
                    preSlotPar = slotPar;
                    slotPar = Instantiate(element, connectorPos, choseSlot.transform.rotation, slotPar).transform;
                }

                Debug.Log("IMA - " + element.name);

                slotPar.position = slotPar.GetChild(0).position;

                Destroy(activeSlots);
                CreateSlots(0);
            }
        }
    }

    public void OnCreateRobotClick()
    {
        var shema = listManager.GetComponent<ListManager>().shema;
        string s = "Схема: ";
       
        foreach (var item in shema)
        {
            s += item.element.name + " >> ";

            var connectorPos = robotElemPar.GetChild(0).position;
            Debug.Log(item.element.name + " " + robotElemPar.GetChild(0).localPosition);
            if (item.element.name.Contains("Щуп"))
            {
                robotElemPar = Instantiate(item.element, connectorPos, robotElemPar.rotation, robotElemPar.GetChild(1)).transform;
            }
            else
            {
                robotElemPar = Instantiate(item.element, connectorPos, item.rotationElement, robotElemPar.GetChild(1)).transform;
            }
            //robotElemPar.position = robotElemPar.GetChild(0).position;
        }
        Debug.Log(s);

        //перемещение сцены

        //windowMove = true;
        //windowHide = true;
        target.SetActive(false);
        cameraMove = true;
    }

    public async void OnSaveShemeClick()
    {
        var shema = listManager.GetComponent<ListManager>().shema;
        string s = "";
        foreach (var item in shema)
        {
            s += $"[{item.element.name};{item.rotationElement}]";
        }
        Debug.Log("Saved file: "+s);
        string path = @"D:\4 курс\курсовой САПР\shema.txt";
        using (FileStream fstream = new FileStream(path, FileMode.OpenOrCreate))
        {
            // преобразуем строку в байты
            byte[] buffer = Encoding.Default.GetBytes(s);
            // запись массива байтов в файл
            await fstream.WriteAsync(buffer, 0, buffer.Length);
        }
        //(element name, rotation)(name 2, rot 2) ... (name N, rot N)
    }

    public async void OnLoadShemaClick()
    {
        var shema = listManager.GetComponent<ListManager>().shema;
        string textFromFile = "";
        string path = @"D:\4 курс\курсовой САПР\shema.txt";

        using (FileStream fstream = File.OpenRead(path))
        {
            // выделяем массив для считывания данных из файла
            byte[] buffer = new byte[fstream.Length];
            // считываем данные
            await fstream.ReadAsync(buffer, 0, buffer.Length);
            // декодируем байты в строку
            textFromFile = Encoding.Default.GetString(buffer);
            Debug.Log("Load shema: "+textFromFile);
        }
        string[] elementInf = textFromFile.Split(new[] { '[', ']' }, System.StringSplitOptions.RemoveEmptyEntries);
        string[][] elements = new string [elementInf.Length][]; 
        for (int i = 0; i < elements.Length; i++)
        {
            //for (int j = 0; j < 2; j++)
            //{
                string[] NR = elementInf[i].Split(';');
                //Debug.Log("1 - "+NR[0]);
                //Debug.Log("2 - " + NR[1]);
                elements[i] = new string[] { NR[0], NR[1] };
            //}
        }
        Debug.Log("----------------");
        foreach (var item in elements)
        {
            foreach (var item2 in item)
            {
                Debug.Log("Array - " + item2);
            }
        }
        Debug.Log("----------------");
    }

    public void ExitApp()
    {
        UnityEngine.Application.Quit();
    }
}