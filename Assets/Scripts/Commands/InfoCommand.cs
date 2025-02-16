public class InfoCommand : CommandTemplate
{
    public InfoCommand(GameManager manager) : base(manager)
    {
        commandName = "info";
        description = "Provides information on a action. Works best if I do info [action name].";
    }
}
