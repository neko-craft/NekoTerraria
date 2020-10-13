using System;
using TShockAPI;
using Terraria;
using TerrariaApi.Server;

namespace TestPlugin {
    [ApiVersion(2, 1)]
    public class TestPlugin: TerrariaPlugin {
        private int[] BUFF_LIST = { 1, 2, 3, 4, 5, 15, 9, 14, 29, 48, 87, 192, 104, 109, 113, 115, 117, 151, 158, 159 };
        /// <summary>
        /// Gets the author(s) of this plugin
        /// </summary>
        public override string Author => "Shirasawa";

        /// <summary>
        /// Gets the description of this plugin.
        /// A short, one lined description that tells people what your plugin does.
        /// </summary>
        public override string Description => "A simple plugin for TShock.";

        /// <summary>
        /// Gets the name of this plugin.
        /// </summary>
        public override string Name => "NekoTerraria";

        /// <summary>
        /// Gets the version of this plugin.
        /// </summary>
        public override Version Version => new Version(1, 0, 0, 0);

        /// <summary>
        /// Initializes a new instance of the TestPlugin class.
        /// This is where you set the plugin's order and perfrom other constructor logic
        /// </summary>
        public TestPlugin(Main game) : base(game) {
        }

        /// <summary>
        /// Handles plugin initialization. 
        /// Fired when the server is started and the plugin is being loaded.
        /// You may register hooks, perform loading procedures etc here.
        /// </summary>
        public override void Initialize() {
            Group.DefaultGroup.AddPermission(Permissions.ignorestackhackdetection);
            Commands.ChatCommands.Add(new Command(it => it.Player.GiveItem(2997, 50), "gt", "tppotion") {
                AllowServer = false,
                HelpText = "Give me a teleport potion!"
            });
            Commands.ChatCommands.Add(new Command(it => it.Player.GiveItem(4870, 50), "gr", "returnpotion") {
                AllowServer = false,
                HelpText = "Give me a return potion!"
            });
            Commands.ChatCommands.Add(new Command(it => addBuff(it.Player), "ab", "allbuff") {
                AllowServer = false,
                HelpText = "Add buffs!"
            });
            Commands.ChatCommands.Add(new Command(it => it.Player.SetBuff(13, 99999999, true), "uw") {
                AllowServer = false,
                HelpText = "Unlimited war!"
            });
            GetDataHandlers.PlayerSpawn.Register((sender, it) => {
                addBuff(it.Player);
                it.Player.Heal();
                it.Player.Group = new SuperAdminGroup();
                it.Player.SetTeam(2);
                Main.npc.ForEach(npc => {
                    if (npc == null || !npc.townNPC) return;
                    npc.lifeMax = 999999999;
                    npc.lifeRegen = 999999999;
                });
            });
        }

        /// <summary>
        /// Handles plugin disposal logic.
        /// *Supposed* to fire when the server shuts down.
        /// You should deregister hooks and free all resources here.
        /// </summary>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                // Deregister hooks here
            }
            base.Dispose(disposing);
        }

        void addBuff(TSPlayer player) {
            BUFF_LIST.ForEach(it => player.SetBuff(it, 99999999, true));
        }
    }
}
