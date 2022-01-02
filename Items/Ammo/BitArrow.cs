using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace MyTestMod.Items.Ammo
{
    public class BitArrow : ModItem
    {
        public override void SetDefaults()
        {
            Item.Size = new Vector2(10, 28);
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 7;
            Item.knockBack = 2.5f;

            Item.maxStack = 999;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.BitArrowProjectile>();
            Item.shootSpeed = 3.5f;

            Item.value = Item.sellPrice(copper: 30);
            Item.rare = ItemRarityID.Blue;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(80);
            recipe.AddIngredient(ItemID.WoodenArrow, 80);
            recipe.AddIngredient(ModContent.ItemType<BitShard>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

    }
}
