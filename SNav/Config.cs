// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using Exiled.API.Interfaces;

namespace Mistaken.SNav
{
    internal sealed class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;

        [Description("If true then debugs will be displayed")]
        public bool VerboseOutput { get; set; }

        [Description("Spawn points for SNav 3000 (Room, OffsetX, OffsetY, OffsetZ)")]
        public string[] SNav3000Spawns { get; set; } = new[]
        {
            "Scp079Second, 4, 2, 8.4",
            "NukeSurface, 5.5, 2, -3",
            "NukeSurface, 6.5, 2, -3",
            "HczArmory, -2, 2, -2",
            "LczArmory, 6, 2, -1",
            "LczArmory, 6, 2, 1.5",
        };

        [Description("Spawn points for SNav Ultimate (Room, OffsetX, OffsetY, OffsetZ)")]
        public string[] SNavUltimateSpawns { get; set; } = new[]
        {
            "Scp079Second, 4.5, 2, 8.4",
        };
    }
}
