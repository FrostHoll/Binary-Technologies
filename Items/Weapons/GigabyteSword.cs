using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MyTestMod.Items.Weapons
{
    class GigabyteSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("The presence of this power steals your enemy's life");
        }

        public override void SetDefaults()
        {
            Item.damage = 150;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4.5f;
            Item.scale = 1f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Yellow;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.GigabyteSwordProjectile>();
            Item.shootSpeed = 11f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Items.Weapons.MegabyteSword>(), 1);
            recipe.AddIngredient(ItemType<GigabyteShard>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }
    }
}
