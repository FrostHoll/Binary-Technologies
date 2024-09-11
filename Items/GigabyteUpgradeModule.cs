using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items
{
    class GigabyteUpgradeModule : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Yellow;
            Item.value = 6000;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BaseUpgradeModule>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GigabyteShard>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

    }
}
