
public class ScoreService
{
    private int score;

    public int Score { get => score; set => score = value; }

    public void AddScore(int ammount) => Score+=ammount;
    
}
