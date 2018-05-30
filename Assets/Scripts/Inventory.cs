using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    private static Inventory _instance;
    public static Inventory Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("Inventory").GetComponent<Inventory>();
            return _instance;
        }
    }

    private TweenPosition tween;

    public List<InventoryItemGrid> itemGridList = new List<InventoryItemGrid>();
    public GameObject inventoryItem;

    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
    }

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GetId(Random.Range(1001, 1003));
        }
    }

    /// <summary>
    /// 拾取到id的物品，并添加到物品栏里面
    /// 处理拾取物品的功能
    /// </summary>
    /// <param name="id"></param>
    public void GetId(int id)
    {
        //第一步 在所有的物品中是否存在该物品
        //第二 若存在，则物品num+1
        //第三 若不存在，查找空的方格，然后把新创建的inventoryItem放到这个空的方格里面
        InventoryItemGrid grid = null;
        foreach (InventoryItemGrid temp in itemGridList)
        {
            if (temp.id == id)
            {
                grid = temp;
                break;
            }
        }
        //存在的情况
        if (grid != null)
        {
            grid.PlusNum();
        }
        //不存在的情况
        else
        {
            foreach (InventoryItemGrid temp in itemGridList)
            {
                if (temp.id == 0)
                {
                    grid = temp;
                    break;
                }
            }
            //第三 若不存在，查找空的方格，然后把新创建的inventoryItem放到这个空的方格里面
            if (grid != null)
            {
                GameObject itemGo = NGUITools.AddChild(grid.gameObject, inventoryItem);
                itemGo.transform.localPosition = Vector3.zero;
                itemGo.GetComponent<UISprite>().depth = 9;
                grid.SetId(id);
            }
        }
    }

    private bool isShow = false;
    //显示背包
    private void Show()
    {
        isShow = true;
        tween.PlayForward();
    }
    //隐藏背包
    private void Hide()
    {
        tween.PlayReverse();
        isShow = false;
    }
    //状态转变
    public void TransformState()
    {
        if(isShow==false)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
}
