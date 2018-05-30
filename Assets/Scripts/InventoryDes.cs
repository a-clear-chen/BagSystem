using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDes : MonoBehaviour {

    private static InventoryDes _instance;
    public static InventoryDes Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("InventoryDes").GetComponent<InventoryDes>();
            return _instance;
        }
    }

    private UILabel label;
    private float timer = 0;

    void Awake()
    {
        _instance = this;
    }

    // Use this for initialization
    void Start() {
        label = this.GetComponentInChildren<UILabel>();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update() {

        if(this.gameObject.activeInHierarchy==true)
        {
            this.timer -= Time.deltaTime;
            if(this.timer<=0)
            {
                this.gameObject.SetActive(false);

            }
        }

    }
    //显示物品信息提示框
    public void Show(int id)
    {
        transform.position = UICamera.currentCamera.ScreenToWorldPoint(Input.mousePosition);
        this.gameObject.SetActive(true);
        this.timer = 0.1f;

        ObjectInfo info = ObjectsInfo.Instance.GetObjectInfoById(id);

        string des = "";
        switch(info.type)
        {
            case ObjectType.Food:
                des = GetFoodDes(info);
                break;
        }
        label.text = des;
        
    }

    private string GetFoodDes(ObjectInfo info)
    {
        string str = "";
        print(info.name);
        str += "名称:" + info.name + "\n";
        str += "+HP:" + info.hp + "\n";
        str += "+MP:" + info.mp + "\n";
        str += "出售价:" + info.price_sell + "\n";
        str += "购买价:" + info.price_buy + "\n";
        return str;
    }
}
