using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items
{
    class EnergyCore : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energy Core");
            Tooltip.SetDefault("It contains powerful energy from another world.");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Orange;
            Item.value = 6000;
            Item.maxStack = 999;
        }

    }
}
