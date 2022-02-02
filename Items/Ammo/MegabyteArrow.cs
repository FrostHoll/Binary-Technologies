using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;


namespace BinaryTechnologies.Items.Ammo
{
    public class MegabyteArrow : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("When hits an enemy, releases a copy of itself");
        }

        public override void SetDefaults()
        {
            Item.Size = new Vector2(10, 10);
            Item.DamageType = DamageClass.Ranged;
            Item.damage = 14;
            Item.knockBack = 2.5f;
            Item.scale = 0.6f;

            Item.maxStack = 999;
            Item.consumable = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.MegabyteArrowProjectile>();
            Item.shootSpeed = 3.5f;

            Item.value = Item.sellPrice(copper: 60);
            Item.rare = ItemRarityID.Lime;
            Item.ammo = AmmoID.Arrow;
        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(80);
            recipe.AddIngredient(ItemID.WoodenArrow, 80);
            recipe.AddIngredient(ModContent.ItemType<MegabyteShard>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }

    }
}
