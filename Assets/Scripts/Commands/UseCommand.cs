public class UseCommand : CommandTemplate
{
    public UseCommand(GameManager manager) : base(manager)
    {
        commandName = "use";
        description = "Use an item or object such as a door.";
    }
}
