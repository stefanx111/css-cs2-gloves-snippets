using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Memory;

namespace Gloves_Test;
public class Gloves_Test : BasePlugin
{
    public override string ModuleAuthor => "StefanX";
    public override string ModuleName => "Gloves Test";
    public override string ModuleVersion => "1.0";

    public override void Load(bool hotReload)
    {
        RegisterEventHandler<EventPlayerSpawn>(OnPlayerSpawn);
    }

    public void SetOrAddAttributeValueByName(CAttributeList attr, string name, float f)
    {
        var SetAttr = VirtualFunction.Create<IntPtr, string, float, int>("\\x55\\x48\\x89\\xE5\\x41\\x57\\x41\\x56\\x49\\x89\\xFE\\x41\\x55\\x41\\x54\\x49\\x89\\xF4\\x53\\x48\\x83\\xEC\\x78");
        SetAttr(attr.Handle, name, f);
    }

    public void SetPlayerBody(CCSPlayerController player, string model, int i)
    {
        var SetBody = VirtualFunction.Create<IntPtr, string, int, int>("\\x55\\x48\\x89\\xE5\\x41\\x56\\x49\\x89\\xF6\\x41\\x55\\x41\\x89\\xD5\\x41\\x54\\x49\\x89\\xFC\\x48\\x83\\xEC\\x08");
        SetBody(player.PlayerPawn.Value!.Handle, model, i);
    }

    private HookResult OnPlayerSpawn(EventPlayerSpawn @event, GameEventInfo info)
	{
		CCSPlayerController? player = @event.Userid;
        if (player != null && player.IsValid && player.PlayerPawn != null && player.PlayerPawn.IsValid && !player.IsBot && !player.IsHLTV)
        {
     
            player.PlayerPawn!.Value!.EconGloves.ItemDefinitionIndex = 5030;
            player.PlayerPawn!.Value!.EconGloves.ItemIDLow = 16384 & 0xFFFFFFFF;
            player.PlayerPawn!.Value!.EconGloves.ItemIDHigh = 16384 >> 32;

            Server.NextFrame(() => {
                player.PlayerPawn!.Value!.EconGloves.Initialized = true;
                SetPlayerBody(player, "default_gloves", 1);
                SetOrAddAttributeValueByName(player.PlayerPawn!.Value!.EconGloves.NetworkedDynamicAttributes, "set item texture prefab", 10048);

            });

        }
		return HookResult.Continue;
	}

}

