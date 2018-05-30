using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemGrid : MonoBehaviour {

    public int id = 0;
    public int num = 0;    //物品数量

    private UILabel numLabel;
    private ObjectInfo info = null;

	// Use this for initialization
	void Start () {
        numLabel = this.GetComponentInChildren<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //物品数量的增加
    public void PlusNum(int num=1)
    {
        this.num += num;
        numLabel.text = this.num.ToString();
    }

    public void SetId(int id,int num=1)
    {
        info = ObjectsInfo.Instance.GetObjectInfoById(id);
        InventoryItem item = this.GetComponentInChildren<InventoryItem>();
        item.SetIconName(id, info.icon_name);
        numLabel.enabled = true;
        this.num = num;
        this.id = id;
        numLabel.text = num.ToString();
    }
    //清空格子存的物品信息
    public void ClearInfo()
    {
        id = 0;
        num = 0;
        info = null;
        numLabel.enabled = false;
    }
}
