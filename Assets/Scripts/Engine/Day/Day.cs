using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

[Serializable]
public class DDate {
    readonly static string[] sst = new string[4] { "sp", "su", "au", "wi" };

    public int year;
    public int date;

    public float time;

    public string season {
        get {
            return sst[(date - 1) / 5];
        }
    }

    public DDate(int year = 0, int date = 0, float time = 0.0f) { 
        this.year = year;
        this.date = date;
        this.time = time;

        Valider();
    }

    public DDate(string sr) {
        var l = sr.Split(" ");
        year = int.Parse(l[0]);
        date = int.Parse(l[1]);
        time = float.Parse(l[2]);

        Valider();
    }

    public void AddDate(int year = 0, int date = 0, float time = 0.0f)
    {
        this.year += year;
        this.date += date;
        this.time += time;

        Valider();
    }

    public void AddDate(DDate d) {
        this.year += d.year;
        this.date += d.date;
        this.time += d.time;

        Valider();
    }

    public void SubDate(int year = 0, int date = 0, float time = 0.0f)
    {
        this.year -= year;
        this.date -= date;
        this.time -= time;

        Valider();
    }

    public void SubDate(DDate d)
    {
        this.year -= d.year;
        this.date -= d.date;
        this.time -= d.time;

        Valider();
    }

    private void Valider()
    {
        #region preprocess valider
        if (time >= 900.0f) {
            date += (int)time / 900;
            time %= 900.0f;
        }

        if (time < 0.0f) {
            var remD = (((int)Mathf.Abs(time)) / 900) + 1;

            time += remD * 900.0f;
            date -= remD;
        }

        if (date > 20)
        {
            year += date / 20;
            date %= 20;
        }

        if (date < 1) {
            year -= ((date / 20) + 1);
            date += ((date / 20) + 1) * 20;
        }
        #endregion
    }
        

    public string serialize() {
        Valider();
        return $"{year} {date} {time}";
    }

    public static string SeasonText(string sn) {
        return TextManager.GetText(KeyedText.keyedText("BasicNouns", Array.IndexOf(sst, sn) + 1));
    }
}

public class Day : MonoBehaviour
{
    public static Day instance
    {
        get; private set;
    }
    public Text dayText;

    public DDate date {
        get { return SaveLoadUtility.instance.data.date; }
        set { SaveLoadUtility.instance.data.date = value; }
    }

    private float d_Multiplier = 1.0f;

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        date.AddDate(0, 0, (Move.instance.movable ? Time.deltaTime : 0) * d_Multiplier);
        Refresh();

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) d_Multiplier *= 2;
        if (Input.GetKeyDown(KeyCode.KeypadMinus)) d_Multiplier /= 2;
    }

    public void Refresh() {
        var dt8 = date.time * 96;
        dayText.text = $"{date.year}년 {date.date}일 {((int)dt8 / 3600):00}:{(int)((dt8 % 3600) / 60):00}:{(int)(dt8 % 60):00} (계절: {DDate.SeasonText(date.season)})";
    }
}
