using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class TestXML : MonoBehaviour {

	// Use this for initialization
	void Start () {
        string locTranslation = Instantiate(Resources.Load("Test")).ToString();
		ParseTranslation(locTranslation); 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    Dictionary<string, string> m_LocText = new Dictionary<string, string>();

    private void ParseTranslation(string translation)
    {
        try
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(translation);
            //root
            XmlNode root = doc.SelectSingleNode("bookstore");
            if (root == null)
            {
                Debug.LogError("empty xml");
                return;
            }
            foreach (XmlNode node in root.ChildNodes)
            {
                if (node.NodeType == XmlNodeType.Comment)
                {
                    continue;
                }
                string id = GetXMLAttributesString(node, "category", string.Empty).ToUpper();
                if (string.IsNullOrEmpty(id))
                {
                    continue;
                }
                if (id == "WEB")
                {
                    var n = node.SelectSingleNode("author");
                    Debug.Log("WEB author=" + n.InnerText);
                    n = node.SelectSingleNode("price");

                    float price;
                    if (float.TryParse(n.InnerText, out price))
                    {
                        Debug.Log("WEB price=" + price);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("error in parse xml " + e.ToString());
        }
    }
    private string GetXMLAttributesString(XmlNode node, string key, string defalutString)
    {
        XmlAttribute attr = node.Attributes[key];
        if (attr == null)
        {
            return defalutString;
        }
        return attr.Value;
    }
}
