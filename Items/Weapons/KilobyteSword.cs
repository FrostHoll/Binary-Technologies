using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace MyTestMod.Items.Weapons
{
    class KilobyteSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("You are feeling determination in your hand...");
        }

        public override void SetDefaults()
        {
            Item.damage = 70;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4.5f;
            Item.scale = 1.2f;
            Item.value = 10000;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<Projectiles.KilobyteSwordProjectile>();
            Item.shootSpeed = 10f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemType<Items.Weapons.ByteSword>(), 1);
            recipe.AddIngredient(ItemType<KilobyteShard>(), 1);
            recipe.AddTile(ModContent.TileType<Tiles.TilePC>());
            recipe.Register();
        }
    }
}
