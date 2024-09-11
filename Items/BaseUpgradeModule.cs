using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace BinaryTechnologies.Items
{
    class BaseUpgradeModule : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 12;
            Item.height = 12;
            Item.rare = ItemRarityID.Blue;
            Item.value = 6000;
            Item.maxStack = 999;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ElectMaterial>(), 2);
            recipe.AddIngredient(ItemID.Wire, 10);            
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

    }
}
