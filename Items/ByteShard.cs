using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items
{
    class ByteShard : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Orange;
            Item.value = 14000;
            Item.maxStack = 999;

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BitShard>(), 8);
            recipe.AddIngredient(ItemID.Wire, 10);
            recipe.AddIngredient(ItemID.HellstoneBar, 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }
    }
}
