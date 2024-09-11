using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace BinaryTechnologies.Items
{
    class TerrabyteShard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Red;
            Item.value = 51200000;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<GigabyteShard>(), 8);
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.LunarBar, 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

    }
}
