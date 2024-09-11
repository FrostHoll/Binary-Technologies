using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items
{
    class ElectMaterial : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Blue;
            Item.value = 1000;
            Item.maxStack = 999;
        }

    }
}
