// -----------------------------------------------------------------------
// <copyright file="SNavHandler.cs" company="Mistaken">
// Copyright (c) Mistaken. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.API.Features.Attributes;
using Exiled.API.Features.Items;
using Exiled.API.Features.Spawn;
using Exiled.API.Interfaces;
using Exiled.CustomItems.API.EventArgs;
using Exiled.CustomItems.API.Features;
using MEC;
using Mistaken.API;
using Mistaken.API.CustomItems;
using Mistaken.API.Diagnostics;
using Mistaken.API.GUI;
using Scp914;
using UnityEngine;

namespace Mistaken.SNav
{
    /// <inheritdoc/>
    public sealed class SNavHandler : Module
    {
        /// <summary>
        /// Rooms looks.
        /// </summary>
        public static readonly Dictionary<SNavRoomType, string[]> Presets = new()
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
        public static readonly HashSet<Room> LastScan = new();

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
                tmp = tmp.Replace("gatea_color", _surfaceScanGateA ? "red" : "green");
                tmp = tmp.Replace("gateahole_color", _surfaceScanGateAHole ? "red" : "green");
                tmp = tmp.Replace("nuke_color", _surfaceScanNuke ? "red" : "green");
                tmp = tmp.Replace("gateb_color", _surfaceScanGateB ? "red" : "green");
                tmp = tmp.Replace("helipad_color", _surfaceScanHelipad ? "red" : "green");
                tmp = tmp.Replace("escape_color", _surfaceScanEscape ? "red" : "green");
                tmp = tmp.Replace("cassieroom_color", _surfaceScanCassieRoom ? "red" : "green");

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
            var rooms = API.Utilities.Room.EZ_HCZ;
            string[] toWrite = new string[rooms.GetLength(0) * 3];
            for (int z = 0; z < rooms.GetLength(0); z++)
            {
                for (int x = 0; x < rooms.GetLength(1); x++)
                {
                    string color = "green";
                    string name = "  END  ";
                    var room = rooms[z, x];
                    var tmp = GetRoomString(GetRoomType(room?.ExiledRoom));
                    if (room == null)
                    {
                        toWrite[(z * 3) + 0] += tmp[0];
                        toWrite[(z * 3) + 1] += tmp[1];
                        toWrite[(z * 3) + 2] += tmp[2];
                        continue;
                    }

                    var roomType = room.ExiledRoom.Type;
                    if (currentRoom == room.ExiledRoom)
                        color = "white";
                    else if (Warhead.IsInProgress)
                    {
                        if (roomType == RoomType.HczNuke || roomType == RoomType.EzGateA || roomType == RoomType.EzGateB || roomType == RoomType.LczChkpA || roomType == RoomType.LczChkpB)
                            color = "red";
                    }
                    else if (MapPlus.IsLCZDecontaminated(35))
                    {
                        if (roomType == RoomType.LczChkpA || roomType == RoomType.LczChkpB)
                            color = "red";
                    }

                    if (ultimate)
                    {
                        if (LastScan.Contains(room.ExiledRoom) && currentRoom != room.ExiledRoom)
                            color = "red";

                        switch (roomType)
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
            var rooms = API.Utilities.Room.LCZ;
            string[] toWrite = new string[rooms.GetLength(0) * 3];
            for (int z = 0; z < rooms.GetLength(0); z++)
            {
                for (int x = 0; x < rooms.GetLength(1); x++)
                {
                    string color = "green";
                    string name = "  END  ";
                    var room = rooms[z, x];
                    var tmp = GetRoomString(GetRoomType(room?.ExiledRoom));
                    if (room == null)
                    {
                        toWrite[(z * 3) + 0] += tmp[0];
                        toWrite[(z * 3) + 1] += tmp[1];
                        toWrite[(z * 3) + 2] += tmp[2];
                        continue;
                    }

                    var roomType = room.ExiledRoom.Type;
                    if (currentRoom == room.ExiledRoom)
                        color = "white";
                    else if (Warhead.IsInProgress)
                    {
                        if (roomType == RoomType.HczNuke || roomType == RoomType.EzGateA || roomType == RoomType.EzGateB || roomType == RoomType.LczChkpA || roomType == RoomType.LczChkpB)
                            color = "red";
                    }
                    else if (MapPlus.IsLCZDecontaminated(35))
                    {
                        if (roomType == RoomType.LczChkpA || roomType == RoomType.LczChkpB)
                            color = "red";
                    }

                    if (ultimate)
                    {
                        if (LastScan.Contains(room.ExiledRoom) && currentRoom != room.ExiledRoom)
                            color = "red";

                        switch (roomType)
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
        /// Returns room rotation.
        /// </summary>
        /// <param name="room">Room.</param>
        /// <returns>Rotation.</returns>
        public static Rotation GetRotation(Room room)
        {
            var y = Math.Round(room.transform.localEulerAngles.y);
            if (y == 0)
                return Rotation.RIGHT;
            else if (y == 90)
                return Rotation.DOWN;
            else if (y == 180)
                return Rotation.LEFT;
            else if (y == 270)
                return Rotation.UP;
            else
            {
                Log.Error(y);
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
                case RoomType.HczStraight:
                case RoomType.Unknown:

                case RoomType.LczAirlock:
                case RoomType.LczPlants:
                case RoomType.LczToilets:
                case RoomType.LczStraight:
                    {
                        var tmp = (Rotation)(((int)GetRotation(room) + (room.Zone == ZoneType.Entrance ? (int)_offsetCheckpoint : (int)_offsetClassD)) % 4);
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
                        var tmp = (Rotation)(((int)GetRotation(room) + (room.Zone == ZoneType.Entrance ? (int)_offsetCheckpoint : (int)_offsetClassD)) % 4);
                        return tmp switch
                        {
                            Rotation.UP => SNavRoomType.HC_LT,
                            Rotation.RIGHT => SNavRoomType.HC_RT,
                            Rotation.DOWN => SNavRoomType.HC_RB,
                            Rotation.LEFT => SNavRoomType.HC_LB,
                            _ => SNavRoomType.ERROR,
                        };
                    }

                case RoomType.HczTCross:
                case RoomType.HczArmory:

                case RoomType.EzTCross:

                case RoomType.LczTCross:
                    {
                        var tmp = (Rotation)(((int)GetRotation(room) + (room.Zone == ZoneType.Entrance ? (int)_offsetCheckpoint : (int)_offsetClassD)) % 4);
                        return tmp switch
                        {
                            Rotation.UP => SNavRoomType.IT_TB_R,
                            Rotation.RIGHT => SNavRoomType.IT_RL_B,
                            Rotation.DOWN => SNavRoomType.IT_TB_L,
                            Rotation.LEFT => SNavRoomType.IT_RL_T,
                            _ => SNavRoomType.ERROR,
                        };
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
                        var tmp = (Rotation)(((int)GetRotation(room) + (room.Zone == ZoneType.Entrance ? (int)_offsetCheckpoint : (int)_offsetClassD)) % 4);
                        return tmp switch
                        {
                            Rotation.UP => SNavRoomType.END_R,
                            Rotation.RIGHT => SNavRoomType.END_B,
                            Rotation.DOWN => SNavRoomType.END_L,
                            Rotation.LEFT => SNavRoomType.END_T,
                            _ => SNavRoomType.ERROR,
                        };
                    }

                case RoomType.LczClassDSpawn:
                    return SNavRoomType.CLASSD;

                case RoomType.Hcz939:
                    {
                        var tmp = (Rotation)((int)(GetRotation(room) + (int)_offsetClassD) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.SCP_939_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.SCP_939_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.HczEzCheckpoint:
                    {
                        var tmp = (Rotation)((int)(GetRotation(room) + (int)_offsetClassD) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.EZ_HCZ_CHECKPOINT_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.EZ_HCZ_CHECKPOINT_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.HczNuke:
                    {
                        var tmp = (Rotation)((int)(GetRotation(room) + (int)_offsetClassD) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.NUKE_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.NUKE_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.Hcz049:
                    {
                        var tmp = (Rotation)((int)(GetRotation(room) + (int)_offsetClassD) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.SCP049_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.SCP049_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.HczHid:
                    {
                        var tmp = (Rotation)((int)(GetRotation(room) + (int)_offsetClassD) % 4);
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
                        var tmp = (Rotation)((int)(GetRotation(room) + (int)_offsetClassD) % 4);
                        return tmp switch
                        {
                            Rotation.UP => SNavRoomType.LCZ_A_L,
                            Rotation.RIGHT => SNavRoomType.LCZ_A_B,
                            Rotation.DOWN => SNavRoomType.LCZ_A_R,
                            Rotation.LEFT => SNavRoomType.LCZ_A_T,
                            _ => SNavRoomType.ERROR,
                        };
                    }

                case RoomType.LczChkpB:
                case RoomType.HczChkpB:
                    {
                        var tmp = (Rotation)((int)(GetRotation(room) + (int)_offsetClassD) % 4);
                        return tmp switch
                        {
                            Rotation.UP => SNavRoomType.LCZ_B_L,
                            Rotation.RIGHT => SNavRoomType.LCZ_B_B,
                            Rotation.DOWN => SNavRoomType.LCZ_B_R,
                            Rotation.LEFT => SNavRoomType.LCZ_B_T,
                            _ => SNavRoomType.ERROR,
                        };
                    }

                case RoomType.EzUpstairsPcs:
                    {
                        var tmp = (Rotation)((int)(GetRotation(room) + (int)_offsetCheckpoint) % 4);
                        if (tmp == Rotation.UP || tmp == Rotation.DOWN)
                            return SNavRoomType.COMPUTERS_UPSTAIRS_RL;
                        else if (tmp == Rotation.RIGHT || tmp == Rotation.LEFT)
                            return SNavRoomType.COMPUTERS_UPSTAIRS_TB;
                        else
                            return SNavRoomType.ERROR;
                    }

                case RoomType.HczTesla:
                    {
                        var tmp = (Rotation)(((int)GetRotation(room) + (room.Zone == ZoneType.Entrance ? (int)_offsetCheckpoint : (int)_offsetClassD)) % 4);
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

        /// <inheritdoc cref="Module(IPlugin{IConfig})"/>
        public SNavHandler(IPlugin<IConfig> p)
            : base(p)
        {
            Log = base.Log;
            Instance = this;
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
        public override string Name => nameof(SNavHandler);

        /// <inheritdoc/>
        public override void OnEnable()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers += this.Server_WaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted += this.Server_RoundStarted;
            Exiled.Events.Handlers.Scp914.UpgradingItem += this.Scp914_UpgradingItem;
        }

        /// <inheritdoc/>
        public override void OnDisable()
        {
            Exiled.Events.Handlers.Server.WaitingForPlayers -= this.Server_WaitingForPlayers;
            Exiled.Events.Handlers.Server.RoundStarted -= this.Server_RoundStarted;
            Exiled.Events.Handlers.Scp914.UpgradingItem -= this.Scp914_UpgradingItem;
        }

        /// <inheritdoc/>
        [CustomItem(ItemType.Radio)]
        public sealed class SNavClasicItem : MistakenCustomItem
        {
            /// <inheritdoc/>
            public override ItemType Type { get; set; } = ItemType.Radio;

            /// <inheritdoc/>
            public override MistakenCustomItems CustomItem => MistakenCustomItems.SNAV_3000;

            /// <inheritdoc/>
            public override string Name { get; set; } = "SNav-3000";

            /// <inheritdoc/>
            public override string Description { get; set; }

            /// <inheritdoc/>
            public override float Weight { get; set; } = 0.4f;

            /// <inheritdoc/>
            public override SpawnProperties SpawnProperties { get; set; }

            /// <inheritdoc/>
            public override Pickup Spawn(Vector3 position, Player previousOwner = null)
            {
                var pickup = base.Spawn(position, previousOwner);
                pickup.Scale = new Vector3(.50f, .50f, 2.0f);
                return pickup;
            }

            /// <inheritdoc/>
            public override Pickup Spawn(Vector3 position, Item item, Player previousOwner = null)
            {
                var pickup = base.Spawn(position, item, previousOwner);
                pickup.Scale = new Vector3(.50f, .50f, 2.0f);
                return pickup;
            }

            /// <inheritdoc/>
            protected override void ShowSelectedMessage(Player player)
            {
                Instance.RunCoroutine(IUpdateInterface(player), "SNav.IUpdateInterface");
            }

            /// <inheritdoc/>
            protected override void OnUpgrading(UpgradingEventArgs ev)
            {
                if (!ev.IsAllowed)
                    return;

                if (ev.KnobSetting == Scp914KnobSetting.Fine || ev.KnobSetting == Scp914KnobSetting.VeryFine)
                {
                    ev.Item.DestroySelf();
                    Get(MistakenCustomItems.SNAV_ULTIMATE).Spawn(ev.OutputPosition, previousOwner: null);
                }
            }
        }

        /// <inheritdoc/>
        [CustomItem(ItemType.Radio)]
        public sealed class SNavUltimateItem : MistakenCustomItem
        {
            /// <inheritdoc/>
            public override ItemType Type { get; set; } = ItemType.Radio;

            /// <inheritdoc/>
            public override MistakenCustomItems CustomItem => MistakenCustomItems.SNAV_ULTIMATE;

            /// <inheritdoc/>
            public override string Name { get; set; } = "SNav-Ultimate";

            /// <inheritdoc/>
            public override string Description { get; set; }

            /// <inheritdoc/>
            public override float Weight { get; set; } = 0.6f;

            /// <inheritdoc/>
            public override SpawnProperties SpawnProperties { get; set; }

            /// <inheritdoc/>
            public override Pickup Spawn(Vector3 position, Player previousOwner = null)
            {
                var pickup = base.Spawn(position, previousOwner);
                pickup.Scale = new Vector3(.75f, .75f, 2.5f);
                return pickup;
            }

            /// <inheritdoc/>
            public override Pickup Spawn(Vector3 position, Item item, Player previousOwner = null)
            {
                var pickup = base.Spawn(position, item, previousOwner);
                pickup.Scale = new Vector3(.75f, .75f, 2.5f);
                return pickup;
            }

            /// <inheritdoc/>
            protected override void ShowSelectedMessage(Player player)
            {
                Instance.RunCoroutine(IUpdateInterface(player), "SNav.IUpdateInterface");
            }
        }

        private static readonly HashSet<Player> _requireUpdate = new();
        private static readonly Dictionary<Player, Room> _lastRooms = new();

        private static Rotation _offsetClassD = Rotation.UP;
        private static Rotation _offsetCheckpoint = Rotation.UP;

        private static bool _requireUpdateUltimate = false;

        private static bool _surfaceScanGateA = false;
        private static bool _surfaceScanGateAHole = false;
        private static bool _surfaceScanNuke = false;
        private static bool _surfaceScanGateB = false;
        private static bool _surfaceScanHelipad = false;
        private static bool _surfaceScanEscape = false;
        private static bool _surfaceScanCassieRoom = false;

        private static new ModuleLogger Log { get; set; }

        private static SNavHandler Instance { get; set; }

        private static IEnumerator<float> IUpdateInterface(Player player)
        {
            int i = 1;
            var clasic = MistakenCustomItems.SNAV_3000.Get();
            var utlimate = MistakenCustomItems.SNAV_ULTIMATE.Get();

            PseudoGUIHandler.Ignore(player);
            _requireUpdate.Add(player);

            yield return Timing.WaitForSeconds(0.1f);

            while (clasic.Check(player.CurrentItem) || utlimate.Check(player.CurrentItem))
            {
                if (!_lastRooms.TryGetValue(player, out Room lastRoom) || lastRoom != player.CurrentRoom)
                {
                    _lastRooms[player] = player.CurrentRoom;
                    _requireUpdate.Add(player);
                    i = 20;
                }

                if (i >= 19 || _requireUpdate.Contains(player) || (_requireUpdateUltimate && utlimate.Check(player.CurrentItem)))
                {
                    UpdateInterface(player);
                    _requireUpdate.Remove(player);
                    i = 0;
                }
                else
                    i++;

                yield return Timing.WaitForSeconds(0.5f);
            }

            if (player.IsConnected)
                player.ShowHint(string.Empty, 1); // Clear Hints
            PseudoGUIHandler.StopIgnore(player);
        }

        private static void UpdateInterface(Player player)
        {
            if (!player.IsConnected)
                return;

            Log.Debug("A1", PluginHandler.Instance.Config.VerboseOutput);
            var clasicItem = MistakenCustomItems.SNAV_3000.Get();
            var utlimateItem = MistakenCustomItems.SNAV_ULTIMATE.Get();
            bool ultimate;
            if (utlimateItem.Check(player.CurrentItem))
                ultimate = true;
            else if (clasicItem.Check(player.CurrentItem))
                ultimate = false;
            else
                return;
            Log.Debug("A2", PluginHandler.Instance.Config.VerboseOutput);
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
                toWrite = player.CurrentRoom.Position.y switch
                {
                    float x when x > -100 && x < 100 => GenerateLCZSNav(player.CurrentRoom, ultimate),
                    float x when x > -1100 && x < -900 => GenerateEZ_HCZSNav(player.CurrentRoom, ultimate),
                    _ => new string[] { "ERROR, UNKNOWN ROOM: " + player.CurrentRoom.Position.y },
                };
            }

            Log.Debug("A3", PluginHandler.Instance.Config.VerboseOutput);

            var list = NorthwoodLib.Pools.ListPool<string>.Shared.Rent(toWrite);
            list.RemoveAll(i => string.IsNullOrWhiteSpace(i));
            while (list.Count < 30)
                list.Insert(0, string.Empty);
            player.ShowHint("<color=green><size=40%><align=left><mspace=0.5em>" + string.Join("<br>", list) + "</mspace></align></size></color>", 11);
            NorthwoodLib.Pools.ListPool<string>.Shared.Return(list);
        }

        private bool _lastDecont;
        private bool _lastWarhead;

        private void Server_RoundStarted()
        {
            this.RunCoroutine(this.DoRoundLoop(), "DoRoundLoop");
        }

        private void Server_WaitingForPlayers()
        {
            _offsetClassD = GetRotation(Room.List.First(r => r.Type == RoomType.LczClassDSpawn));
            _offsetCheckpoint = (Rotation)(((int)GetRotation(Room.List.First(r => r.Type == RoomType.HczEzCheckpoint)) + (int)_offsetClassD) % 4);
        }

        private void Scp914_UpgradingItem(Exiled.Events.EventArgs.UpgradingItemEventArgs ev)
        {
            if (!ev.IsAllowed)
                return;

            if (ev.KnobSetting != Scp914KnobSetting.Fine)
                return;

            if (ev.Item.Type != ItemType.Radio)
                return;

            switch (UnityEngine.Random.Range(1, 101))
            {
                case int i when i < 33:
                    MistakenCustomItem.Get(MistakenCustomItems.SNAV_3000).Spawn(ev.OutputPosition, previousOwner: null);
                    break;
                case int i when i < 66:
                    break;
                default:
                    return;
            }

            ev.Item.Destroy();
        }

        private IEnumerator<float> DoRoundLoop()
        {
            yield return Timing.WaitForSeconds(1);
            foreach (var item in PluginHandler.Instance.Config.SNavUltimateSpawns)
            {
                string[] data = item.Split(',');
                if (data.Length < 4 || !float.TryParse(data[1], out float x) || !float.TryParse(data[2], out float y) || !float.TryParse(data[3], out float z))
                {
                    Log.Error($"Config Error | \"{item}\" is not correct SNav Spawn Data");
                    continue;
                }

                var door = Door.List.FirstOrDefault(i => i.Type.ToString() == data[0]);
                if (door == null)
                {
                    Log.Warn("Invalid Data, Unknown Door \"data[0]\"");
                    continue;
                }

                MistakenCustomItems.SNAV_ULTIMATE.TrySpawn(door.Base.transform.position + (door.Base.transform.forward * x) + (door.Base.transform.right * z) + (Vector3.up * y), out _);
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

                var door = Door.List.FirstOrDefault(i => i.Type.ToString() == data[0]);
                if (door == null)
                {
                    Log.Warn($"Invalid Data, Unknown Door \"{data[0]}\"");
                    continue;
                }

                MistakenCustomItems.SNAV_3000.TrySpawn(door.Base.transform.position + (door.Base.transform.forward * x) + (door.Base.transform.right * z) + (Vector3.up * y), out _);
                yield return Timing.WaitForSeconds(0.1f);
            }

            yield return Timing.WaitForSeconds(300);
            int rid = RoundPlus.RoundId;
            while (Round.IsStarted && rid == RoundPlus.RoundId)
            {
                yield return Timing.WaitForSeconds(15);
                LastScan.Clear();
                _surfaceScanGateA = false;
                _surfaceScanGateAHole = false;
                _surfaceScanNuke = false;
                _surfaceScanGateB = false;
                _surfaceScanHelipad = false;
                _surfaceScanEscape = false;
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
                                _surfaceScanEscape = true;
                                continue;
                            }
                            else if (player.Position.z <= -73)
                            {
                                _surfaceScanCassieRoom = true;
                                continue;
                            }
                        }

                        if (player.Position.x >= 148)
                        {
                            _surfaceScanHelipad = true;
                            continue;
                        }

                        if (player.Position.x >= 68)
                        {
                            _surfaceScanGateB = true;
                            continue;
                        }

                        if (player.Position.x <= 42 && player.Position.x >= 37 && player.Position.z <= -32 && player.Position.z >= -38)
                        {
                            _surfaceScanNuke = true;
                            continue;
                        }

                        if (player.Position.z >= -21)
                        {
                            _surfaceScanGateA = true;
                            continue;
                        }

                        _surfaceScanGateAHole = true;
                        continue;
                    }
                }

                var decont = MapPlus.IsLCZDecontaminated(45) && !MapPlus.IsLCZDecontaminated();
                if (this._lastDecont != decont)
                {
                    this._lastDecont = decont;
                    foreach (var player in RealPlayers.List.Where(p => p.IsAlive))
                    {
                        if (!MistakenCustomItem.TryGet(player, out CustomItem item))
                            continue;
                        if (item.Id == (int)MistakenCustomItems.SNAV_3000 || item.Id == (int)MistakenCustomItems.SNAV_ULTIMATE)
                            _requireUpdate.Add(player);
                    }
                }

                var warhead = Warhead.IsInProgress;
                if (this._lastWarhead != warhead)
                {
                    this._lastWarhead = warhead;
                    foreach (var player in RealPlayers.List.Where(p => p.IsAlive))
                    {
                        if (!MistakenCustomItem.TryGet(player, out CustomItem item))
                            continue;
                        if (item.Id == (int)MistakenCustomItems.SNAV_3000 || item.Id == (int)MistakenCustomItems.SNAV_ULTIMATE)
                            _requireUpdate.Add(player);
                    }
                }

                _requireUpdateUltimate = true;
                yield return Timing.WaitForSeconds(1);
                _requireUpdateUltimate = false;
            }
        }
    }
}
