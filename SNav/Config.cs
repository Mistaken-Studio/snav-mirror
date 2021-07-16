// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using Mistaken.API;

namespace Mistaken.SNav
{
    /// <inheritdoc/>
    public class Config : IAutoUpdatableConfig
    {
        /// <inheritdoc/>
        public bool VerbouseOutput { get; set; }

        /// <inheritdoc/>
        public string AutoUpdateUrl { get; set; }

        /// <inheritdoc/>
        public AutoUpdateType AutoUpdateType { get; set; }

        /// <inheritdoc/>
        public string AutoUpdateLogin { get; set; }

        /// <inheritdoc/>
        public string AutoUpdateToken { get; set; }

        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets SNav 3000 Spawns.
        /// </summary>
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
        public string[] SNavUltimateSpawns { get; set; } = new string[]
        {
            "Scp079Second, 4.5, 2, 8.4",
        };
    }
}
