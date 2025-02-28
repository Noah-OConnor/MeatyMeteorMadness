using System;
using System.IO;
using UnityEngine;

// DESIGN PATTERN - Memento
// Implements the Memento pattern by storing game state in a serializable object.

[Serializable]
public class GameMemento
{
    public int BestScore;
    public int BestMeat;
    public int BestTime;

    public GameMemento(int score, int meat, int time)
    {
        BestScore = score;
        BestMeat = meat;
        BestTime = time;
    }

    public static void Save(GameMemento memento)
    {
        string json = JsonUtility.ToJson(memento);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public static GameMemento Load()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameMemento>(json);
        }
        return new GameMemento(0, 0, 0); // Default values if no save exists
    }
}
