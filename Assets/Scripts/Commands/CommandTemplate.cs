using UnityEngine;

public abstract class CommandTemplate {
    protected string commandName;
    protected string description;
    protected GameManager gameManager;
    public CommandTemplate(GameManager manager)
    {
        gameManager = manager;
    }
    public string getName()
    {
        return commandName;
    }
    public virtual bool Execute()
    {
        //return true if the command is successfully run
        //false if the command is not meant to be executed, or not implemented
        return false;
    }
    public string getDescription()
    {
        return commandName + ": "+ description;
    }
}
