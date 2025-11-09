using System.Runtime.CompilerServices;
using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Entities.Constants;
using Evo.api.plugin;
using Evo.api.plugin.services;
using Evo.plugin.extensions;

namespace Evo.plugin.services;

public sealed class NoBlockService : INoBlockService {
  public bool IsEnabled { get; private set; } = true;
  public void Enable() => IsEnabled = true;
  public void Disable() => IsEnabled = false;
  public void Toggle() => IsEnabled = !IsEnabled;
  
  private const byte DEBRIS = (byte)CollisionGroup.COLLISION_GROUP_DEBRIS;
  private const string C_COLLISION_PROPERTY = "CCollisionProperty";
  private const string M_COLLISION_GROUP = "m_CollisionGroup";
  private const string M_COLLISION_ATTRIBUTE = "m_collisionAttribute";

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public void ApplyNoBlock(CCSPlayerController? player)
  {
    if (!IsEnabled) return;
    
    if (player is null || !player.IsReal()) return;
    
    var pawn = player.PlayerPawn.Value;
    if (pawn is null || pawn.Handle == IntPtr.Zero || !pawn.IsValid) return;
    
    var collision = pawn.Collision;
    var attr      = collision.CollisionAttribute;

    // If already set on both, nothing to do
    if (attr.CollisionGroup == DEBRIS && collision.CollisionGroup == DEBRIS)
      return;

    // Set only when different, and notify only for what changed
    if (attr.CollisionGroup != DEBRIS)
    {
      attr.CollisionGroup = DEBRIS;
      Utilities.SetStateChanged(pawn, C_COLLISION_PROPERTY, M_COLLISION_ATTRIBUTE);
    }

    if (collision.CollisionGroup == DEBRIS) return;
    collision.CollisionGroup = DEBRIS;
    Utilities.SetStateChanged(pawn, C_COLLISION_PROPERTY, M_COLLISION_GROUP);
  }
}