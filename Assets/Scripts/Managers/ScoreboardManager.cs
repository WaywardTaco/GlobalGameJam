using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UserScore : MonoBehaviour
{

    public string Name;
    public int Money;
    public int Days;

    public UserScore(string name, int money, int days)
    {
        Name = name;
        Money = money;
        Days = days;
    }

    public string GetName() { return Name; }
    public int GetMoney() { return Money; }
    public int GetDays() { return Days; }

}

public class ScoreboardManager : MonoBehaviour
{

    static UserScore[] ScoreBoard;
    Dictionary<UserScore, int> Ranks = new Dictionary<UserScore, int>();
    IEnumerable<UserScore> UserRankings = ScoreBoard.OrderBy(UserScore => UserScore.Days);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
