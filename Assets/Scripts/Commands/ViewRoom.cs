public class ViewRoom : CommandTemplate
{
    public ViewRoom(GameManager manager, PlayerInfo player) : base(manager)
    {
        commandName = "view room";
        description = "Observes the room once more, listing all things visible to me.";
    }
    public override bool Execute()
    {
        gameManager.displayCurrentRoomDesc();
        return true;
    }
}
