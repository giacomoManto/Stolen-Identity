

public class CheckInventory : CommandTemplate
{
    PlayerInfo playerInfo;
    public CheckInventory(GameManager manager, PlayerInfo player) : base(manager)
    {
        commandName = "check inventory";
        description = "Checks the players inventory, listing all of their items and ID cards.";
        playerInfo = player;
    }
    public override bool Execute()
    {
        gameManager.AddTextToJournal(playerInfo.listInventory());
        return true;
    }
}
