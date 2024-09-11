using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace BinaryTechnologies
{
    public class BinaryTechnologies : Mod
    {
        public static BinaryTechnologies instance;

        //public static ModKeybind temp;

        public static readonly string TransPath = "Mods.BinaryTechnologies.";

        public override void Load()
        {
            instance = this;
            //temp = KeybindLoader.RegisterKeybind(this, "Test Button", "P");
        }
    }
}