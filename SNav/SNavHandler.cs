﻿// -----------------------------------------------------------------------
// <copyright file="SNavHandler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Extensions;
using Exiled.API.Features;
using MEC;
using Mistaken.API;
using Mistaken.API.Diagnostics;
using Mistaken.API.GUI;
using Mistaken.CustomItems;
using Scp914;
using UnityEngine;

namespace Mistaken.SNav
{
    /// <inheritdoc/>
    public class SNavHandler : Module
    {
        /// <summary>
        /// Enables Expereimental Feature.
        /// </summary>
        public static readonly bool HideTablet = false;

        /// <summary>
        /// Rooms looks.
        /// </summary>
        public static readonly Dictionary<SNavRoomType, string[]> Presets = new Dictionary<SNavRoomType, string[]>()
        {
            {
                SNavRoomType.ERROR,
                new string[]
                {
                    "#########",
                    "#########",
                    "#########",
                }
            },
            {
                SNavRoomType.NONE,
                new string[]
                {
                    "         ",
                    "         ",
                    "         ",
                }
            },
            {
                SNavRoomType.HS_TB,
                new string[]
                {
                    "   | |   ",
                    "   | |   ",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.HS_LR,
                new string[]
                {
                    "_________",
                    "_________",
                    "         ",
                }
            },
            {
                SNavRoomType.HC_LT,
                new string[]
                {
                    "___| |   ",
                    "_____|   ",
                    "         ",
                }
            },
            {
                SNavRoomType.HC_RT,
                new string[]
                {
                    "   | |___",
                    "   |_____",
                    "         ",
                }
            },
            {
                SNavRoomType.HC_LB,
                new string[]
                {
                    "_____.   ",
                    "___. |   ",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.HC_RB,
                new string[]
                {
                    "   ._____",
                    "   | .___",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.IT_RL_T,
                new string[]
                {
                    "___| |___",
                    "_________",
                    "         ",
                }
            },
            {
                SNavRoomType.IT_RL_B,
                new string[]
                {
                    "_________",
                    "___. .___",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.IT_TB_L,
                new string[]
                {
                    "___| |   ",
                    "___. |   ",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.IT_TB_R,
                new string[]
                {
                    "   | |___",
                    "   | .___",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.IX,
                new string[]
                {
                    "___| |___",
                    "___. .___",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.END_T,
                new string[]
                {
                    ".__| |__.",
                    "|  END  |",
                    "|_______|",
                }
            },
            {
                SNavRoomType.END_B,
                new string[]
                {
                    "._______.",
                    "|  END  |",
                    "|__. .__|",
                }
            },
            {
                SNavRoomType.END_R,
                new string[]
                {
                    "._______.",
                    "|  END  .",
                    "|_______|",
                }
            },
            {
                SNavRoomType.END_L,
                new string[]
                {
                    "._______.",
                    ".  END  |",
                    "|_______|",
                }
            },
            {
                SNavRoomType.CLASSD,
                new string[]
                {
                    "._______.",
                    "|_______.",
                    "         ",
                }
            },
            {
                SNavRoomType.SCP_939_TB,
                new string[]
                {
                    ".__| |__.",
                    "|  939  |",
                    "|__. .__|",
                }
            },
            {
                SNavRoomType.SCP_939_RL,
                new string[]
                {
                    "._______.",
                    ". _939_ .",
                    "|_______|",
                }
            },
            {
                SNavRoomType.EZ_HCZ_CHECKPOINT_TB,
                new string[]
                {
                    ".__| |__.",
                    "|__.C.__|",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.EZ_HCZ_CHECKPOINT_RL,
                new string[]
                {
                    ".__|<mark>_</mark>|__.",
                    ".__ C __.",
                    "   |<mark>_</mark>|   ",
                }
            },
            {
                SNavRoomType.LCZ_A_B,
                new string[]
                {
                    "._______.",
                    "|__.A.__|",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.LCZ_A_L,
                new string[]
                {
                    "   |<mark>_</mark>|__.",
                    "   |A __.",
                    "   |<mark>_</mark>|   ",
                }
            },
            {
                SNavRoomType.LCZ_A_T,
                new string[]
                {
                    ".__| |__.",
                    "|___A___|",
                    "         ",
                }
            },
            {
                SNavRoomType.LCZ_A_R,
                new string[]
                {
                    ".__|<mark>_</mark>|   ",
                    ".__ A|   ",
                    "   |<mark>_</mark>|   ",
                }
            },
            {
                SNavRoomType.LCZ_B_B,
                new string[]
                {
                    "._______.",
                    "|__.B.__|",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.LCZ_B_L,
                new string[]
                {
                    "   |<mark>_</mark>|__.",
                    "   |B __.",
                    "   |<mark>_</mark>|   ",
                }
            },
            {
                SNavRoomType.LCZ_B_T,
                new string[]
                {
                    ".__| |__.",
                    "|___B___|",
                    "         ",
                }
            },
            {
                SNavRoomType.LCZ_B_R,
                new string[]
                {
                    ".__|<mark>_</mark>|   ",
                    ".__ B|   ",
                    "   |<mark>_</mark>|   ",
                }
            },
            {
                SNavRoomType.NUKE_TB,
                new string[]
                {
                    ".__| |   ",
                    "|<mark>__</mark>. |   ",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.NUKE_RL,
                new string[]
                {
                    "._______.",
                    ".__. .__.",
                    "   |<mark>_</mark>|   ",
                }
            },
            {
                SNavRoomType.SCP049_TB,
                new string[]
                {
                    "   | '<mark>_</mark>| ",
                    "   | |   ",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.SCP049_RL,
                new string[]
                {
                    ".|<mark>_</mark>|____.",
                    "._______.",
                    "         ",
                }
            },
            {
                SNavRoomType.HID_TB,
                new string[]
                {
                    ".__| |   ",
                    "|    |   ",
                    "|__. |   ",
                }
            },
            {
                SNavRoomType.HID_RL,
                new string[]
                {
                    "._______.",
                    ". _____ .",
                    "|__._.__|",
                }
            },
            {
                SNavRoomType.COMPUTERS_UPSTAIRS_TB,
                new string[]
                {
                    ".__| |__.",
                    "|       |",
                    "|<mark>__</mark>. .__|",
                }
            },
            {
                SNavRoomType.COMPUTERS_UPSTAIRS_RL,
                new string[]
                {
                    "._______.",
                    ". ______.",
                    "|______<mark>_</mark>|",
                }
            },
            {
                SNavRoomType.TESLA_TB,
                new string[]
                {
                    "   | |   ",
                    "   [ ]   ",
                    "   | |   ",
                }
            },
            {
                SNavRoomType.TESLA_RL,
                new string[]
                {
                    "___._.___",
                    "___._.___",
                    "         ",
                }
            },
        };

        /// <summary>
        /// Rooms where someone was on last scan.
        /// </summary>
        public static readonly HashSet<Room> LastScan = new HashSet<Room>();

        /// <summary>
        /// Gets rooms in LCZ.
        /// </summary>
        public static Room[,] LCZRooms { get; private set; } = new Room[0, 0];

        /// <summary>
        /// Gets rooms in EZ and HCZ.
        /// </summary>
        public static Room[,] EZ_HCZRooms { get; private set; } = new Room[0, 0];

        /// <summary>
        /// Spawns SNAV.
        /// </summary>
        /// <param name="ultimate">If SNav should be ultimate.</param>
        /// <param name="pos">Where SNav should be spawned.</param>
        /// <returns>Spawned SNav.</returns>
        public static Pickup SpawnSNAV(bool ultimate, Vector3 pos)
        {
            if (ultimate)
            {
                return MapPlus.Spawn(
                    new Inventory.SyncItemInfo
                    {
                        durability = 401000f,
                        id = ItemType.WeaponManagerTablet,
                    },
                    pos,
                    Quaternion.identity,
                    new Vector3(2.5f, .75f, .75f));
            }
            else
            {
                return MapPlus.Spawn(
                    new Inventory.SyncItemInfo
                    {
                        durability = 301000f,
                        id = ItemType.WeaponManagerTablet,
                    },
                    pos,
                    Quaternion.identity,
                    new Vector3(2.0f, .50f, .50f));
            }
        }

        /// <summary>
        /// Generates Surface map.
        /// </summary>
        /// <param name="ultimate">If <see langword="true"/> then map will contain SNavUltimate elements.</param>
        /// <returns>Surface Map.</returns>
        public static string[] GenerateSurfaceSNav(bool ultimate = false)
        {
            var tmp =
@"    
             <color=gatea_color>._.</color>                                                          <color=escape_color>.______.</color>
         <color=gatea_color>.___| |___.</color>                                                      <color=escape_color>|      |</color>
         <color=gatea_color>|         |</color>                                                      <color=escape_color>|_|‾‾ ‾|</color>
       <color=gatea_color>|‾  GATE  A |</color>                                                      <color=escape_color>|ESCAPE|</color>
       <color=gatea_color>`‾|         |</color>                                                      <color=escape_color>| |____|</color>
          <color=gatea_color>‾‾|   |‾‾`</color>                                                      <color=escape_color>| |___.</color>
            <color=gatea_color>|   |</color>                                                         <color=escape_color>|___. |</color>
            <color=gatea_color>|   |</color>                                                             <color=escape_color>| |________.</color>
       <color=gatea_color>.____|   |_____.</color>                                                       <color=escape_color>|________. |</color>
       <color=gatea_color>|              |</color>                                                                <color=escape_color>| |</color>
       <color=gatea_color>`‾‾‾‾<color=gateahole_color>|   |</color>‾‾‾<color=gateahole_color>| |</color>     <color=nuke_color>._____.</color>                                                    <color=escape_color>| |</color>
            <color=gateahole_color>|   |   | |     <color=nuke_color>|NUKE |</color>                                                    <color=escape_color>: :</color>
            <color=gateahole_color>|   |   | |     <color=nuke_color>|_. ._|</color>                                                    <color=escape_color>| |</color>
       <color=gateahole_color>.____|   |___|_|_______| |_______.</color>                                            <color=escape_color>._| |_.</color>
       <color=gateahole_color>|                      | |   |   |</color><color=gateb_color>   ._|‾|                  </color><color=helipad_color>._________________</color><color=escape_color>|     |</color>
       <color=gateahole_color>|                      | |   |   |</color><color=gateb_color>   |   |__________________</color><color=helipad_color>|                       |</color>
<color=gateahole_color>|‾‾‾‾‾‾‾‾‾‾‾|   |‾‾‾\ ‾‾‾‾‾‾‾‾` `‾‾‾|‾‾‾|</color><color=gateb_color>‾‾‾|   GATE B             </color><color=helipad_color>                        |</color>
<color=gateahole_color>|           |   |    ‾‾‾‾‾‾‾        |   |</color><color=gateb_color>   `‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾</color><color=helipad_color>                        |</color>
<color=gateahole_color>| CAR ENTRY |   |                   |   |</color><color=gateb_color>                          </color><color=helipad_color>        HELIPAD         |</color>
<color=gateahole_color>|           |   |                   |   |</color><color=gateb_color>                          </color><color=helipad_color>                        |</color>
<color=gateahole_color>|___________|   |___________________|   |</color><color=gateb_color>__________________________</color><color=helipad_color>.                       |</color>
       <color=gateahole_color>|                                |</color><color=gateb_color>                          </color><color=helipad_color>|                       |</color>
       <color=gateahole_color>|                                |</color><color=gateb_color>                          </color><color=helipad_color>`‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾</color><color=cassieroom_color>|     |</color>
       <color=gateahole_color>`‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾‾`</color>                                            <color=cassieroom_color>|     |</color>
                                                                                     <color=cassieroom_color>|     |</color>
                                                                                     <color=cassieroom_color>`‾‾‾‾‾`</color>
";
            if (ultimate)
            {
                tmp = tmp.Replace("gatea_color", surfaceScanGateA ? "red" : "green");
                tmp = tmp.Replace("gateahole_color", surfaceScanGateAHole ? "red" : "green");
                tmp = tmp.Replace("nuke_color", surfaceScanNuke ? "red" : "green");
                tmp = tmp.Replace("gateb_color", surfaceScanGateB ? "red" : "green");
                tmp = tmp.Replace("helipad_color", surfaceScanHelipad ? "red" : "green");
                tmp = tmp.Replace("escape_color", surfaceScanEscape ? "red" : "green");
                tmp = tmp.Replace("cassieroom_color", surfaceScanCassieRoom ? "red" : "green");

                return tmp.Split('\n');
            }
            else
            {
                tmp = tmp.Replace("gatea_color", "green");
                tmp = tmp.Replace("gateahole_color", "green");
                tmp = tmp.Replace("nuke_color", "green");
                tmp = tmp.Replace("gateb_color", "green");
                tmp = tmp.Replace("helipad_color", "green");
                tmp = tmp.Replace("escape_color", "green");
                tmp = tmp.Replace("cassieroom_color", "green");

                return tmp.Split('\n');
            }
        }

        /// <summary>
        /// Generates EZ and HCZ map, using <paramref name="currentRoom"/> as players room.
        /// </summary>
        /// <param name="currentRoom">Room to be highlithed on white.</param>
        /// <param name="ultimate">If <see langword="true"/> then map will contain SNavUltimate elements.</param>
        /// <returns>EZ and HCZ Map.</returns>
        public static string[] GenerateEZ_HCZSNav(Room currentRoom, bool ultimate = false)
        {
            var rooms = EZ_HCZRooms;
            string[] toWrite = new string[rooms.GetLength(0) * 3];
            for (int z = 0; z < rooms.GetLength(0); z++)
            {
                for (int x = 0; x < rooms.GetLength(1); x++)
                {
                    string color = "green";
                    string name = "  END  ";
                    var room = rooms[z, x];
                    var tmp = GetRoomString(GetRoomType(room));
                    if (room == null)
                    {
                        toWrite[(z * 3) + 0] += tmp[0];
                        toWrite[(z * 3) + 1] += tmp[1];
                        toWrite[(z * 3) + 2] += tmp[2];
                        continue;
                    }

                    if (currentRoom == room)
                        color = "white";
                    else if (Warhead.IsInProgress)
                    {
                        if (room.Type == RoomType.HczNuke || room.Type == RoomType.EzGateA || room.Type == RoomType.EzGateB || room.Type == RoomType.LczChkpA || room.Type == RoomType.LczChkpB)
                            color = "red";
                    }
                    else if (MapPlus.IsLCZDecontaminated(35))
                    {
                        if (room.Type == RoomType.LczChkpA || room.Type == RoomType.LczChkpB)
                            color = "red";
                    }

                    if (Generator079.Generators.Any(g => g.CurRoom == room.Name && g.NetworkremainingPowerup > 0f))
                    {
                        var gen = Generator079.Generators.Find(g => g.CurRoom == room.Name);
                        if (gen.NetworkisTabletConnected)
                            color = "yellow";
                        else
                            color = "blue";
                    }

                    if (ultimate)
                    {
                        if (LastScan.Contains(room) && currentRoom != room)
                            color = "red";

                        switch (room.Type)
                        {
                            case RoomType.EzGateA:
                                name = "GATE  A";
                                break;
                            case RoomType.EzGateB:
                                name = "GATE  B";
                                break;
                            case RoomType.Hcz106:
                                name = "SCP 106";
                                break;
                            case RoomType.Hcz079:
                                name = "SCP 079";
                                break;
                            case RoomType.Hcz096:
                                name = "SCP 096";
                                break;
                            case RoomType.Lcz012:
                                name = "SCP 012";
                                break;
                            case RoomType.Lcz914:
                                name = "SCP 914";
                                break;
                            case RoomType.Lcz173:
                                name = "SCP 173";
                                break;
                            case RoomType.LczGlassBox:
                                name = "SCP 372";
                                break;
                            case RoomType.LczCafe:
                                name = "   PC  ";
                                break;
                            case RoomType.LczArmory:
                                name = "ARMORY ";
                                break;
                        }
                    }

                    toWrite[(z * 3) + 0] += $"<color={color}>" + tmp[0] + "</color>";
                    toWrite[(z * 3) + 1] += $"<color={color}>" + tmp[1].Replace("  END  ", name) + "</color>";
                    toWrite[(z * 3) + 2] += $"<color={color}>" + tmp[2] + "</color>";
                }
            }

            return toWrite;
        }

        /// <summary>
        /// Generates LCZ map, using <paramref name="currentRoom"/> as players room.
        /// </summary>
        /// <param name="currentRoom">Room to be highlithed on white.</param>
        /// <param name="ultimate">If <see langword="true"/> then map will contain SNavUltimate elements.</param>
        /// <returns>LCZ Map.</returns>
        public static string[] GenerateLCZSNav(Room currentRoom, bool ultimate = false)
        {
            var rooms = LCZRooms;
            string[] toWrite = new string[rooms.GetLength(0) * 3];
            for (int z = 0; z < rooms.GetLength(0); z++)
            {
                for (int x = 0; x < rooms.GetLength(1); x++)
                {
                    string color = "green";
                    string name = "  END  ";
                    var room = rooms[z, x];
                    var tmp = GetRoomString(GetRoomType(room));
                    if (room == null)
                    {
                        toWrite[(z * 3) + 0] += tmp[0];
                        toWrite[(z * 3) + 1] += tmp[1];
                        toWrite[(z * 3) + 2] += tmp[2];
                        continue;
                    }

                    if (currentRoom == room)
                        color = "white";
                    else if (Warhead.IsInProgress)
                    {
                        if (room.Type == RoomType.HczNuke || room.Type == RoomType.EzGateA || room.Type == RoomType.EzGateB || room.Type == RoomType.LczChkpA || room.Type == RoomType.LczChkpB)
                            color = "red";
                    }
                    else if (MapPlus.IsLCZDecontaminated(35))
                    {
                        if (room.Type == RoomType.LczChkpA || room.Type == RoomType.LczChkpB)
                            color = "red";
                    }

                    if (Generator079.Generators.Any(g => g.CurRoom == room.Name && g.NetworkremainingPowerup > 0f))
                    {
                        var gen = Generator079.Generators.Find(g => g.CurRoom == room.Name);
                        if (gen.NetworkisTabletConnected)
                            color = "yellow";
                        else
                            color = "blue";
                    }

                    if (ultimate)
                    {
                        if (LastScan.Contains(room) && currentRoom != room)
                            color = "red";

                        switch (room.Type)
                        {
                            case RoomType.EzGateA:
                                name = "GATE  A";
                                break;
                            case RoomType.EzGateB:
                                name = "GATE  B";
                                break;
                            case RoomType.Hcz106:
                                name = "SCP 106";
                                break;
                            case RoomType.Hcz079:
                                name = "SCP 079";
                                break;
                            case RoomType.Hcz096:
                                name = "SCP 096";
                                break;
                            case RoomType.Lcz012:
                                name = "SCP 012";
                                break;
                            case RoomType.Lcz914:
                                name = "SCP 914";
                                break;
                            case RoomType.Lcz173:
                                name = "SCP 173";
                                break;
                            case RoomType.LczGlassBox:
                                name = "SCP 372";
                                break;
                            case RoomType.LczCafe:
                                name = "   PC  ";
                                break;
                            case RoomType.LczArmory:
                                name = "ARMORY ";
                                break;
                        }
                    }

                    toWrite[(z * 3) + 0] += $"<color={color}>" + tmp[0] + "</color>";
                    toWrite[(z * 3) + 1] += $"<color={color}>" + tmp[1].Replace("  END  ", name) + "</color>";
                    toWrite[(z * 3) + 2] += $"<color={color}>" + tmp[2] + "</color>";
                }
            }

            return toWrite;
        }

        /// <summary>
        /// Returns rooms based on <paramref name="pos"/>.
        /// </summary>
        /// <param name="pos">Position.</param>
        /// <returns>Rooms.</returns>
        public static Room[,] GetRooms(float pos)
        {
            switch (pos)
            {
                case float x when x < -900 && x > -1100:
                    return EZ_HCZRooms;
                case float x when x < 100 && x > -100:
                    return LCZRooms;
                default:
                    return new Room[0, 0];
            }
        }

        /// <summary>
        /// Returns room rotation.
        /// </summary>
        /// <param name="room">Room.</param>
        /// <returns>Rotation.</returns>
        public static Rotation GetRotateion(Room room)
        {
            if (room.transform.localEulerAngles.y == 0)
                return Rotation.RIGHT;
            else if (room.transform.localEulerAngles.y == 90)
                return Rotation.DOWN;
            else if (room.transform.localEulerAngles.y == 180)
                return Rotation.LEFT;
            else if (room.transform.localEulerAngles.y == 270)
                return Rotation.UP;
            else
            {
                Log.Error(room.transform.localEulerAngles.y);
                return (Rotation)(-99);
            }
        }

        /// <summary>
        /// Returns room type.
        /// </summary>
        /// <param name="room">Room.</param>
        /// <returns>Room type.</returns>
        public static SNavRoomType GetRoomType(Room room)
        {
            switch (room?.Type)
            {
                case RoomType.EzCafeteria:
                case RoomType.EzConference:
                case RoomType.EzDownstairsPcs:
                case RoomType.EzPcs:
                case RoomType.EzStraight:

                case RoomType.HczServers:
                case RoomType.Unknown:

                case RoomType.LczAirlock:
                case RoomType.LczPlants:
                case RoomType.LczToilets:
                case RoomType.LczStraight:
                    {
                        var tmp = (Rotation)((int)GetRotateion(room) + (room.Zone == ZoneType.Entrance ? (int)offsetCheckpoint : (int)offsetClassD));
                        if ((int)tmp > 3)
                            tmp -= 4;
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.HS_LR;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.HS_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.EzIntercom:
                case RoomType.EzCurve:

                case RoomType.HczCurve:

                case RoomType.LczCurve:
                    {
                        var tmp = (Rotation)((int)GetRotateion(room) + (room.Zone == ZoneType.Entrance ? (int)offsetCheckpoint : (int)offsetClassD));
                        if ((int)tmp > 3)
                            tmp -= 4;
                        switch (tmp)
                        {
                            case Rotation.UP:
                                return SNavRoomType.HC_LT;
                            case Rotation.RIGHT:
                                return SNavRoomType.HC_RT;
                            case Rotation.DOWN:
                                return SNavRoomType.HC_RB;
                            case Rotation.LEFT:
                                return SNavRoomType.HC_LB;
                            default:
                                return SNavRoomType.ERROR;
                        }
                    }

                case RoomType.HczTCross:
                case RoomType.HczArmory:

                case RoomType.LczTCross:
                    {
                        var tmp = (Rotation)((int)GetRotateion(room) + (room.Zone == ZoneType.Entrance ? (int)offsetCheckpoint : (int)offsetClassD));
                        if ((int)tmp > 3)
                            tmp -= 4;
                        switch (tmp)
                        {
                            case Rotation.UP:
                                return SNavRoomType.IT_TB_R;
                            case Rotation.RIGHT:
                                return SNavRoomType.IT_RL_B;
                            case Rotation.DOWN:
                                return SNavRoomType.IT_TB_L;
                            case Rotation.LEFT:
                                return SNavRoomType.IT_RL_T;
                            default:
                                return SNavRoomType.ERROR;
                        }
                    }

                case RoomType.EzCrossing:

                case RoomType.HczCrossing:

                case RoomType.LczCrossing:
                    {
                        return SNavRoomType.IX;
                    }

                case RoomType.EzGateA:
                case RoomType.EzGateB:
                case RoomType.EzCollapsedTunnel:
                case RoomType.EzShelter:
                case RoomType.EzVent:

                case RoomType.Hcz106:
                case RoomType.Hcz096:
                case RoomType.Hcz079:

                case RoomType.LczGlassBox:
                case RoomType.LczCafe:
                case RoomType.LczArmory:
                case RoomType.Lcz914:
                case RoomType.Lcz173:
                case RoomType.Lcz012:
                    {
                        var tmp = (Rotation)(((int)GetRotateion(room) + (room.Zone == ZoneType.Entrance ? (int)offsetCheckpoint : (int)offsetClassD)) % 4);
                        if ((int)tmp > 3)
                            tmp -= 4;
                        switch (tmp)
                        {
                            case Rotation.UP:
                                return SNavRoomType.END_R;
                            case Rotation.RIGHT:
                                return SNavRoomType.END_B;
                            case Rotation.DOWN:
                                return SNavRoomType.END_L;
                            case Rotation.LEFT:
                                return SNavRoomType.END_T;
                            default:
                                return SNavRoomType.ERROR;
                        }
                    }

                case RoomType.LczClassDSpawn:
                    return SNavRoomType.CLASSD;

                case RoomType.Hcz939:
                    {
                        var tmp = (Rotation)((int)(GetRotateion(room) + (int)offsetClassD) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.SCP_939_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.SCP_939_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.HczEzCheckpoint:
                    {
                        var tmp = (Rotation)((int)(GetRotateion(room) + (int)offsetClassD) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.EZ_HCZ_CHECKPOINT_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.EZ_HCZ_CHECKPOINT_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.HczNuke:
                    {
                        var tmp = (Rotation)((int)(GetRotateion(room) + (int)offsetClassD) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.NUKE_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.NUKE_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.Hcz049:
                    {
                        var tmp = (Rotation)((int)(GetRotateion(room) + (int)offsetClassD) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.SCP049_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.SCP049_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.HczHid:
                    {
                        var tmp = (Rotation)((int)(GetRotateion(room) + (int)offsetClassD) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.HID_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.HID_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.LczChkpA:
                case RoomType.HczChkpA:
                    {
                        var tmp = (Rotation)((int)(GetRotateion(room) + (int)offsetClassD) % 4);
                        switch (tmp)
                        {
                            case Rotation.UP:
                                return SNavRoomType.LCZ_A_L;
                            case Rotation.RIGHT:
                                return SNavRoomType.LCZ_A_B;
                            case Rotation.DOWN:
                                return SNavRoomType.LCZ_A_R;
                            case Rotation.LEFT:
                                return SNavRoomType.LCZ_A_T;
                            default:
                                return SNavRoomType.ERROR;
                        }
                    }

                case RoomType.LczChkpB:
                case RoomType.HczChkpB:
                    {
                        var tmp = (Rotation)((int)(GetRotateion(room) + (int)offsetClassD) % 4);
                        switch (tmp)
                        {
                            case Rotation.UP:
                                return SNavRoomType.LCZ_B_L;
                            case Rotation.RIGHT:
                                return SNavRoomType.LCZ_B_B;
                            case Rotation.DOWN:
                                return SNavRoomType.LCZ_B_R;
                            case Rotation.LEFT:
                                return SNavRoomType.LCZ_B_T;
                            default:
                                return SNavRoomType.ERROR;
                        }
                    }

                case RoomType.EzUpstairsPcs:
                    {
                        var tmp = (Rotation)((int)(GetRotateion(room) + (int)offsetCheckpoint) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.COMPUTERS_UPSTAIRS_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.COMPUTERS_UPSTAIRS_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.HczTesla:
                    {
                        var tmp = (Rotation)((int)GetRotateion(room) + (room.Zone == ZoneType.Entrance ? (int)offsetCheckpoint : (int)offsetClassD));
                        if ((int)tmp > 3)
                            tmp -= 4;
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.TESLA_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.TESLA_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                default:
                    return SNavRoomType.NONE;
            }
        }

        /// <summary>
        /// Returns room preset.
        /// </summary>
        /// <param name="type">Room Type.</param>
        /// <returns>Preset.</returns>
        public static string[] GetRoomString(SNavRoomType type) => Presets[type];

        /// <inheritdoc cref="Module.Module(Exiled.API.Interfaces.IPlugin{Exiled.API.Interfaces.IConfig})"/>
        public SNavHandler(PluginHandler p)
            : base(p)
        {
            Log = base.Log;
            Instance = this;
            new SNavClasicItem();
            new SNavUltimateItem();
        }

        /// <summary>
        /// Room types.
        /// </summary>
        public enum SNavRoomType
        {
#pragma warning disable CS1591
            ERROR,
            NONE,
            HS_TB,
            HS_LR,
            HC_LT,
            HC_LB,
            HC_RB,
            HC_RT,
            IT_RL_T,
            IT_RL_B,
            IT_TB_L,
            IT_TB_R,
            IX,
            SCP_939_TB,
            SCP_939_RL,
            EZ_HCZ_CHECKPOINT_TB,
            EZ_HCZ_CHECKPOINT_RL,

            LCZ_A_R,
            LCZ_A_T,
            LCZ_A_B,
            LCZ_A_L,

            LCZ_B_R,
            LCZ_B_T,
            LCZ_B_B,
            LCZ_B_L,

            NUKE_TB,
            NUKE_RL,

            SCP049_TB,
            SCP049_RL,

            HID_TB,
            HID_RL,

            COMPUTERS_UPSTAIRS_TB,
            COMPUTERS_UPSTAIRS_RL,

            END_T,
            END_B,
            END_R,
            END_L,

            CLASSD,

            TESLA_TB,
            TESLA_RL,
#pragma warning restore CS1591
        }

        /// <summary>
        /// Room rotation.
        /// </summary>
        public enum Rotation
        {
#pragma warning disable CS1591
            UP,
            RIGHT,
            DOWN,
            LEFT,
#pragma warning restore CS1591
        }

        /// <inheritdoc/>
        public override string Name => "SNavHandler";

        /// <inheritdoc/>
        public override void OnDisable()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= this.Handle(() => this.Server_WaitingForPlayers(), "WaitingForPlayers");
            Exiled.Events.Handlers.Server.RoundStarted -= this.Handle(() => this.Server_RoundStarted(), "RoundStart");
            Exiled.Events.Handlers.Player.InsertingGeneratorTablet -= this.Handle<Exiled.Events.EventArgs.InsertingGeneratorTabletEventArgs>((ev) => this.Player_InsertingGeneratorTablet(ev));
            Exiled.Events.Handlers.Player.ActivatingWorkstation -= this.Handle<Exiled.Events.EventArgs.ActivatingWorkstationEventArgs>((ev) => this.Player_ActivatingWorkstation(ev));
            Exiled.Events.Handlers.Player.DeactivatingWorkstation -= this.Handle<Exiled.Events.EventArgs.DeactivatingWorkstationEventArgs>((ev) => this.Player_DeactivatingWorkstation(ev));
        }

        /// <inheritdoc/>
        public override void OnEnable()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers += this.Handle(() => this.Server_WaitingForPlayers(), "WaitingForPlayers");
            Exiled.Events.Handlers.Server.RoundStarted += this.Handle(() => this.Server_RoundStarted(), "RoundStart");
            Exiled.Events.Handlers.Player.InsertingGeneratorTablet += this.Handle<Exiled.Events.EventArgs.InsertingGeneratorTabletEventArgs>((ev) => this.Player_InsertingGeneratorTablet(ev));
            Exiled.Events.Handlers.Player.ActivatingWorkstation += this.Handle<Exiled.Events.EventArgs.ActivatingWorkstationEventArgs>((ev) => this.Player_ActivatingWorkstation(ev));
            Exiled.Events.Handlers.Player.DeactivatingWorkstation += this.Handle<Exiled.Events.EventArgs.DeactivatingWorkstationEventArgs>((ev) => this.Player_DeactivatingWorkstation(ev));
        }

        /// <inheritdoc/>
        public class SNavClasicItem : CustomItem
        {
            /// <inheritdoc cref="CustomItem.CustomItem"/>
            public SNavClasicItem() => this.Register();

            /// <inheritdoc/>
            public override string ItemName => "SNav-3000";

            /// <inheritdoc/>
            public override ItemType Item => ItemType.WeaponManagerTablet;

            /// <inheritdoc/>
            public override SessionVarType SessionVarType => SessionVarType.CI_SNAV;

            /// <inheritdoc/>
            public override int Durability => 301;

            /// <inheritdoc/>
            public override Vector3 Size => new Vector3(2.0f, .50f, .50f);

            /// <inheritdoc/>
            public override Upgrade[] Upgrades => new Upgrade[]
            {
                new Upgrade
                {
                    Input = ItemType.WeaponManagerTablet,
                    Durability = 0,
                    Chance = 100,
                    KnobSetting = Scp914Knob.OneToOne,
                },
            };

            /// <inheritdoc/>
            public override void OnStartHolding(Player player, Inventory.SyncItemInfo item)
            {
                PseudoGUIHandler.Ignore(player);
                RequireUpdate.Add(player);
                UpdateInterface(player);
                Instance.RunCoroutine(IUpdateInterface(player), "SNav.IUpdateInterface");
            }

            /// <inheritdoc/>
            public override void OnStopHolding(Player player, Inventory.SyncItemInfo item)
            {
                player.ShowHint(string.Empty, 1); // Clear Hints
                PseudoGUIHandler.StopIgnore(player);
                UpdateVisibility(player, true);
            }

            /// <inheritdoc/>
            public override Pickup OnUpgrade(Pickup pickup, Scp914Knob setting)
            {
                if (setting == Scp914Knob.Fine || setting == Scp914Knob.VeryFine)
                    pickup.durability = 401000f;
                return pickup;
            }
        }

        /// <inheritdoc/>
        public class SNavUltimateItem : CustomItem
        {
            /// <inheritdoc cref="CustomItem.CustomItem"/>
            public SNavUltimateItem() => this.Register();

            /// <inheritdoc/>
            public override string ItemName => "SNav-Ultimate";

            /// <inheritdoc/>
            public override ItemType Item => ItemType.WeaponManagerTablet;

            /// <inheritdoc/>
            public override SessionVarType SessionVarType => SessionVarType.CI_SNAV;

            /// <inheritdoc/>
            public override int Durability => 401;

            /// <inheritdoc/>
            public override Vector3 Size => new Vector3(2.5f, .75f, .75f);

            /// <inheritdoc/>
            public override void OnStartHolding(Player player, Inventory.SyncItemInfo item)
            {
                PseudoGUIHandler.Ignore(player);
                RequireUpdate.Add(player);
                UpdateInterface(player);
                Instance.RunCoroutine(IUpdateInterface(player), "SNav.IUpdateInterface");
            }

            /// <inheritdoc/>
            public override void OnStopHolding(Player player, Inventory.SyncItemInfo item)
            {
                player.ShowHint(string.Empty, 1); // Clear Hints
                PseudoGUIHandler.StopIgnore(player);
                UpdateVisibility(player, true);
            }
        }

        private static readonly Dictionary<WorkStation, Inventory.SyncItemInfo> Workstations = new Dictionary<WorkStation, Inventory.SyncItemInfo>();

        private static readonly HashSet<Player> RequireUpdate = new HashSet<Player>();
        private static readonly Dictionary<Player, Room> LastRooms = new Dictionary<Player, Room>();

        private static Rotation offsetClassD = Rotation.UP;
        private static Rotation offsetCheckpoint = Rotation.UP;

        private static bool requireUpdateUltimate = false;

        private static bool surfaceScanGateA = false;
        private static bool surfaceScanGateAHole = false;
        private static bool surfaceScanNuke = false;
        private static bool surfaceScanGateB = false;
        private static bool surfaceScanHelipad = false;
        private static bool surfaceScanEscape = false;
        private static bool surfaceScanCassieRoom = false;

        private static new ModuleLogger Log { get; set; }

        private static SNavHandler Instance { get; set; }

        private static void UpdateVisibility(Player p, bool visible)
        {
            if (!HideTablet)
                return;
            var old = p.ReferenceHub.transform.localScale;
            p.ReferenceHub.transform.localScale = new Vector3(visible ? p.ReferenceHub.transform.localScale.x : 0, p.ReferenceHub.transform.localScale.y, p.ReferenceHub.transform.localScale.z);
            var sendSpawnMessage = Server.SendSpawnMessage;
            if (sendSpawnMessage != null)
            {
                sendSpawnMessage.Invoke(
                    null,
                    new object[]
                    {
                        p.ReferenceHub.characterClassManager.netIdentity,
                        p.Connection,
                    });
            }

            p.ReferenceHub.transform.localScale = old;
        }

        private static IEnumerator<float> IUpdateInterface(Player player)
        {
            yield return Timing.WaitForSeconds(0.5f);
            int i = 1;
            UpdateVisibility(player, false);
            while (player.CurrentItem.id == ItemType.WeaponManagerTablet)
            {
                if (!LastRooms.TryGetValue(player, out Room lastRoom) || lastRoom != player.CurrentRoom)
                {
                    LastRooms[player] = player.CurrentRoom;
                    RequireUpdate.Add(player);
                    i = 20;
                }

                if (i >= 19 || RequireUpdate.Contains(player) || (requireUpdateUltimate && player.CurrentItem.durability == 401000f))
                {
                    UpdateInterface(player);
                    RequireUpdate.Remove(player);
                    i = 0;
                }
                else
                    i++;

                yield return Timing.WaitForSeconds(0.5f);
            }

            UpdateVisibility(player, true);
        }

        private static void UpdateInterface(Player player)
        {
            bool ultimate;
            if (player.CurrentItem.durability == 401000f)
                ultimate = true;
            else if (player.CurrentItem.durability == 301000f)
                ultimate = false;
            else
                return;
            string[] toWrite;
            if (player.Position.y < -500 && player.Position.y > -700)
            {
                toWrite =
@"
  |‾‾‾‾‾‾‾|‾‾‾|‾‾|
__|  /‾‾‾‾|   '  |
|   | <color=red>X</color>   |   |  |
‾‾|  \____|   |‾‾'
  |___________|
".Split('\n');
            }
            else if (player.Position.y < -700 && player.Position.y > -800)
            {
                toWrite =
@"
      _____________
     |      `     |
     | .    ,     |
.___.| .  |‾‾‾|   |
|   || .      |   |
| |‾`|__. |   |   |
| |_____| |‾‾‾‾‾‾‾`
|         |
‾‾‾‾‾‾‾‾‾‾`
".Split('\n');
            }
            else if (player.Position.y > 800)
            {
                toWrite = GenerateSurfaceSNav();
            }
            else
            {
                switch (player.CurrentRoom.Position.y)
                {
                    case float x when x > -100 && x < 100:
                        toWrite = GenerateLCZSNav(player.CurrentRoom, ultimate);
                        break;
                    case float x when x > -1100 && x < -900:
                        toWrite = GenerateEZ_HCZSNav(player.CurrentRoom, ultimate);
                        break;
                    default:
                        toWrite = new string[] { "ERROR, UNKNOWN ROOM: " + player.CurrentRoom.Position.y };
                        break;
                }
            }

            var list = NorthwoodLib.Pools.ListPool<string>.Shared.Rent(toWrite);
            list.RemoveAll(i => string.IsNullOrWhiteSpace(i));
            while (list.Count < 65)
                list.Insert(0, string.Empty);
            player.ShowHint("<voffset=25em><color=green><size=25%><align=left><mspace=0.5em>" + string.Join("<br>", list) + "</mspace></align></size></color></voffset>", 11);
            NorthwoodLib.Pools.ListPool<string>.Shared.Return(list);
        }

        private void Player_ActivatingWorkstation(Exiled.Events.EventArgs.ActivatingWorkstationEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;
            var first = ev.Player.Items.FirstOrDefault(i => i.id == ItemType.WeaponManagerTablet);
            if (first.durability >= 301000f)
            {
                Workstations[ev.Workstation] = first;
                ev.Player.ShowHint(string.Empty, 1); // Clear Hints
                PseudoGUIHandler.StopIgnore(ev.Player);
                UpdateVisibility(ev.Player, true);
            }
        }

        private void Player_DeactivatingWorkstation(Exiled.Events.EventArgs.DeactivatingWorkstationEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;
            if (Workstations.TryGetValue(ev.Workstation, out var snav))
            {
                ev.Player.AddItem(snav);
                Workstations.Remove(ev.Workstation);
                ev.IsAllowed = false;
                ev.Workstation.NetworkisTabletConnected = false;
                ev.Workstation.Network_playerConnected = null;
            }
        }

        private void Player_InsertingGeneratorTablet(Exiled.Events.EventArgs.InsertingGeneratorTabletEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;
            var first = ev.Player.Inventory.items.FirstOrDefault(i => i.id == ItemType.WeaponManagerTablet);
            if (first.durability >= 301000f)
            {
                GeneratorPatch.Generators[ev.Generator] = first;
                ev.Player.ShowHint(string.Empty, 1); // Clear Hints
                PseudoGUIHandler.StopIgnore(ev.Player);
                UpdateVisibility(ev.Player, true);
            }
        }

        private void Server_RoundStarted()
        {
            this.RunCoroutine(this.DoRoundLoop(), "DoRoundLoop");
        }

        private IEnumerator<float> DoRoundLoop()
        {
            var initOne = SpawnSNAV(true, Vector3.zero);
            this.CallDelayed(5, () => initOne.Delete(), "Remove SNav");
            yield return Timing.WaitForSeconds(1);
            foreach (var item in PluginHandler.Instance.Config.SNavUltimateSpawns)
            {
                string[] data = item.Split(',');
                if (data.Length < 4 || !float.TryParse(data[1], out float x) || !float.TryParse(data[2], out float y) || !float.TryParse(data[3], out float z))
                {
                    Log.Error($"Config Error | \"{item}\" is not correct SNav Spawn Data");
                    continue;
                }

                var door = Map.Doors.FirstOrDefault(i => i.Type().ToString() == data[0]);
                if (door == null)
                {
                    Log.Warn("Invalid Data, Unknown Door \"data[0]\"");
                    continue;
                }

                SpawnSNAV(true, door.transform.position + (door.transform.forward * x) + (door.transform.right * z) + (Vector3.up * y));
                yield return Timing.WaitForSeconds(0.1f);
            }

            foreach (var item in PluginHandler.Instance.Config.SNav3000Spawns)
            {
                string[] data = item.Split(',');
                if (data.Length < 4 || !float.TryParse(data[1], out float x) || !float.TryParse(data[2], out float y) || !float.TryParse(data[3], out float z))
                {
                    Log.Error($"Config Error | \"{item}\" is not correct SNav Spawn Data");
                    continue;
                }

                var door = Map.Doors.FirstOrDefault(i => i.Type().ToString() == data[0]);
                if (door == null)
                {
                    Log.Warn($"Invalid Data, Unknown Door \"{data[0]}\"");
                    continue;
                }

                SpawnSNAV(false, door.transform.position + (door.transform.forward * x) + (door.transform.right * z) + (Vector3.up * y));
                yield return Timing.WaitForSeconds(0.1f);
            }

            yield return Timing.WaitForSeconds(300);
            int rid = RoundPlus.RoundId;
            while (Round.IsStarted && rid == RoundPlus.RoundId)
            {
                yield return Timing.WaitForSeconds(15);
                LastScan.Clear();
                surfaceScanGateA = false;
                surfaceScanGateAHole = false;
                surfaceScanNuke = false;
                surfaceScanGateB = false;
                surfaceScanHelipad = false;
                surfaceScanEscape = false;
                foreach (var player in RealPlayers.List.Where(p => p.IsAlive))
                {
                    if (player.CurrentRoom != null)
                        LastScan.Add(player.CurrentRoom);
                    if (player.Position.y > 900 && player.Position.y < 1012)
                    {
                        // 194 +900 -44 -> 147 +900 -44
                        if (player.Position.x <= 194 && player.Position.x >= 147)
                        {
                            if (player.Position.z >= -44)
                            {
                                surfaceScanEscape = true;
                                continue;
                            }
                            else if (player.Position.z <= -73)
                            {
                                surfaceScanCassieRoom = true;
                                continue;
                            }
                        }

                        if (player.Position.x >= 148)
                        {
                            surfaceScanHelipad = true;
                            continue;
                        }

                        if (player.Position.x >= 68)
                        {
                            surfaceScanGateB = true;
                            continue;
                        }

                        if (player.Position.x <= 42 && player.Position.x >= 37 && player.Position.z <= -32 && player.Position.z >= -38)
                        {
                            surfaceScanNuke = true;
                            continue;
                        }

                        if (player.Position.z >= -21)
                        {
                            surfaceScanGateA = true;
                            continue;
                        }

                        surfaceScanGateAHole = true;
                        continue;
                    }
                }

                requireUpdateUltimate = true;
                yield return Timing.WaitForSeconds(1);
                requireUpdateUltimate = false;
            }
        }

        private void Server_WaitingForPlayers()
        {
            try
            {
                offsetClassD = GetRotateion(Map.Rooms.First(r => r.Type == RoomType.LczClassDSpawn));
                offsetCheckpoint = (Rotation)(((int)GetRotateion(Map.Rooms.First(r => r.Type == RoomType.HczEzCheckpoint)) + (int)offsetClassD) % 4);

                List<int> zAxis = new List<int>();
                List<int> xAxis = new List<int>();
                try
                {
                    foreach (var item in Map.Rooms.Where(r => r.Zone == ZoneType.LightContainment))
                    {
                        int z = (int)Math.Floor(item.Position.z);
                        int x = (int)Math.Floor(item.Position.x);
                        if (!zAxis.Contains(z))
                            zAxis.Add(z);
                        if (!xAxis.Contains(x))
                            xAxis.Add(x);
                    }
                }
                catch (System.Exception ex)
                {
                    Log.Error("CatchId: 3");
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                }

                xAxis.Sort();
                zAxis.Sort();
                zAxis.Reverse();
                try
                {
                    LCZRooms = new Room[zAxis.Count, xAxis.Count];
                    for (int i = 0; i < zAxis.Count; i++)
                    {
                        try
                        {
                            var z = zAxis[i];
                            List<Room> roomsList = new List<Room>();
                            for (int j = 0; j < xAxis.Count; j++)
                            {
                                try
                                {
                                    var x = xAxis[j];
                                    LCZRooms[i, j] = Map.Rooms.Where(r => r.Zone == ZoneType.LightContainment).FirstOrDefault(p => (int)Math.Floor(p.Position.z) == z && (int)Math.Floor(p.Position.x) == x);
                                }
                                catch (System.Exception ex)
                                {
                                    Log.Error("CatchId: 4.2");
                                    Log.Error(ex.Message);
                                    Log.Error(ex.StackTrace);
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Log.Error("CatchId: 4.1");
                            Log.Error(ex.Message);
                            Log.Error(ex.StackTrace);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Log.Error("CatchId: 4");
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                }

                zAxis.Clear();
                xAxis.Clear();
                try
                {
                    foreach (var item in Map.Rooms.Where(r => (r.Zone == ZoneType.HeavyContainment || r.Zone == ZoneType.Entrance) && r.Type != RoomType.Pocket))
                    {
                        try
                        {
                            int z = (int)Math.Floor(item.Position.z);
                            int x = (int)Math.Floor(item.Position.x);
                            if (!zAxis.Contains(z))
                                zAxis.Add(z);
                            if (!xAxis.Contains(x))
                                xAxis.Add(x);
                        }
                        catch (System.Exception ex)
                        {
                            Log.Error("CatchId: 5.1");
                            Log.Error(ex.Message);
                            Log.Error(ex.StackTrace);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Log.Error("CatchId: 5");
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                }

                xAxis.Sort();
                zAxis.Sort();
                try
                {
                    for (int i = 0; i < xAxis.Count; i++)
                    {
                        try
                        {
                            var x = xAxis[i];
                            if (!Map.Rooms.Where(r => (r.Zone == ZoneType.HeavyContainment || r.Zone == ZoneType.Entrance) && r.Type != RoomType.Pocket).Any(p => (int)Math.Floor(p.Position.x) == x))
                            {
                                xAxis.RemoveAt(i);
                                i--;
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Log.Error("CatchId: 6.1");
                            Log.Error(ex.Message);
                            Log.Error(ex.StackTrace);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Log.Error("CatchId: 6");
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                }

                zAxis.Reverse();
                EZ_HCZRooms = new Room[zAxis.Count, xAxis.Count];
                try
                {
                    for (int i = 0; i < zAxis.Count; i++)
                    {
                        try
                        {
                            var z = zAxis[i];
                            List<Room> roomsList = new List<Room>();
                            for (int j = 0; j < xAxis.Count; j++)
                            {
                                try
                                {
                                    var x = xAxis[j];
                                    EZ_HCZRooms[i, j] = Map.Rooms.Where(r => (r.Zone == ZoneType.HeavyContainment || r.Zone == ZoneType.Entrance) && r.Type != RoomType.Pocket).FirstOrDefault(p => (int)Math.Floor(p.Position.z) == z && (int)Math.Floor(p.Position.x) == x);
                                }
                                catch (System.Exception ex)
                                {
                                    Log.Error("CatchId: 7.2");
                                    Log.Error(ex.Message);
                                    Log.Error(ex.StackTrace);
                                }
                            }
                        }
                        catch (System.Exception ex)
                        {
                            Log.Error("CatchId: 7.1");
                            Log.Error(ex.Message);
                            Log.Error(ex.StackTrace);
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    Log.Error("CatchId: 7");
                    Log.Error(ex.Message);
                    Log.Error(ex.StackTrace);
                }
            }
            catch (System.Exception ex)
            {
                Log.Error("CatchId: 0");
                Log.Error(ex.Message);
                Log.Error(ex.StackTrace);
                this.CallDelayed(5, this.Server_WaitingForPlayers, "Retry.Server_WaitingForPlayers");
            }
        }
    }
}
