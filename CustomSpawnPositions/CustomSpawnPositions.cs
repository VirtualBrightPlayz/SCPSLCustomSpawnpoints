using Smod2;
using Smod2.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualBrightPlayz.SCPSL.CustomSpawnPositions
{
    [PluginDetails(author = "VirtualBrightPlayz",
        description = "Custom spawnpoints for almost anything. Saved in a config/database file. (Suggested by numerous people in SMod2 Discord)",
        id = "virtualbrightplayz.scpsl.customspawnpositions",
        name = "Custom Spawn Positions",
        version = "1.0",
        SmodMajor = 3,
        SmodMinor = 0,
        SmodRevision = 0)]
    public class CustomSpawnPositions : Plugin
    {

        public static Dictionary<string, string> roomNamesLCZ = new Dictionary<string, string>() {
            //LCZ
            { "173", "root_173" },
            { "classdspawn", "root_classdspawn" },
            { "cafe", "root_cafe" },
            { "chkpa", "root_@chkpa" },
            { "chkpb", "root_#chkpb" },
            { "914", "root_914" },
            { "012", "root_012" },
            { "toilets", "root_toilets" },
            { "372", "root_372" },
            { "armory", "root_armory" },
        };

        public static Dictionary<string, string> roomNamesHCZ = new Dictionary<string, string>() {
            //HCZ
            { "lifta", "root_@chkpa" },
            { "liftb", "root_#chkpb" },
            { "457", "root_457" },
            { "096", "root_457" },
            { "nuke", "root_!nuke" },
            { "microhid", "root_hid" },
            { "079", "root_079" },
            { "049", "root_$049" },
            { "939", "root_testroom" },
            { "106", "root_106" },
            { "servers", "root_servers" },
            { "ez", "root_checkpoint" },
        };

        public override void OnDisable()
        {
        }

        public override void OnEnable()
        {
            this.Info("CustomSpawnPositions plugin enabled.");
        }

        public override void Register()
        {
            this.AddCommand("addsp", new CSPCommandAddSpawnPoint(this));
            this.AddCommand("addis", new CSPCommandAddItemSpawn(this));
            this.AddEventHandlers(new CSPEventHandler(this));
        }
    }
}
