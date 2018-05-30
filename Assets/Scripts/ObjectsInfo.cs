using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsInfo : MonoBehaviour {
    
    private static ObjectsInfo _instance;
    public static ObjectsInfo Instance
    {
        get
        {
            if (_instance == null)
                _instance = GameObject.Find("GameSetting").GetComponent<ObjectsInfo>();
            return _instance;
        }
    }

    private Dictionary<int, ObjectInfo> objectaInfoDict = new Dictionary<int, ObjectInfo>();

    public TextAsset objectsIfoListText;

    void Awake()
    {
        _instance = this;
        ReadText();
        //print(objectaInfoDict.Keys.Count);
    }

    public ObjectInfo GetObjectInfoById(int id)
    {
        ObjectInfo info = null;
        objectaInfoDict.TryGetValue(id, out info);
        return info;
    }

    //读取文本信息
    void ReadText()
    {
        string text = objectsIfoListText.text;
        string[] strArray = text.Split('\n');

        foreach (string str in strArray)
        {
            string[] proArray = str.Split(',');
            
            ObjectInfo info = new ObjectInfo();
            int id = int.Parse(proArray[0]);
            string name = proArray[1];
            string icon_name = proArray[2];
            string str_type = proArray[3];
            ObjectType type = ObjectType.Food;
            switch(str_type)
            {
                case "Food":
                    type = ObjectType.Food;
                    break;
                case "Material":
                    type = ObjectType.Material;
                    break;
                case "Tool":
                    type = ObjectType.Tool;
                    break;
                case "Property":
                    type = ObjectType.Property;
                    break;
                case "Task":
                    type = ObjectType.Task;
                    break;
            }

            info.id = id;info.name = name;info.icon_name = icon_name;info.type = type;
            
            if(type==ObjectType.Food)
            {
                int hp = int.Parse(proArray[4]);
                int mp = int.Parse(proArray[5]);
                int price_sell = int.Parse(proArray[6]);
                int price_buy = int.Parse(proArray[7]);
                info.hp = hp;info.mp = mp;info.price_sell = price_sell;info.price_buy = price_buy;
            }

            objectaInfoDict.Add(id, info);    //添加到字典中，id作为key，物品信息作为value


        }

    }
}

//物品种类
//         食品 Food(1000)
//         材料 Material(2000)
//         工具 Tool(3000)
//         道具 Property(4000)
//         任务 Task(5000）
//     物品属性分析
//      id 名称 icon名称 属性（药品） 加血量值 加魔法值 出售价 购买价

//物品种类
public enum ObjectType
{
    Food,
    Material,
    Tool,
    Property,
    Task
}

//一条物品信息
public class ObjectInfo
{
    public int id;
    public string name;
    public string icon_name;       //图集中的物品名称
    public ObjectType type;
    public int hp;
    public int mp;
    public int price_sell;
    public int price_buy;
}
