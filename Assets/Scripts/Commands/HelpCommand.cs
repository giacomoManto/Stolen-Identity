public class HelpCommand : CommandTemplate
{
    public HelpCommand(GameManager manager) : base(manager)
    {
        commandName = "help";
        description = "All of my important reminders. This should help me get started and I should use this when confused.";
        executeText = "If I remember correctly...which isn't saying much, <b>'list actions'</b> helps me remember what general things I'm able to do. If I want more information on certain actions I can write in <b>'info [action]'</b> to learn more. If I want to glean more information from important, <b>bold</b>, objects in the room I can write <b>'inspect [object]</b>'. I guess it's also important to remember that most <b>objects</b> have less generic actions I can do to them, like <b>eat</b>ing an <b>apple</b> or <b>open</b>ing a <b>door</b>. I should always keep  my actions simple, something like <b>verb noun</b>.";
    }
}
