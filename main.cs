using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TerrariaApi.Server;
using TShockAPI;
using Terraria;
using System.Reflection;

namespace magnusi
{
    [ApiVersion(1, 25)]
    public class FuckExplosives : TerrariaPlugin
    {
        private static System.Timers.Timer aTimer;
        public static System.Timers.Timer settimeout;
        public TSPlayer GlobalisedPlayer;
        private List<TSPlayer> joined;

        public FuckExplosives(Main game) : base(game) {}
        public override Version Version     { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
        public override string  Author      { get { return "magnusi"; } }
        public override string  Name        { get { return "FuckExplosives"; } }
        public override string  Description { get { return "Self-explanatory"; } }

        public override void Initialize()
        {
            aTimer = new System.Timers.Timer(10000);
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            aTimer.Interval = 4000;
            aTimer.Enabled = true;
        }

        private void YouCantPlay(object sender, ElapsedEventArgs e)
        {
            GlobalisedPlayer.SendInfoMessage("you can't play on this server with Explosives, keep that in mind");
            settimeout.Stop();
        }

        private void OnBTimedEvent(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer plr in TShock.Players)
                if (!joined.Contains(plr))
                    joined.Add(plr);
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            foreach (TSPlayer plr in TShock.Players)
                foreach (Terraria.Item itm in plr.TPlayer.inventory)
                    if (itm.name == "Explosives"
                     || itm.name == "Dynamite"
                     || itm.name == "Bomb"
                     || itm.name == "Sticky Bomb"
                     || itm.name == "Bouncy Bomb"
                     || itm.name == "Bouncy Dynamite"
                     || itm.name == "Sticky Dynamite" && !plr.Group.HasPermission("tshock.admin.kick"))
                    {
                        TSPlayer.All.SendInfoMessage(plr.Name + " was kicked for having " + itm.name + " in their inventory.");
                        plr.Disconnect("You can't play with " + itm.name + " in your inventory.");
                    }
        }
    }
}
