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
