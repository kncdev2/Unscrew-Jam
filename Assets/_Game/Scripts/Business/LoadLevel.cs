using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Obstacle
{
}

[Serializable]
public class Hole
{
    public int eType;
}

[Serializable]
public class Box
{
    public int eColor;
    public Hole[] holes;
}

[Serializable]
public class Shape
{
    public int identify;
    public int id;
    public int eColorShape;
    public int layer;
    public Vector3 worldPos;
    public float localRotationZ;
    public float localScaleX;
    public float localScaleY;
    public Hole[] holes;
}

[Serializable]
public class LevelData
{
    public int level;
    public int eDifficulty;
    public int holeQueue;
    public Box[] boxes;
    public Shape[] shapes;
    public Obstacle[] obstacles;
}

public class LoadLevel : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] LevelData levelData;
    void Start()
    {
        LoadJsonFromResources();
    }

    void LoadJsonFromResources()
    {
        TextAsset textAsset = Resources.Load<TextAsset>($"levels/{level}");

        if (textAsset != null)
        {
            string jsonContent = textAsset.text;
            levelData = JsonUtility.FromJson<LevelData>(jsonContent);
            Debug.Log(jsonContent);
        }
        else
        {
            Debug.LogError("Text file not found in Resources.");
        }
    }
}
