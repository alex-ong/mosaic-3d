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
    public bool ready = false;
    protected float PercLoaded = 0.0f;
    
    IEnumerator Start()
    {
        string path = this.GetComponent<MasterController>().rootFolder + this.fileToLoad;
        Debug.Log("Loading data file from:" + path);
        WWW request = new WWW(path);
        
        yield return request;
        string text = request.text;
        
        //Split the string into an array over several frames     
        List<string> entries = new List<string>();
        System.Text.StringBuilder tempString = new System.Text.StringBuilder();
        
        for (int i = 0; i < text.Length; i++) {
            tempString.Append(text[i]);
            if (text[i] == '\n') {
                string result = tempString.ToString();
                entries.Add(result.Trim());
                tempString.Length = 0;
                tempString.Capacity = 0;
            }
            if (i % 10000 == 0) {
                yield return new WaitForEndOfFrame();                
            }
            this.PercLoaded = 0.5f * i / (float)text.Length;
        }
                
        StartCoroutine(this.ParseData(entries));
    }

    private IEnumerator ParseData(List<string> lines)
    {
        int perFrame = 1000;
        int lineCounter = perFrame;
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
            lineCounter++;
            if (lineCounter % perFrame == 0) {
                yield return new WaitForEndOfFrame();               
                this.PercLoaded = 0.5f + (lineCounter / (float)lines.Count) * 0.5f;
            }
        }
        
        this.ready = true;           
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

    public void OnGUI()
    {
        float boxWidth = 200f;
        float boxHeight = 50f;
        Rect box = new Rect(Screen.width / 2 - boxWidth / 2, Screen.height / 2 - boxHeight * 2, boxWidth, boxHeight);
        if (!ready) {
            GUI.Box(box, "Loading color diffs..." + (PercLoaded * 100f).ToString("0.00") + "%");
        }
    }

}
