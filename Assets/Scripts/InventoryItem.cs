using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : UIDragDropItem {

    private UISprite sprite;
    private int id;

    protected override void Awake()
    {
        base.Awake();
        sprite = this.GetComponent<UISprite>();

    }

    protected override void Update()
    {
        base.Update();
        if(isHover)
        {
            InventoryDes.Instance.Show(id);
        }
    }

    //拖拽结束时调用
    protected override void OnDragDropRelease(GameObject surface)
    {
        base.OnDragDropRelease(surface);
        if(surface!=null)
        {
            //拖放到了空的格子里
            if (surface.tag == Tags.inventory_item_grid)
            {
                //拖放到了自己的格子里
                if(surface==this.transform.parent.gameObject)
                {

                }
                //拖放到了其它空的格子里
                else
                {
                    InventoryItemGrid oldParent = this.transform.parent.GetComponent<InventoryItemGrid>();

                    this.transform.parent = surface.transform;
                    InventoryItemGrid newParent = surface.GetComponent<InventoryItemGrid>();
                    newParent.SetId(oldParent.id, oldParent.num);

                    oldParent.ClearInfo();
                }
            }
            //拖放到了有物品的格子里
            else if(surface.tag==Tags.inventory_item)
            {
                InventoryItemGrid thisParent = this.transform.parent.GetComponent<InventoryItemGrid>();
                InventoryItemGrid surgaceParent = surface.transform.parent.GetComponent<InventoryItemGrid>();
                int id = thisParent.id;int num = thisParent.num;
                thisParent.SetId(surgaceParent.id, surgaceParent.num);
                surgaceParent.SetId(id, num);
            }
        }
        ResetPosition();

    }
    //重置位置
    void ResetPosition()
    {
        transform.localPosition = Vector3.zero;
    }

    //根据ID获取物品信息
    public void SetId(int id)
    {
        ObjectInfo info = ObjectsInfo.Instance.GetObjectInfoById(id);
        sprite.spriteName = info.icon_name;
    }

    public void SetIconName(int id, string icon_name)
    {
        sprite.spriteName = icon_name;
        this.id = id;
    }

    private bool isHover = false;
    //鼠标移到物体上
    public void OnHoverOver()
    {
        isHover = true;
    }
    //鼠标移除物体
    public void OnHoverOut()
    {
        isHover = false;
    }

}
