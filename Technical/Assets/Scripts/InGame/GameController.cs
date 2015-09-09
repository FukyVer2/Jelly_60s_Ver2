using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using PathologicalGames;

public class GameController : MonoSingleton<GameController>, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    public Sprite[] gemImageStart;
    public Sprite[] gemImageChange;
    public GameObject gemPrefabs;
    public GameObject[] cucDacBiet;
    public GameObject conect;
    public GameObject destroyGem;
    [HideInInspector]
    public List<GameObject> ListDelete;//list Object de xoa

    public RectTransform canvasRectTransform;

    public int countRow;//so hang cua mang
    public int countCollumn;//so cot cua mang
    public int iTwenPos;//vi tri in ra luc dau
    private float localScale = 1;//kich thuc gem

    public int GemHeight;
    public int GemWitdh;

    [HideInInspector]
    public bool activeTimeHelp = false;

    public float timeHelp = 5;

    [HideInInspector]
    public bool activeDestroyGem = false;

    [HideInInspector]
    public int score = 0;
    public Transform tranfsIn;
    public Transform tranfsOut;
    public GameObject timeStar;
    public Transform gemContainer;
    public Transform conectContainer;

    [HideInInspector]
    public GameObject[][] arrGem;//list Game Object hien ra man hinh
    //private RaycastHit2D rayHit;
    [HideInInspector]
    private List<GameObject> listConect;//tao lien ket cho cac cuc(sau nay thanh thanh Animation)
    //[HideInInspector]
    public List<List<GameObject>> listLoangDau;//kiem tra con duong nao de an khong
    [HideInInspector]
    private List<GameObject> listMouse;
    //private int index;//so thu tu cac Prefabs  
    [HideInInspector]
    private float scale = 0.01f;
    [HideInInspector]
    private bool boolScale = false;
    [HideInInspector]
    public bool activeHelp;
    private float help;
    //private Gem gem;
    //private int vitriX, vitriY;

    [HideInInspector]
    public int indexRandom;

    [HideInInspector]
    public bool activeAddtime;
    public float disX;
    public float disY;

    public UIController uiController;
    public GameObject hieuUng;
    private List<GameObject> listHieuUng = new List<GameObject>();
    public List<Gem> listGemAddTime = new List<Gem>();
    void Awake()
    {

    }

    // Use this for initialization
    public void Start()
    {
        ListDelete = new List<GameObject>();
        listMouse = new List<GameObject>();
        listLoangDau = new List<List<GameObject>>();
        listConect = new List<GameObject>();
        activeAddtime = false;
        activeTimeHelp = true;
        RandomGem();// random hinh anh khi moi dua vao game o vi tri ItweenPos	
        CheckListInvalid();
        SetTotalGem();

    }


    float xxx = 0;
    bool yyy = false;
    // Update is called once per frame
    void Update()
    {

        if (activeDestroyGem == true)
        {
            CacCucRoiXuong();
            if (yyy == true)
            {
                if (xxx > 1f)
                {
                    RenderSpecial();
                }
                xxx += Time.deltaTime;
            }
        }

        if (activeTimeHelp == true)
        {
            localScale += scale;
            if (localScale > 1.2)
            {
                scale = -0.01f;
            }
            if (localScale < 0.8)
            {
                scale = 0.01f;
            }

            if (help > timeHelp)
            {
                activeHelp = true;
            }
            if (help > 1.5f)
            {
                if (listLoangDau.Count == 0)//neu k con duong nao de an Ramdom lai map
                {
                    RandomMap();
                }
            }
            help += Time.deltaTime;
        }
        if (activeTimeHelp == false)
        {
            activeHelp = false;
            help = 0;
        }
        if (activeHelp == true)
        {
            ScaleGem();
        }
        if (activeHelp == false)
        {
            ResetScaleGem();
        }
        if (uiController.isGameOver)
        {
            for (int i = 0; i < listHieuUng.Count; i++)
            {
                Destroy(listHieuUng[i]);
            }

        }
        //RenderNen(ListDelete);
    }
    //Random Gem ra màn hình
    public void RandomGem()
    {
        arrGem = new GameObject[countCollumn][];//khởi tạo 1 mảng 2 chiều
        for (int i = 0; i < countCollumn; i++)
        {
            arrGem[i] = new GameObject[countRow];//khởi tạo 1 mảng 2 chiều
        }
        for (int i = 0; i < countCollumn; i++)
        {
            for (int j = 0; j < countRow; j++)
            {
                InstantiateGem(i, j, 0);//in ra cac Object o vi tri PosIT
            }
        }
    }
    public int[] maxGem;
    private int[] totalGemColor = new int[5];
    void addTotalGem(int index)
    {
        totalGemColor[index] += 1;
    }
    void SubGem(int index)
    {
        totalGemColor[index] -= 1;
    }
    void SubTotalGem(int totalGemDelete, int indexTotalGemDelete)
    {
        totalGemColor[indexTotalGemDelete] -= totalGemDelete;
        totalGemDelete = 0;
    }

    void SetTotalGem()
    {
        for (int i = 0; i < 5; i++)
        {
            totalGemColor[i] = 0;
        }
        for (int i = 0; i < countCollumn; i++)
        {
            for (int j = 0; j < countRow; j++)
            {
                if (arrGem[i][j] != null)
                {
                    Gem _gem = arrGem[i][j].GetComponent<Gem>();
                    if (_gem != null)
                    {
                        if (_gem.inDex == 0)
                        {
                            totalGemColor[0] += 1;
                        }
                        if (_gem.inDex == 1)
                        {
                            totalGemColor[1] += 1;
                        }
                        if (_gem.inDex == 2)
                        {
                            totalGemColor[2] += 1;
                        }
                        if (_gem.inDex == 3)
                        {
                            totalGemColor[3] += 1;
                        }
                        if (_gem.inDex == 4)
                        {
                            totalGemColor[4] += 1;
                        }
                    }
                }
            }
        }
    }
    void InstantiateGem(int row, int collumn, int ItPos)
    {
        int index;
        do
        {
            index = Random.Range(0, 5);

        } while (totalGemColor[index] >= maxGem[index]);
        addTotalGem(index);
        //GameObject a = Instantiate(listGem[index], Vector3.zero, Quaternion.identity) as GameObject; //new Vector3(row * 0.75f - x, collumn * 0.75f - y + posItween, 0)
        //add vao Canvas        
        GameObject gemObj = SpawnGem(gemPrefabs, "gem");

        gemObj.GetComponent<Gem>().spriteStart = gemImageStart[index];
        gemObj.GetComponent<Gem>().spriteChange = gemImageChange[index];

        Vector3 pos = new Vector3((row - 3.0f) * (GemWitdh + disX), (collumn - 3.5f) * (GemHeight + disY) + ItPos, 1);

        gemObj.transform.SetParent(gemContainer);
        gemObj.transform.localScale = Vector3.one;
        gemObj.transform.localPosition = pos;
        arrGem[row][collumn] = gemObj;

        Gem gem = gemObj.GetComponent<Gem>();
        if (gem != null)
        {
            gem.SetProfile(collumn, row, index);
            gem.Reset();
            int randomSpecial = Random.Range(1, 100);
            if (randomSpecial <= 15)
            {
                int randTypeSpecial = Random.Range(1, 100);
                if (randTypeSpecial < 40)
                {
                    gem.isSpecialCollum = true;
                }
                if (randTypeSpecial >= 40 && randTypeSpecial < 80)
                {
                    gem.isSpecialRow = true;
                }
                if (randTypeSpecial >= 80)
                {
                    gem.isSpecial = true;
                }

            }
            gem.ResetSprite();
            gem.ResetSpriteStart();
            gem.ResetActive();
        }
        Vector3 posIT = new Vector3((row - 3.0f) * (GemWitdh + disX), (collumn - 3.5f) * (GemHeight + disY), 1);
        arrGem[row][collumn].GetComponent<Gem>().MovePosition(posIT, 0.5f);

    }
    public GameObject SpawnGem(GameObject obj, string nameSpawnPool)
    {
        SpawnPool gemPool = PoolManager.Pools[nameSpawnPool];
        //Transform gemObj = gemPool.Spawn(listGem[index]);
        Transform gemObj = gemPool.Spawn(obj);
        return gemObj.gameObject;
    }

    public void DespawnGem(Transform gemTrans, string nameSpawnPool)
    {

        SpawnPool gemPool = PoolManager.Pools[nameSpawnPool];
        gemPool.Despawn(gemTrans);
    }
    #region Game
    public void InstantiateTimeStar()
    {
        int row;
        int collumn;
        do
        {
            row = Random.Range(0, countCollumn);
            collumn = Random.Range(0, countRow);
            //if (arrGem[row][collumn] == null || arrGem[row][collumn].GetComponent<Gem>().timeAdd == true)
            //{
            //    row = Random.Range(0, countCollumn);
            //    collumn = Random.Range(0, countRow);
            //}
        } while (arrGem[row][collumn] == null || arrGem[row][collumn].GetComponent<Gem>().timeAdd == true);
        if (arrGem[row][collumn] != null)
        {
            GameObject a = Instantiate(timeStar, tranfsIn.position, Quaternion.identity) as GameObject;
            a.GetComponent<Gem>().MovePositionStar(arrGem[row][collumn].transform.position, 1.0f);
            a.transform.parent = arrGem[row][collumn].transform;
            //a.transform.parent = gemContainer;
            a.transform.localScale = Vector3.one;
            Gem gem = arrGem[row][collumn].GetComponent<Gem>();
            TimeAdd t = a.GetComponent<TimeAdd>();
            if (gem != null)
            {
                gem.timeAdd = true;
                gem.addTimeGame = uiController.timeAddValue;
                uiController.timeAddValue = 0;
                if (!listGemAddTime.Contains(gem))
                {
                    listGemAddTime.Add(gem);
                }
                if (t != null)
                {
                    t.textTime.text = gem.addTimeGame.ToString();
                }
            }

            activeAddtime = false;
        }

    }
    void InstantiateConect(GameObject gameObj1, GameObject gameObj2)
    {
        if (gameObj1 == null || gameObj2 == null)
        {
            return;
        }
        float x, y;
        x = (gameObj1.transform.position.x + gameObj2.transform.position.x) / 2;
        y = (gameObj1.transform.position.y + gameObj2.transform.position.y) / 2;

        GameObject a = SpawnGem(conect, "conect");

        a.transform.SetParent(conectContainer);
        Vector3 pos = new Vector3(x, y, 0);
        a.transform.localScale = new Vector3(0.45f, 0.45f, 0);

        EffectController effect = a.GetComponent<EffectController>();
        if (effect != null)
        {
            if (gameObj1.transform.localPosition.x < gameObj2.transform.localPosition.x || gameObj1.transform.localPosition.y < gameObj2.transform.localPosition.y)
            {
                effect.SetPositionBetweenTwoGem(gameObj1.transform, gameObj2.transform);
            }

            else
            {
                effect.SetPositionBetweenTwoGem(gameObj2.transform, gameObj1.transform);
            }


            listConect.Add(a);
        }
    }
    private List<GameObject> listItween = new List<GameObject>();
    private List<GameObject> listDacBiet = new List<GameObject>();

    int[] x = new int[5];
    [ContextMenu("CheckTotalGem")]
    void CheckTotalGem()
    {
        for (int i = 0; i < 5; i++)
        {
            x[i] = 0;
        }
        for (int i = 0; i < countCollumn; i++)
        {
            for (int j = 0; j < countRow; j++)
            {
                Gem _gem = arrGem[i][j].GetComponent<Gem>();
                if (_gem.inDex == 0)
                {
                    x[0] += 1;
                }
                if (_gem.inDex == 1)
                {
                    x[1] += 1;
                }
                if (_gem.inDex == 2)
                {
                    x[2] += 1;
                }
                if (_gem.inDex == 3)
                {
                    x[3] += 1;
                }
                if (_gem.inDex == 4)
                {
                    x[4] += 1;
                }
            }
        }
    }

    [ContextMenu("Test")]
    void Test()
    {
        CheckTotalGem();
        for (int i = 0; i < 5; i++)
        {
            Debug.Log(System.String.Format("Gem[{0}] = {1}", i, totalGemColor[i]));
            Debug.Log(System.String.Format("Gem Total[{0}] = {1}", i, x[i]));
        }
    }
    void Xoa()
    {
        for (int i = 0; i < listConect.Count; i++)
        {
            DespawnGem(listConect[i].transform, "conect");
        }
        //xoa cac cuc        
        if (ListDelete.Count >= 3)
        {
            uiController.ResetFillAmount();
            Gem _gem = ListDelete[0].GetComponent<Gem>();
            uiController.totalDelete[_gem.inDex] += ListDelete.Count;

            //uiController.RandomSpecial(uiController.totalDelete[_gem.inDex], _gem.inDex);


            //xoa cac Gem trong listDelete
            for (int i = 0; i < ListDelete.Count; i++)
            {
                Gem _gemDel = ListDelete[i].GetComponent<Gem>();

                if (_gemDel.cucDacBiet == true)
                {
                    for (int m = 0; m < listDacBiet.Count; m++)
                    {
                        listDacBiet[m].GetComponent<Gem>().Test(ListDelete[i]);
                    }
                }
                if (uiController.isGameOver == false)
                    SetSoundDelete(ListDelete[0].GetComponent<Gem>().inDex);
                Gem gem = ListDelete[i].GetComponent<Gem>();
                arrGem[gem.row][gem.collumn] = null;
                //score += 10;
                AddScore(ListDelete);
                DespawnGem(ListDelete[i].transform, "gem");
            }
        }
        ListDelete.Clear();
        listDeleteNen.Clear();
        listConect.Clear();
        listMouse.Clear();
        activeDestroyGem = true;
        //GameOver();

        if (activeAddtime == true)
        {
            InstantiateTimeStar();
        }

    }
    void AddScore(List<GameObject> _listDelete)
    {
        int x2 = 1;
        if (uiController.countCombo == 0)
        {
            x2 = 1;
        }
        else
        {
            x2 = uiController.countCombo;
        }
        //Debug.Log("X2  = " + x2);
        if (_listDelete.Count <= 5)
        {
            score += (int)(10 * expScore) * x2;
        }
        if (_listDelete.Count > 5 && _listDelete.Count <= 10)
        {
            score += (int)((10 + (10f * 0.2f)) * expScore) * x2;
        }
        if (_listDelete.Count > 10 && _listDelete.Count <= 15)
        {
            score += (int)((10 + (10f * 0.5f)) * expScore) * x2;
        }
        if (_listDelete.Count > 15)
        {
            score += (int)((10 + (10f * 0.7f)) * expScore) * x2;
        }

    }
    void SetSoundDelete(int index)
    {
        if (index == 0)
        {
            AudioController.Instance.PlaySound(AudioType.REMOVE_LIGHT);
        }
        if (index == 1)
        {
            AudioController.Instance.PlaySound(AudioType.REMOVE_SOIL);
        }
        if (index == 2)
        {
            AudioController.Instance.PlaySound(AudioType.REMOVE_TRASH);
        }
        if (index == 3)
        {
            AudioController.Instance.PlaySound(AudioType.REMOVE_WATER);
        }
        if (index == 4)
        {
            AudioController.Instance.PlaySound(AudioType.REMOVE_WORM);
        }
    }
    Vector2 GetPositionTouch(PointerEventData eventData)
    {
        Vector2 pointerPostion = eventData.position;//ClampToWindow(eventData);
        Vector2 localPointerPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, pointerPostion, eventData.pressEventCamera, out localPointerPosition);
        return localPointerPosition;
    }
    int GetIndexGemX(Vector2 pos)
    {

        int indexX = (int)((pos.x + GemWitdh / 2) / (GemWitdh + disX) + 3.0f);
        return indexX;
    }
    int GetIndexGemY(Vector2 pos)
    {
        int indexY = (int)((pos.y + GemHeight / 2 + disY) / (GemHeight + disY) + 3.5f);

        return indexY;
    }
    float KiemTraKhoangCach(GameObject a, GameObject b)
    {
        float khoangCach = Vector3.Distance(a.transform.position, b.transform.position);
        return khoangCach;
    }

    //hàm bắt đầu kéo
    public void OnBeginDrag(PointerEventData eventData)
    {
        FinishTutorial.Instance.Finish();
        listItween.Clear();
        listMove.Clear();
        listDeleteNen.Clear();
        Vector2 pos = GetPositionTouch(eventData);//lấy vị trí Touch
        //trả về số hàng và số cột
        int i = GetIndexGemX(pos);
        int j = GetIndexGemY(pos);
        if (i < 0 || j < 0 || i > countCollumn - 1 || j > countRow - 1)
        {
            return;
        }
        activeTimeHelp = false;
        listDacBiet.Clear();
        if (arrGem[i][j] == null)
        {
            return;
        }
        //kiểm tra trong List Delete chưa có thì Add vào List
        if (ListDelete.Count == 0 && arrGem[i][j].GetComponent<Gem>().cucDacBiet == false)
        {
            ListDelete.Add(arrGem[i][j]);
            listItween.Add(arrGem[i][j]);
            listMouse.Add(arrGem[i][j]);
            if (!listMove.Contains(arrGem[i][j]))
            {
                listMove.Add(arrGem[i][j]);
            }

            listDeleteNen.Add(arrGem[i][j]);
            
            ListDelete[ListDelete.Count - 1].GetComponent<Gem>().ChangSprite();
        }

    }
    public GameObject number;
    //Hàm Kết thúc kéo 
    public void OnEndDrag(PointerEventData eventData)
    {
        uiController.countDelete = 0;
        //xóa các Conect trên màn hình
        for (int i = 0; i < listConect.Count; i++)
        {
            DespawnGem(listConect[i].transform, "conect");
        }
        listConect.Clear();//xóa hết các Conect
        //nếu List Delete == 1 hoặc == 2 thì không làm gì
        if (ListDelete.Count == 1)
        {
            ListDelete[0].GetComponent<Gem>().ResetSprite();
            ListDelete.Clear();
            listDeleteNen.Clear();
            listConect.Clear();
            listMouse.Clear();
            listDacBiet.Clear();
            if (listHieuUng.Count != 0)
            {
                Destroy(listHieuUng[0]);
            }
            listHieuUng.Clear();
        }
        if (ListDelete.Count == 2)
        {
            if (ListDelete[1].GetComponent<Gem>().cucDacBiet == true)
            {
                int a = ListDelete[1].gameObject.GetComponent<Gem>().PosX();
                int b = ListDelete[1].gameObject.GetComponent<Gem>().PosY();
                ResetCucDacBiet(a, b);
            }
            ListDelete[0].GetComponent<Gem>().ResetSprite();
            ListDelete[1].GetComponent<Gem>().ResetSprite();
            ListDelete.Clear();
            listDeleteNen.Clear();
            listConect.Clear();
            listMouse.Clear();
            listDacBiet.Clear();
            if (listHieuUng.Count != 0)
            {
                Destroy(listHieuUng[0]);
            }
            listHieuUng.Clear();
        }
        //nếu List Conect lớn hơn 3 thì cho nổ các cục Gem
        if (ListDelete.Count >= 3)
        {
            Vector3 posNumber = new Vector3(ListDelete[ListDelete.Count - 1].transform.position.x, ListDelete[ListDelete.Count - 1].transform.position.y, -1);

            GameObject n = Instantiate(number, posNumber, Quaternion.identity) as GameObject;
            NumberDrag nDrag = n.GetComponent<NumberDrag>();
            if (nDrag != null)
            {
                nDrag.number = ListDelete.Count;
                nDrag.Calculogic();
            }

            uiController.countGem = ListDelete.Count;
            uiController.CheckCombo();
            uiController.Combo();

            Gem _g = ListDelete[1].GetComponent<Gem>();
            if (_g == null)
            {
                return;
            }
            //kiem tra xem cac cuc dac biet co o trong listDelete khong
            for (int i = 0; i < ListDelete.Count; i++)
            {
                Gem _gemDacBiet = ListDelete[i].GetComponent<Gem>();
                if (_gemDacBiet.timeAdd == true)
                {
                    //GameObject start = ListDelete[i].transform.GetChild(0).gameObject;
                    Gem _gemStart = ListDelete[i].GetComponentInChildren<Gem>();
                    _gemStart.gameObject.transform.SetParent(gemContainer);
                    if (_gemStart != null)
                    {
                        _gemStart.MoveItween(new Vector3(0, 600, 0), 0.5f);
                        //Debug.Log("Thoi gian duoc cong la = " + _gemDacBiet.addTimeGame);
                        uiController.AddTime(_gemDacBiet.addTimeGame);//cong thoi gian Game
                    }
                }
                if (_gemDacBiet.destroyCollum == true)
                {
                    NoTheoChieuNgang(_gemDacBiet.row);
                }
                if (_gemDacBiet.destroyRow == true)
                {
                    NoTheoChieuDoc(_gemDacBiet.collumn);
                }
            }
            for (int i = 0; i < ListDelete.Count; i++)
            {
                Vector3 pos = new Vector3(ListDelete[i].transform.localPosition.x, ListDelete[i].transform.localPosition.y, 0);
                if (uiController.isGameOver == false)
                {
                    GameObject _destroy = Instantiate(destroyGem, Vector3.one, Quaternion.identity) as GameObject;
                    _destroy.transform.SetParent(gemContainer);
                    _destroy.transform.localScale = new Vector3(75, 75, 0);
                    _destroy.transform.localPosition = pos;
                }
            }

            for (int i = 0; i < ListDelete.Count; i++)
            {
                MoveItween(ListDelete[i], transfGemOut.localPosition, 0.4f);

            }
            
        }
        activeTimeHelp = true;
        yyy = true;
        
        listMove.Clear();
        
    }
#endregion
	#region Render
    void RenderSpecial()
    {

        xxx = 0;
        yyy = false;
        if (uiController.isGameOver == false)
        {
            for (int i = 0; i < listHieuUng.Count; i++)
            {
                HieuUng hieuUng = listHieuUng[i].GetComponent<HieuUng>();
                indexRandom = hieuUng.RenderSpecial();
                InstantiateItemDacBiet();
                if (_indexX != -1 && _indexY != -1)
                {
                    hieuUng.MoveItween(arrGem[_indexX][_indexY].transform.position, 2f);
                }

            }
        }
        listHieuUng.Clear();

    }
	
    private GameObject transfHieuUng;

    GameObject GetArrGem(int x, int y)
    {
        return arrGem[x][y];
    }
    int _indexX = -1;
    int _indexY = -1;
    void SetIndexX(int index)
    {
        this._indexX = index;
    }
    void SetIndexY(int index)
    {
        this._indexY = index;
    }
    public RectTransform transfGemOut;
    #endregion
    //hàm khi đang kéo
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = GetPositionTouch(eventData);//lấy vị trí kéo
        int x = GetIndexGemX(pos);
        int y = GetIndexGemY(pos);
        if (x < 0 || y < 0 || x > countCollumn - 1 || y > countRow - 1)
        {
            return;
        }
        if (ListDelete == null)
        {
            return;
        }

        if (ListDelete.Count <= 0 || ListDelete[0] == null)
        {
            return;
        }
        //nếu cục kéo tới tồn tại
        if (arrGem[x][y] != null)
        {
            Gem gem = arrGem[x][y].GetComponent<Gem>();
            if (gem.inDex == ListDelete[0].GetComponent<Gem>().inDex && KiemTraKhoangCach(arrGem[x][y], ListDelete[ListDelete.Count - 1]) <= 1.2f)//kiem tra de dua vao listDelete
            {
                
                if (!listMove.Contains(arrGem[x][y]))
                {
                    listMove.Add(arrGem[x][y]);
                }
                if (!listMouse.Contains(arrGem[x][y]))
                {
                    listMouse.Add(arrGem[x][y]);
                }
                if (!ListDelete.Contains(arrGem[x][y]) && ListDelete.Count >= 1)//kiem tra xem doi tuong chon da co trong ListDelete chua
                {
                    

                    InstantiateConect(ListDelete[ListDelete.Count - 1], arrGem[x][y]);//xuat ket noi ra man hinh

                    //neu cuc keo duoc chua 1 trong so cac cuc dac biet se bao cho nguoi choi biet
                    if (gem.isSpecial || gem.isSpecialCollum || gem.isSpecialRow)
                    {
                        GameObject _hieuUngObj = Instantiate(hieuUng, arrGem[x][y].transform.position, Quaternion.identity) as GameObject;

                        HieuUng _hieuUng = _hieuUngObj.GetComponent<HieuUng>();
                        if (_hieuUng != null)
                        {
                            if (gem.isSpecial)
                            {
                                _hieuUng.isSpecial = true;
                            }
                            if (gem.isSpecialCollum)
                            {
                                _hieuUng.isSpecialCollum = true;
                            }
                            if (gem.isSpecialRow)
                            {
                                _hieuUng.isSpecialRow = true;
                            }
                        }
                        listHieuUng.Add(_hieuUngObj);
                    }
                    if (!ListDelete.Contains(arrGem[x][y]))
                        ListDelete.Add(arrGem[x][y]);
                    
                    

                    listItween.Add(arrGem[x][y]);
                    SetSoundDrag(ListDelete);

                    if (ListDelete[ListDelete.Count - 1] != null)
                    {
                        ListDelete[ListDelete.Count - 1].GetComponent<Gem>().ChangSprite();
                    }
                    if (ListDelete.Contains(arrGem[x][y]) && arrGem[x][y].GetComponent<Gem>().cucDacBiet == true)
                    {
                        int a = arrGem[x][y].GetComponent<Gem>().PosX();
                        int b = arrGem[x][y].GetComponent<Gem>().PosY();
                        CucDacBiet(a, b);
                    }
                }
                if (ListDelete.Count >= 2)
                {
                    if (arrGem[x][y] == ListDelete[ListDelete.Count - 2] && listConect.Count >= 1)//neu nguoi choi quay lai cuc phia trc co
                    {
                        Gem _gem = ListDelete[ListDelete.Count - 1].GetComponent<Gem>();
                        if (_gem.isSpecial || _gem.isSpecialCollum || _gem.isSpecialRow)
                        {
                            Destroy(listHieuUng[listHieuUng.Count - 1]);
                            listHieuUng.Remove(listHieuUng[listHieuUng.Count - 1]);
                        }
                        //listItween.Add(ListDelete[ListDelete.Count - 1]);
                        _gem.ResetSprite();
                        listDeleteNen.Remove(ListDelete[ListDelete.Count - 1]);
                        ListDelete.RemoveAt(ListDelete.Count - 1);
                        Destroy(listConect[listConect.Count - 1]);
                        listConect.RemoveAt(listConect.Count - 1);
                        
                        SetSoundDrag(ListDelete);

                    }
                }
                for (int i = 0; i < listMouse.Count; i++)
                {
                    if (listMouse[i].GetComponent<Gem>().cucDacBiet == true)
                    {
                        GameObject m = listMouse[i];
                        if (!ListDelete.Contains(listMouse[i]))
                        {
                            int a = m.gameObject.GetComponent<Gem>().PosX();
                            int b = m.gameObject.GetComponent<Gem>().PosY();
                            ResetCucDacBiet(a, b);
                        }
                    }
                }
            }
        }
        listDeleteNen.Clear();
        for (int i = 0; i < ListDelete.Count;i++ )
        {
            if(!listDeleteNen.Contains(ListDelete[i]))
            {
                listDeleteNen.Add(ListDelete[i]);
            }
        }  
        for (int i = 0; i < listDeleteNen.Count; i++)
        {
            Gem g = listDeleteNen[i].GetComponent<Gem>();
            if (g != null)
            {
                if (g.destroyCollum == true)
                {
                    AddRow(g.row);
                    break;
                }
                if (g.destroyRow == true)
                {
                    AddCollumn(g.collumn);
                    break;
                }
            }
        }
        //MoveSpecial(listMouse, ListDelete[ListDelete.Count - 1]);
        Move(listMove, ListDelete[ListDelete.Count - 1]);
        uiController.countDelete = ListDelete.Count;
    }
    #region
    public void MoveItween(GameObject obj, Vector3 pos, float movetime)
    {
        iTween.MoveTo(obj, iTween.Hash(
            iT.MoveTo.position, pos,//toi vi tri cuoi
            iT.MoveTo.islocal, true,
            iT.MoveTo.time, movetime,//thoi gian
            iT.MoveTo.oncomplete, "Xoa",//goi den ham Xoa
            iT.MoveTo.oncompletetarget, gameObject
            ));
    }

    //hàm âm thanh
    void SetSoundDrag(List<GameObject> listObj)
    {
        if (listObj.Count == 2)
        {
            AudioController.Instance.PlaySound(AudioType.SG_MERGER1);
        }
        if (listObj.Count == 3)
        {
            AudioController.Instance.PlaySound(AudioType.SG_MERGER2);
        }
        if (listObj.Count == 4)
        {
            AudioController.Instance.PlaySound(AudioType.SG_MERGER3);
        }
        if (listObj.Count == 5)
        {
            AudioController.Instance.PlaySound(AudioType.SG_MERGER4);
        }
        if (listObj.Count == 6)
        {
            AudioController.Instance.PlaySound(AudioType.SG_MERGER5);
        }
        if (listObj.Count == 7)
        {
            AudioController.Instance.PlaySound(AudioType.SG_MERGER6);
        }
        if (listObj.Count == 8)
        {
            AudioController.Instance.PlaySound(AudioType.SG_MERGER7);
        }
        if (listObj.Count == 9)
        {
            AudioController.Instance.PlaySound(AudioType.SG_MERGER8);
        }
        if (listObj.Count >= 10)
        {
            AudioController.Instance.PlaySound(AudioType.SG_MERGER9);
        }
    }
    void NoTheoChieuNgang(int vitri)
    {
        for (int i = 0; i < countRow; i++)
        {
            if (!ListDelete.Contains(arrGem[vitri][i]))
            {
                if (arrGem[vitri][i] != null)
                {
                    ListDelete.Add(arrGem[vitri][i]);
                    listDeleteNen.Add(arrGem[vitri][i]);
                }
            }

        }
    }
    void AddRow(int vitri)
    {
        for (int i = 0; i < countRow; i++)
        {
            if (arrGem[vitri][i] != null)
            {
                if (!ListDelete.Contains(arrGem[vitri][i]))
                {
                    listDeleteNen.Add(arrGem[vitri][i]);
                }
            }

        }
    }
    void AddCollumn(int vitri)
    {
        for (int i = 0; i < countCollumn; i++)
        {
            if (arrGem[i][vitri] != null)
            {
                if (!ListDelete.Contains(arrGem[i][vitri]))
                {
                    listDeleteNen.Add(arrGem[i][vitri]);
                }
            }
        }
    }
    
    //no cac cuc theo chieu ngang
    void NoTheoChieuDoc(int vitri)
    {
        for (int i = 0; i < countCollumn; i++)
        {
            if (!ListDelete.Contains(arrGem[i][vitri]))
            {
                if (arrGem[i][vitri] != null)
                {
                    ListDelete.Add(arrGem[i][vitri]);
                }
            }
        }
    }
    void CucDacBiet(int i, int j)
    {
        listDacBiet.Clear();
        for (int m = i - 1; m <= i + 1; m++)
        {
            for (int n = j - 1; n <= j + 1; n++)
            {
                if (m >= 0 && n >= 0 && m < countCollumn && n < countRow && arrGem[m][n] != arrGem[i][j])
                {
                    //arrGem[m][n].GetComponent<Gem>().ChangSpriteDacBiet(arrGem[i][j]);  
                    if (!ListDelete.Contains(arrGem[m][n]))
                    {
                        arrGem[m][n].GetComponent<Gem>().ChangSpriteDacBiet(arrGem[i][j]);

                        if (arrGem[m][n].GetComponent<Gem>().cucDacBiet == false)
                            listDacBiet.Add(arrGem[m][n]);
                    }

                }
            }
        }
    }
    void ResetCucDacBiet(int i, int j)
    {
        for (int m = i - 1; m <= i + 1; m++)
        {
            for (int n = j - 1; n <= j + 1; n++)
            {
                if (m >= 0 && n >= 0 && m < countCollumn && n < countRow)
                {
                    //arrGem[m][n].GetComponent<Gem>().ResetSpriteDacBiet(arrGem[m][n]);
                    if (!ListDelete.Contains(arrGem[m][n]))
                    {
                        //SubTotalGem(1, arrGem[m][n].GetComponent<Gem>().inDex);
                        arrGem[m][n].GetComponent<Gem>().ResetSpriteDacBiet(arrGem[m][n]);
                    }

                }
            }
        }
    }
    void DiChuyenCacCuc(int m, int n)
    {
        if (arrGem[m][n] == null)
        {
            if (arrGem[m][n + 1] != null)
            {
                Vector3 pos = new Vector3((m - 3.0f) * (GemWitdh + disX), (n - 3.5f) * (GemHeight + disY), 0);
                Gem _gem = arrGem[m][n + 1].GetComponent<Gem>();
                _gem.GetComponent<Gem>().collumn -= 1;
                _gem.MovePosition(pos, 0.5f);

                arrGem[m][n] = arrGem[m][n + 1];
                arrGem[m][n + 1] = null;
            }
        }
    }
    float delayInstance;
    void CacCucRoiXuong()
    {
        for (int i = 0; i < countCollumn; i++)
        {
            if (arrGem[i][countRow - 1] == null)
            {
                if (delayInstance > 0.05)
                {
                    InstantiateGem(i, countRow - 1, iTwenPos);
                    delayInstance = 0;
                }
                delayInstance += Time.deltaTime;
            }
            for (int j = 0; j < countRow - 1; j++)
            {
                DiChuyenCacCuc(i, j);
            }
        }
        CheckListInvalid();
        SetTotalGem();
    }
    List<GameObject> LoangDau(List<GameObject> list, int i, int j)
    {

        for (int b = j - 1; b <= j + 1; b++)
        {
            for (int a = i - 1; a <= i + 1; a++)
            {
                if (a >= 0 && b >= 0 && a <= countCollumn - 1 && b <= countRow - 1)
                {
                    if (arrGem[i][j].GetComponent<Gem>().inDex != null && arrGem[a][b] != null)
                    {
                        if (arrGem[i][j].GetComponent<Gem>().inDex == arrGem[a][b].GetComponent<Gem>().inDex && arrGem[a][b].GetComponent<Gem>().check == false)
                        {
                            //Debug.Log(arrGem[i][j]);
                            arrGem[i][j].GetComponent<Gem>().check = true;
                            arrGem[a][b].GetComponent<Gem>().check = true;

                            if (!list.Contains(arrGem[i][j]))
                            {
                                list.Add(arrGem[i][j]);
                            }
                            if (!list.Contains(arrGem[a][b]))
                            {
                                list.Add(arrGem[a][b]);
                            }
                            LoangDau(list, a, b);
                        }
                    }
                }

            }
        }
        return list;
    }
    void ResetCheckGem()
    {
        for (int i = 0; i < countCollumn; i++)
        {
            for (int j = 0; j < countRow; j++)
            {
                if (arrGem[i][j] != null)
                {
                    arrGem[i][j].GetComponent<Gem>().check = false;

                }
            }
        }

    }
    public void CheckListInvalid()
    {
        listLoangDau.Clear();
        for (int j = 0; j < countRow; j++)
        {
            for (int i = 0; i < countCollumn; i++)
            {
                if (arrGem[i][j] != null)
                {
                    List<GameObject> list = new List<GameObject>();
                    LoangDau(list, i, j);
                    if (list.Count >= 3)
                    {
                        listLoangDau.Add(list);
                    }
                }
            }
        }
        ResetCheckGem();
    }
    void ScaleGem()
    {
        for (int i = 0; i < 3; i++)
        {
            if (listLoangDau.Count > 0)
            {
                if (listLoangDau[0][i] != null)
                    listLoangDau[0][i].transform.localScale = new Vector3(localScale, localScale, 1);
            }
        }
        boolScale = true;
    }
    void ResetScaleGem()
    {
        if (boolScale == true)
        {
            for (int i = 0; i < 3; i++)
            {
                if (listLoangDau.Count > 0)
                {
                    if (listLoangDau[0][i] != null)
                    {
                        listLoangDau[0][i].transform.localScale = new Vector3(1, 1, 1);
                    }
                }
            }
            boolScale = false;
        }
    }
    void InstantiateItemDacBiet()
    {

        //int indexRandom;
        int vitriX = Random.Range(0, countCollumn - 1);
        int vitriY = Random.Range(0, countRow - 1);
        if (arrGem[vitriX][vitriY] == null)
        {
            return;
        }
        if (arrGem[vitriX][vitriY] != null && arrGem[vitriX][vitriY].GetComponent<Gem>().cucDacBiet != true && !ListDelete.Contains(arrGem[vitriX][vitriY]))
        {
            if (indexRandom == 0)
            {
                arrGem[vitriX][vitriY].GetComponent<Gem>().destroyCollum = true;
                GameObject dacbiet = Instantiate(cucDacBiet[0], arrGem[vitriX][vitriY].transform.position, Quaternion.identity) as GameObject;
                dacbiet.transform.parent = arrGem[vitriX][vitriY].transform;
                dacbiet.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                SetIndexX(vitriX);
                SetIndexY(vitriY);
            }
            if (indexRandom == 1)
            {
                arrGem[vitriX][vitriY].GetComponent<Gem>().destroyRow = true;
                GameObject dacbiet = Instantiate(cucDacBiet[1], arrGem[vitriX][vitriY].transform.position, Quaternion.identity) as GameObject;
                dacbiet.transform.parent = arrGem[vitriX][vitriY].transform;
                dacbiet.transform.localScale = new Vector3(0.5f, 0.5f, 1);
                //transfHieuUng = arrGem[vitriX][vitriY];
                SetIndexX(vitriX);
                SetIndexY(vitriY);
            }
            if (indexRandom == 2)
            {
                arrGem[vitriX][vitriY].GetComponent<Gem>().cucDacBiet = true;
                GameObject dacbiet = Instantiate(cucDacBiet[2], arrGem[vitriX][vitriY].transform.position, Quaternion.identity) as GameObject;
                dacbiet.transform.parent = arrGem[vitriX][vitriY].transform;
                dacbiet.transform.localScale = Vector3.one;
                //transfHieuUng = arrGem[vitriX][vitriY];
                SetIndexX(vitriX);
                SetIndexY(vitriY);
            }


        }

    }
    public void RandomMap()
    {
        for (int i = 0; i < 5; i++)
        {
            totalGemColor[i] = 0;
        }
        for (int i = 0; i < countCollumn; i++)
        {
            for (int j = 0; j < countRow; j++)
            {
                if (arrGem[i][j] != null)
                {
                    DespawnGem(arrGem[i][j].gameObject.transform, "gem");

                }
                InstantiateGem(i, j, 200);//in ra cac Object o vi tri PosIT  
            }
        }
        ReStart();

        CheckListInvalid();
    }
    void ReStart()
    {
        score = 0;
        help = 0;
        activeAddtime = false;
        boolScale = false;
        activeAddtime = false;
        activeTimeHelp = true;
        activeHelp = false;
        activeDestroyGem = false;
        xxx = 0;
        yyy = false;
        for (int i = 0; i < listConect.Count; i++)
        {
            Destroy(listConect[i]);
        }
        for (int i = 0; i < listHieuUng.Count; i++)
        {
            Destroy(listHieuUng[i]);
        }
        ListDelete.Clear();
        listMouse.Clear();
        listDacBiet.Clear();
        listConect.Clear();
        listItween.Clear();
        listHieuUng.Clear();
        listLoangDau.Clear();

    }
    #endregion


    public List<GameObject> listDeleteNen = new List<GameObject>();
    
    private List<GameObject> listMove = new List<GameObject>();
    void Move(List<GameObject> listMouse, GameObject obj)
    {
        bool isColumn = false;
        bool isRow = false;
        for (int i = 0; i < listMouse.Count; i++)
        {
            Gem _gem = listMouse[i].GetComponent<Gem>();
            if (_gem.destroyCollum == true)
            {
                isColumn = true;
            }
            if (_gem.destroyRow == true)
            {
                isRow = true;
            }
        }       
        for(int i = 0; i < listMouse.Count; i++)
        {
            Gem gem = listMouse[i].GetComponent<Gem>();
            if(gem != null)
            {
                if (gem.listChil.Count > 0)
                {
                    for (int j = 0; j < gem.listChil.Count; j++)
                    {
                        if (gem.listChil[j].gameObject != null)
                        {
                            gem.listChil[j].transform.SetParent(obj.transform);
                            Vector3 pos = new Vector3(obj.transform.position.x, obj.transform.position.y, 0);
                            gem.listChil[j].transform.localPosition = pos;
                            gem.listChil[j].transform.localScale = new Vector3(0.5f, 0.5f, 1);
                        }
                    }

                    gem.destroyRow = false;
                    gem.destroyCollum = false;

                    Gem gemObj = obj.GetComponent<Gem>();
                    if(gemObj != null)
                    {
                        gemObj.destroyCollum = isColumn;
                        gemObj.destroyRow = isRow;
                    }
                }
            }
        }
  
    }

    private float totalScore = 0;
    private float expScore = 1;

    private const float exp = 10000000.0f;
    public void ExpScore()
    {
        totalScore = PlayerPrefs.GetFloat("Total") + score;

        PlayerPrefs.SetFloat("Total", totalScore);
        PlayerPrefs.Save();//Lưu Hight Score

        float triso = PlayerPrefs.GetFloat("Total") / exp;
        expScore += triso;
    }
    

    
}
