using System;
using System.Collections;
using System.IO;
using System.Xml;

using UnityEngine;

public class Lang
{
    private Hashtable Strings;

    public Lang(string path, string language, bool web = false)
    {
        if (!web)
        {
            setLanguage(path, language);
        }
    }

    public void setLanguage(string path, string language)
    {
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(path);

        Strings = new Hashtable();
        XmlNodeList elements = xml.DocumentElement.GetElementsByTagName(language);

        if (elements != null)
        {
            foreach (XmlNode languageValues in elements)
            {
                XmlNodeList languageContents = languageValues.ChildNodes;
                foreach (XmlNode values in languageContents)
                {
                    Strings.Add(values.Name, values.InnerText);
                }
            }
        }
        else
        {
            Debug.LogError("The specified language does not exist: " + language);
        }
    }


    public string getString(string name)
    {
        if (!Strings.ContainsKey(name))
        {
            Debug.LogError("The specified string does not exist: " + name);
            return "";
        }
        return "" + Strings[name];
    }

}