using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items
{
    class ByteUpgradeModule : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Byte Upgrade Module");
            Tooltip.SetDefault("Imbue a weapon with the power of Byte Shard.");
        }

        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Orange;
            Item.value = 6000;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Items.BaseUpgradeModule>(), 1);
            recipe.AddIngredient(ModContent.ItemType<Items.ByteShard>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

    }
}
