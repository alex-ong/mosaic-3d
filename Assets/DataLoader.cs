using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class DataLoader : MonoBehaviour
{
    public string fileToLoad;

    private class ColorString
    {
        public Color c;
        public string s;
        public bool used;
    }

    private List<ColorString> colorStrings = new List<ColorString>();
    private List<ColorString> remainingColorStrings = new List<ColorString>();
    // Use this for initialization
    void Start()
    {
        string path = this.GetComponent<MasterController>().rootFolder + this.fileToLoad;
        Debug.Log("Loading data file from:" + path);
        WWW request = new WWW(path);
        
        while (!request.isDone) {            
        }
        Debug.Log("Data: " + request.text);
        string[] data = request.text.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        this.ParseData(data);
    }

    private void ParseData(string[] lines)
    {
        foreach (string line in lines) {
            int fileColorSplit = -1;
            for (int i = 0; i < line.Length; i++) {
                if (line[i] == ',') {
                    fileColorSplit = i;
                    break;
                }
            }
            if (fileColorSplit < 0) {
                break;
            }

            string fileName = line.Substring(0, fileColorSplit);
            string colorString = line.Substring(fileColorSplit + 2, line.Length - 2 - (fileColorSplit + 1));
            string[] colorComponents = colorString.Split(new char[]{ ',' });
            Color rgb = new Color(
                            int.Parse(colorComponents[0]) / 255.0f,
                            int.Parse(colorComponents[1]) / 255.0f,
                            int.Parse(colorComponents[2]) / 255.0f
                        );

            ColorString cs = new ColorString();
            cs.c = rgb;
            cs.s = fileName;
            cs.used = false;
            this.colorStrings.Add(cs);
        }
            
    }

    public string RandomFile()
    {
        return "picture (3220).jpg";
    }

    public void Reset()
    {
        this.remainingColorStrings = new List<ColorString>(this.colorStrings);
    }

    public string matchColour(Color rgb)
    {
        this.remainingColorStrings.OrderBy(cs =>
        Mathf.Abs(cs.c.r - rgb.r) +
        Mathf.Abs(cs.c.g - rgb.b) +
        Mathf.Abs(cs.c.b - rgb.b)
        ); 

        string result = this.remainingColorStrings[0].s;
        this.remainingColorStrings.RemoveAt(0);
        return result;
    }


	
}
