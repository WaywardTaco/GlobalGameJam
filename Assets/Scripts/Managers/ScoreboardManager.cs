using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScore : MonoBehaviour
{

    private string Name;
    private int Money;
    private int Days;

    public string GetName() { return Name; }
    public int GetMoney() { return Money; }
    public int GetDays() { return Days; }

}

public class ScoreboardManager : MonoBehaviour
{

    private UserScore[] ScoreBoard;
    Dictionary<UserScore, int> Ranks = new Dictionary<UserScore, int>();
    //IEnumerable<UserScore> UserRankings = UserScore.OrderBy(UserScore => UserScore.Days);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
