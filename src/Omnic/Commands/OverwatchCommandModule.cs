using NetCord;
using NetCord.Rest;
using NetCord.Services;
using NetCord.Services.ApplicationCommands;
using Omnic.Helpers;

namespace Omnic.Commands;

[SlashCommand("overwatch", "Overwatch command", DefaultGuildPermissions = Permissions.UseApplicationCommands)]
public class OverwatchCommandModule : ApplicationCommandModule<ApplicationCommandContext>
{
    [SubSlashCommand("role", "Get a random role")]
    public string RandomRole(int count)
    {
        if (count is < 1 or > 10)
            return "Count must be between 1 and 10";
        
        return $"Roles: {string.Join("", OverwatchHelpers.GetRandomRole(count).Select(r => r.Emoji))}";
    }

    [SubSlashCommand("challenge", "Get a random challenge")]
    public async Task RandomChallenge()
    {
        Challenge challenge = Random.Shared.GetItems(OverwatchHelpers.Challenges, 1)[0];

        EmbedProperties embed = new EmbedProperties()
            .WithTitle(challenge.Name)
            .WithDescription(challenge.Description);

        ActionRowProperties actionRow = new ActionRowProperties()
            .WithComponents([
                new ButtonProperties("ow.challenge.complete", "Complete", ButtonStyle.Success),
                new ButtonProperties("ow.challenge.abort", "Abort", ButtonStyle.Danger),
                ]);

        InteractionMessageProperties response = new InteractionMessageProperties()
            .WithContent("Your challenge is:")
            .WithEmbeds([embed])
            .WithComponents([actionRow]);

        await RespondAsync(InteractionCallback.Message(response));
    }
}