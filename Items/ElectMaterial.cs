using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items
{
    class ElectMaterial : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Electrical spares");
            Tooltip.SetDefault("At first sight it looks like a scrap.");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(silver: 2);
            Item.maxStack = 999;
        }

    }
}
