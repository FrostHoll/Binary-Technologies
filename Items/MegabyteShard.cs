using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace MyTestMod.Items
{
    class MegabyteShard : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Megabyte Shard");
            Tooltip.SetDefault("You can barely hold it in your hands...");
            //ItemID.Sets.ItemIconPulse[Item.type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Lime;
            Item.value = 800000;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Items.KilobyteShard>(), 8);
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

    }
}
