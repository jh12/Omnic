namespace Omnic.Helpers;

public static class OverwatchHelpers
{
    public static OverwatchRole Tank = new("Tank", "🛡️");
    public static OverwatchRole Damage = new("Damage", "🏹");
    public static OverwatchRole Support = new("Support", "💊");

    public static OverwatchRole[] Roles = [Tank, Damage, Support];

    // TODO: Move to file and make editable, maybe also categorize to easier pick specific challenges
    public static Challenge[] Challenges = [
        new ("Anonymous Heroes", "Don't say any hero names"),
        new ("Shotcaller Shuffle", "Each game a different player must do callouts"),
        new ("Least Played Chaos", "Everyone must play their least played hero"),
        new ("Role Roulette", "Randomized roles each queue"),
        new ("Sweet Death", "Compliment the hero who killed you"),
        new ("Buddy System", "Find a buddy and stick together"),
        new ("Questionable Comms", "Communications must be questions"),
        new ("Career Change Interviews", "Interview the changed hero, what made them change their career"),
        new ("Nature Documentary", "When spectating/dead narrate the game as a nature documentary"),
        new ("Hot Takes", "When dead, share any hot take. You do not have to agree on it either"),
        new ("Deadly Joker", "Tell a joke when you are dead")
    ];

    public static OverwatchRole GetRandomRole()
    {
        return GetRandomRole(1)[0];
    }

    public static OverwatchRole[] GetRandomRole(int results)
    {
        return Random.Shared.GetItems(Roles, results);
    }
}

public record OverwatchRole(string Name, string Emoji);
public record Challenge(string Name, string Description);