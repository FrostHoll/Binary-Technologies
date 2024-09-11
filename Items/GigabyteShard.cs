using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;

namespace BinaryTechnologies.Items
{
    class GigabyteShard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 16;
            Item.rare = ItemRarityID.Yellow;
            Item.value = 6400000;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<Items.MegabyteShard>(), 8);
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.BeetleHusk, 2);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

    }
}
