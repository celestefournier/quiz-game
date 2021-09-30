using System.Collections.Generic;

[System.Serializable]
public class Difficulty
{
    public string name;
    public List<Question> questions = new List<Question>();
}
