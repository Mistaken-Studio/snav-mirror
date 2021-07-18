// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.ComponentModel;
using Mistaken.Updater.Config;

namespace Mistaken.SNav
{
    /// <inheritdoc/>
    public class Config : IAutoUpdatableConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether debugs should be displayed.
        /// </summary>
        [Description("If true then debugs will be displayed")]
        public bool VerbouseOutput { get; set; }

        /// <inheritdoc/>
        [Description("Auto Update Settings")]
        public System.Collections.Generic.Dictionary<string, string> AutoUpdateConfig { get; set; }

        /// <summary>
        /// Gets or sets SNav 3000 Spawns.
        /// </summary>
        [Description("Spawn points for SNav 3000 (Room, OffsetX, OffsetY, OffsetZ)")]
        public string[] SNav3000Spawns { get; set; } = new string[]
        {
            "Scp079Second, 4, 2, 8.4",
            "NukeSurface, 5.5, 2, -3",
            "NukeSurface, 6.5, 2, -3",
            "HczArmory, -2, 2, -2",
            "LczArmory, 6, 2, -1",
            "LczArmory, 6, 2, 1.5",
        };

        /// <summary>
        /// Gets or sets SNav Ultimate Spawns.
        /// </summary>
        [Description("Spawn points for SNav Ultimate (Room, OffsetX, OffsetY, OffsetZ)")]
        public string[] SNavUltimateSpawns { get; set; } = new string[]
        {
            "Scp079Second, 4.5, 2, 8.4",
        };
    }
}
