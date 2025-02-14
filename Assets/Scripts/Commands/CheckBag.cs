

public class CheckBag : CommandTemplate
{
    PlayerInfo playerInfo;
    public CheckBag(GameManager manager, PlayerInfo player) : base(manager)
    {
        commandName = "check bag";
        description = "Checks the players inventory, listing all of their items and ID cards.";
        playerInfo = player;
    }
    public override bool Execute()
    {
        gameManager.addTextToJournal(playerInfo.listInventory());
        return true;
    }
}
