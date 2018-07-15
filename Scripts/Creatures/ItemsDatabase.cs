using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public static class ItemsDatabase {

    public static Dictionary<int, ItemData> items = new Dictionary<int, ItemData>();

    public static void ReadFile()
    {
        TextAsset xmlAsset = Resources.Load("ItemsData") as TextAsset;
        // надо получить число элементов в root'овом теге.
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlAsset) xmlDoc.LoadXml(xmlAsset.text);

        XmlNodeList dataList = xmlDoc.GetElementsByTagName("item");

        foreach (XmlNode item in dataList)
        {
            ItemData itemData = new ItemData();

            foreach (XmlNode attribute in item.ChildNodes)
            {
                if (attribute.Name == "id") int.TryParse(attribute.InnerText, out itemData.id);
                else if (attribute.Name == "name") itemData.name = attribute.InnerText;
                else if (attribute.Name == "descriptionItem") itemData.descriptionItem = attribute.InnerText;
                else if (attribute.Name == "pathIcon") itemData.pathIcon = attribute.InnerText;
                else if (attribute.Name == "categories") itemData.categories = int.Parse(attribute.InnerText);
                else if (attribute.Name == "isstackable") itemData.isStackable = int.Parse(attribute.InnerText) == 1 ? true : false;
            }
            items.Add(itemData.id, itemData);
        }
    }

    public static ItemData GetItem (int id)
    {
        if (!items.ContainsKey(id)) return null;
        //Debug.Log("return items[id]");
        return items[id];
    }
}
