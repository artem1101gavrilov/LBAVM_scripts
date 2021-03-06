﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class NPCData : CreatureData
{
    public List<string> dialogs;
    public int questID;
    public string name;

    public NPCData(string NPCname)
    {
        /*questID = -1;//нет квеста
        //TODO вынести чтение файла в NPCController
        name = NPCname;
        System.IO.StreamReader file = new System.IO.StreamReader("NPC_QuestID_Dialog.txt", System.Text.Encoding.GetEncoding(1251));
        string line;
        while ((line = file.ReadLine()) != null)
        {
            if (line == name)
            {
                line = file.ReadLine();
                int.TryParse(line, out questID);
                dialog = file.ReadLine();
            }
        }
        file.Close();*/
        dialogs = new List<string>();
        //Загружаем из ресурсов наш xml файл
        TextAsset xmlAsset = Resources.Load("NPC_QuestID_Dialog") as TextAsset;
        // надо получить число элементов в root'овом теге.
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlAsset) xmlDoc.LoadXml(xmlAsset.text);
        
        XmlNodeList dataList = xmlDoc.GetElementsByTagName("npc");
        
        foreach (XmlNode item in dataList) {
            XmlNodeList itemContent = item.ChildNodes;
            bool ThisNPC = false;
            foreach (XmlNode itemItens in itemContent) {
                if (itemItens.Name == "name") {
                    if(itemItens.InnerText == NPCname){
                        name = NPCname;
                        ThisNPC = true;
                    }
                }
                else if (itemItens.Name == "questID" && ThisNPC) questID = int.Parse(itemItens.InnerText); //TODO to int
                else if (itemItens.Name == "dialog" && ThisNPC) dialogs.Add(itemItens.InnerText);
            }
        }
    }

    public string GetDialog ()
    {
        int numb = Random.Range(0, dialogs.Count);
        return dialogs[numb];
    }
}
