public class HelpCommand : CommandTemplate
{
    public HelpCommand(GameManager manager) : base(manager)
    {
        commandName = "help";
        description = "All of my important reminders. This should help me get started and I should use this when confused.";
    }
}
