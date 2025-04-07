using JetBrains.Annotations;
using UnityEngine;

public struct IDCard
{
    public string Name { get; }
    public int SecurityLevel { get; }

    private IDCard(string name, int securityLevel)
    {
        Name = name;
        SecurityLevel = securityLevel;
    }

    // Predefined ID levels
    public static readonly IDCard None = new IDCard("Any", 0);
    public static readonly IDCard Thief = new IDCard("Thief", 1);
    public static readonly IDCard Patient = new IDCard("Patient", 1);
    public static readonly IDCard Brawler = new IDCard("Brawler", 1);
    public static readonly IDCard Guard = new IDCard("Guard", 2);
    public static readonly IDCard Doctor = new IDCard("Doctor", 3);

    public static IDCard StringAsID(string name)
    {
        switch (name.ToLower()) {
            case "thief":
                return Thief;
            case "patient":
                return Patient;
            case "brawler":
                return Brawler;
            case "guard":
                return Guard;
            case "doctor":
                return Doctor;
            default:
                return IDCard.None;
        }
    }


    public override string ToString() => $"{Name}";

    public int GetSecurityLevel() => SecurityLevel;
}
