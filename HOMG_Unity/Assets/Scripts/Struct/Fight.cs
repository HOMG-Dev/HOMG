
public class Fight
{
    public int player1Id; // 玩家1 ID
    public bool player1DEFingTag; // 玩家1 是否处于防御状态
    public int player2Id; // 玩家2 ID
    public bool player2DEFingTag; // 玩家2 是否处于防御状态

    public Division player1Division; // 玩家1 战斗师
    public Division player2Division; // 玩家2 战斗师


    public Fight(int player1Id, bool player1DEFingTag, int player2Id,bool player2DEFingTag, Division player1Division, Division player2Division)
    {
        this.player1Id = player1Id;
        this.player1DEFingTag = player1DEFingTag;
        this.player2Id = player2Id;
        this.player2DEFingTag = player2DEFingTag;
        this.player1Division = player1Division;
        this.player2Division = player2Division;
    }

}
