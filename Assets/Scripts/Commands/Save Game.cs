public class SaveGame : CommandTemplate
{
    public SaveGame(GameManager manager, PlayerInfo player) : base(manager)
    {
        commandName = "save game";
        description = "Saves the players current progress.";
    }
    public override bool Execute()
    {
        gameManager.addTextToJournal("Game state saved! (Not really but at least this message pops up!)");
        return true;
    }
}
