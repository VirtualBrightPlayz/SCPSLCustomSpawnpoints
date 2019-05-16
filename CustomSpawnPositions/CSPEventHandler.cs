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

            var keys = new List<string>();
            var values = new List<KeyValuePair<string, Vector3>>();

            var groups = new List<string>();

            //calc. all spawnpoints and spawnpoint groups
            foreach (var item in arr2)
            {
                Vector3 pos1 = new Vector3(float.Parse(item.Split(' ')[2]), float.Parse(item.Split(' ')[3]), float.Parse(item.Split(' ')[4]));
                //keys = name of spawnpoint
                keys.Add(item.Split(' ')[1].Split(':')[0]);
                //values.string = name of room
                //values.vector3 = position to spawn in room
                values.Add(new KeyValuePair<string, Vector3>(item.Split(' ')[0], pos1));
            }

            List<KeyValuePair<int, int>> itemstospawn = new List<KeyValuePair<int, int>>();
            List<int> itemstospawnids = new List<int>();

            foreach (var item in itemarr)
            {
                var itemid = int.Parse(item.Split(' ')[0]);
                var spawnpointid = item.Split(' ')[1];
                if (keys.Contains(spawnpointid))
                {
                    List<int> indexes = new List<int>();
                    for (int i = 0; i < keys.Count; i++)
                    {
                        if (keys[i].Equals(spawnpointid))
                        {
                            indexes.Add(i);
                        }
                    }
                    itemstospawn.Add(new KeyValuePair<int, int>(indexes[Random.Range(0, indexes.Count)], itemid));
                    //itemstospawnids.Add(itemid);
                }
            }

            var lczrooms = GameObject.Find("LightRooms").transform;
            var hczrooms = GameObject.Find("HeavyRooms").transform;
            var ezrooms = GameObject.Find("EntranceRooms").transform;

            for (int i = 0; i < lczrooms.childCount; i++)
            {
                var room = lczrooms.GetChild(i);
                foreach (var item in itemstospawn)
                {
                    var index = item.Key;
                    if (!CustomSpawnPositions.roomNamesLCZ.ContainsKey(values[index].Key))
                        continue;
                    if (room.name.ToLower().Contains(CustomSpawnPositions.roomNamesLCZ[values[index].Key]))
                    {
                        Pickup.inv.SetPickup(item.Value, -4.65664672E+11f, room.TransformPoint(values[index].Value), Quaternion.identity, 0, 0, 0);
                    }
                }
            }
            for (int i = 0; i < hczrooms.childCount; i++)
            {
                var room = hczrooms.GetChild(i);
                foreach (var item in itemstospawn)
                {
                    var index = item.Key;
                    if (!CustomSpawnPositions.roomNamesHCZ.ContainsKey(values[index].Key))
                        continue;
                    if (room.name.ToLower().Contains(CustomSpawnPositions.roomNamesHCZ[values[index].Key]))
                    {
                        Pickup.inv.SetPickup(item.Value, -4.65664672E+11f, room.TransformPoint(values[index].Value), Quaternion.identity, 0, 0, 0);
                    }
                }
            }

            //old code
            /*return;
            var itemspawns = new Dictionary<string, string>();
            var itemspawns2 = new List<string>();
            var itemspawnsid = new List<int>();
            foreach (var item in itemarr)
            {
                //for (int j = 0; j < arr2.Count; j++)
                //{
                //if (item.Split(' ')[1].Equals(arr2[j].Split(' ')[1]))
                //spawnpoint name | item id
                //itemspawns.Add(item.Split(' ')[1], item.Split(' ')[0]);
                //add spawnpoint name
                itemspawns2.Add(item.Split(' ')[1]);
                //add item id
                itemspawnsid.Add(int.Parse(item.Split(' ')[0]));
                //}
            }
            var lczrooms2 = GameObject.Find("LightRooms").transform;
            var hczrooms2 = GameObject.Find("HeavyRooms").transform;
            var ezrooms2 = GameObject.Find("EntranceRooms").transform;
            var itemstospawn2 = new Dictionary<string, Vector3>();
            var itemgroups = new List<Dictionary<int, string>>();
            var itemgroupkeys = new List<string>();
            var itemgroupvals = new List<string>();
            foreach (var item in arr2)
            {
                //0 = group name
                //1 = some id, just to make them different
                var item2 = item.Split(' ')[1].Split(':');
                if (item2.Length == 2 && itemspawns2.Contains(item2[0]) && !itemgroupkeys.Contains(item2[0]))
                {
                    //add the name of group to a list
                    itemgroupkeys.Add(item2[0]);
                    //add the whole listing to the value
                    itemgroupvals.Add(item);
                    //group | name of spawn group
                    //itemgroups.Add(int.Parse(item2[1]), item2[0]);
                    //itemgroupkeys.Add(int.Parse(item2[1]));
                }
                else
                {
                    Vector3 pos1 = new Vector3(float.Parse(item.Split(' ')[2]), float.Parse(item.Split(' ')[3]), float.Parse(item.Split(' ')[4]));
                    //add the name of room and item id and position to a dict.
                    itemstospawn2.Add(item.Split(' ')[0] + " " + itemspawnsid[itemspawns2.IndexOf(item.Split(' ')[1])], pos1);
                }
            }


            //itemgroups[itemgroupkeys[Random.Range(0, itemgroupkeys.Count)]];
            return;
            for (int i = 0; i < lczrooms.childCount; i++)
            {
                var room = lczrooms.GetChild(i);
                foreach (var item in itemspawns)
                {
                    var spawnitem = item.Key;
                    var args = spawnitem.Split(' ');
                    if (room.name.ToLower().Contains(CustomSpawnPositions.roomNamesLCZ[args[0]]))
                    {
                        Vector3 pos1 = new Vector3(float.Parse(spawnitem.Split(' ')[2]), float.Parse(spawnitem.Split(' ')[3]), float.Parse(spawnitem.Split(' ')[4]));
                        Pickup.inv.SetPickup(int.Parse(item.Value), -4.65664672E+11f, room.TransformPoint(pos1), Quaternion.identity, 0, 0, 0);
                    }
                }
            }
            for (int i = 0; i < hczrooms.childCount; i++)
            {
                var room = hczrooms.GetChild(i);
                foreach (var item in itemspawns)
                {
                    var spawnitem = item.Key;
                    var args = spawnitem.Split(' ');
                    if (room.name.ToLower().Contains(CustomSpawnPositions.roomNamesHCZ[args[0]]))
                    {
                        Vector3 pos1 = new Vector3(float.Parse(spawnitem.Split(' ')[2]), float.Parse(spawnitem.Split(' ')[3]), float.Parse(spawnitem.Split(' ')[4]));
                        Pickup.inv.SetPickup(int.Parse(item.Value), -4.65664672E+11f, room.TransformPoint(pos1), Quaternion.identity, 0, 0, 0);
                    }
                }
            }*/
        }
    }
}