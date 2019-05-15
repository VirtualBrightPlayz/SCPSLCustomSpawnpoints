using Smod2.API;
using Smod2.EventHandlers;
using Smod2.Events;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace VirtualBrightPlayz.SCPSL.CustomSpawnPositions
{
    internal class CSPEventHandler : IEventHandlerRoundStart
    {
        private CustomSpawnPositions customSpawnPositions;

        public CSPEventHandler(CustomSpawnPositions customSpawnPositions)
        {
            this.customSpawnPositions = customSpawnPositions;
        }

        void IEventHandlerRoundStart.OnRoundStart(RoundStartEvent ev)
        {
            var arr2 = new List<string>();
            if (File.Exists(FileManager.GetAppFolder() + "spawnpoint_db.txt"))
                arr2 = new List<string>(File.ReadAllLines(FileManager.GetAppFolder() + "spawnpoint_db.txt"));
            var itemarr = new List<string>();
            if (File.Exists(FileManager.GetAppFolder() + "itemspawns.txt"))
                itemarr = new List<string>(File.ReadAllLines(FileManager.GetAppFolder() + "itemspawns.txt"));
            var itemspawns = new Dictionary<string, string>();
            foreach (var item in itemarr)
            {
                for (int j = 0; j < arr2.Count; j++)
                {
                    if (item.Split(' ')[1].Equals(arr2[j].Split(' ')[1]))
                        itemspawns.Add(arr2[j], item.Split(' ')[0]);
                }
            }
            var lczrooms = GameObject.Find("LightRooms").transform;
            var hczrooms = GameObject.Find("HeavyRooms").transform;
            var ezrooms = GameObject.Find("EntranceRooms").transform;
            for (int i = 0; i < lczrooms.childCount; i++)
            {
                var room = lczrooms.GetChild(i);
                foreach (var item in itemspawns)
                {
                    var spawnitem = item.Key;
                    if (room.name.ToLower().Contains(spawnitem.Split(' ')[0]))
                    {
                        Vector3 pos1 = new Vector3(float.Parse(spawnitem.Split(' ')[2]), float.Parse(spawnitem.Split(' ')[3]), float.Parse(spawnitem.Split(' ')[4]));
                        Pickup.inv.SetPickup(int.Parse(item.Value), -4.65664672E+11f, room.TransformPoint(pos1), Quaternion.identity, 0, 0, 0);
                    }
                }                
            }
        }
    }
}