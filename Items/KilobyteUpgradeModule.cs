using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items
{
    class KilobyteUpgradeModule : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Kilobyte Upgrade Module");
            Tooltip.SetDefault("Imbue a weapon with the power of Kilobyte Shard.");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Pink;
            Item.value = 6000;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Items.BaseUpgradeModule>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Items.KilobyteShard>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

    }
}
