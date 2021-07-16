// -----------------------------------------------------------------------
// <copyright file="GeneratorPatch.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.Generic;
using Exiled.API.Features;
using Mistaken.API;
using UnityEngine;

namespace Mistaken.SNav
{
    [HarmonyLib.HarmonyPatch(typeof(Generator079), nameof(Generator079.EjectTablet))]
    internal static class GeneratorPatch
    {
        internal static readonly Dictionary<Generator079, Inventory.SyncItemInfo> Generators = new Dictionary<Generator079, Inventory.SyncItemInfo>();

#pragma warning disable SA1313 // Parameter names should begin with lower-case letter
        internal static bool Prefix(Generator079 __instance)
#pragma warning restore SA1313 // Parameter names should begin with lower-case letter
        {
            if (Generators.TryGetValue(__instance, out var snav))
            {
                MapPlus.Spawn(snav, __instance.tabletEjectionPoint.position, __instance.tabletEjectionPoint.rotation, Vector3.one);
                Generators.Remove(__instance);
                __instance.NetworkisTabletConnected = false;
                return false;
            }

            return true;
        }
    }
}
