public class InspectCommand : CommandTemplate
{
    public InspectCommand(GameManager manager) : base(manager)
    {
        commandName = "inspect";
        description = "Inspects an object/item/NPC and gives me information about it. ";
    }
}
