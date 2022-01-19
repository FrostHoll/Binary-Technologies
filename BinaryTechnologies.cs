using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.GameContent.Bestiary;
using Microsoft.Xna.Framework;

namespace BinaryTechnologies
{
    public class BinaryTechnologies : Mod
    {
        public static BinaryTechnologies instance;

        public static readonly string TransPath = "Mods.BinaryTechnologies.";

        public override void Load()
        {
            instance = this;
        }

        public static void GetBestiaryInfo(int npcID)
        {
            BestiaryEntry obj = Main.BestiaryDB.FindEntryByNPCID(npcID);

            foreach (IBestiaryInfoElement item in obj.Info)
            {
                if (item is SpawnConditionBestiaryInfoElement)
                {
                    var _i = item as SpawnConditionBestiaryInfoElement;
                    Main.NewText("SC Icon name: " + _i.GetDisplayNameKey());
                    if (_i.GetBackgroundImage() != null) Main.NewText("SC Image name: " + _i.GetBackgroundImage().Name);
                    Main.NewText("SC Image color: " + _i.GetBackgroundColor().ToString());
                }

                if (item is SpawnConditionBestiaryOverlayInfoElement)
                {
                    var _i = item as SpawnConditionBestiaryOverlayInfoElement;
                    Main.NewText("SC overlay Icon name" + _i.GetDisplayNameKey());
                    if (_i.GetBackgroundOverlayImage() != null) Main.NewText("SC overlay Image name: " + _i.GetBackgroundOverlayImage().Name);
                    Main.NewText("SC overlay Image color: " + _i.GetBackgroundOverlayColor().ToString());
                }

                if (item is SpawnConditionDecorativeOverlayInfoElement)
                {
                    var _i = item as SpawnConditionDecorativeOverlayInfoElement;

                    if (_i.GetBackgroundOverlayImage() != null) Main.NewText("SC decorative overlay Image name: " + _i.GetBackgroundOverlayImage().Name);
                    Main.NewText("SC decorative overlay Image color: " + _i.GetBackgroundOverlayColor().ToString());
                }
            }

        }

        public override void AddRecipes()
        {
            var recipe = CreateRecipe(ItemID.Wire, 5);
            recipe.AddIngredient(ItemID.CopperBar, 2);
            recipe.AddIngredient(ItemID.Silk);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
            recipe = CreateRecipe(ItemID.Wire, 5);
            recipe.AddIngredient(ItemID.TinBar, 2);
            recipe.AddIngredient(ItemID.Silk);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }

    public class BinaryTechnologiesPlayer : ModPlayer
    {
        public bool electpwrglove = false;

        public bool electShock = false;

        //public byte multiToolStatus = 0;

        public override void ResetEffects()
        {
            base.ResetEffects();
            electpwrglove = false;
            electShock = false;
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (electpwrglove && item.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextBool(2))
                {
                    target.AddBuff(ModContent.BuffType<Buffs.ElectShock>(), 120);
                }
            }

            base.OnHitNPC(item, target, damage, knockback, crit);
        }

        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (electpwrglove && proj.DamageType == DamageClass.Melee)
            {
                if (Main.rand.NextBool(2))
                {
                    target.AddBuff(ModContent.BuffType<Buffs.ElectShock>(), 120);
                }
            }
            base.OnHitNPCWithProj(proj, target, damage, knockback, crit);
        }

        public override void UpdateDead()
        {
            electShock = false;
        }

        public override void UpdateBadLifeRegen()
        {
            if (electShock)
            {
                if (Player.lifeRegen > 0)
                {
                    Player.lifeRegen = 0;
                }
                Player.lifeRegenTime = 0;

                Player.lifeRegen -= 30;
            }
        }

        public override void DrawEffects(PlayerDrawSet drawInfo, ref float r, ref float g, ref float b, ref float a, ref bool fullBright)
        {
            if (electShock)
            {
                if (Main.rand.NextBool(4) && drawInfo.shadow == 0f)
                {
                    int dust = Dust.NewDust(drawInfo.Position - new Vector2(2f, 2f), Player.width + 4, Player.height + 4, DustID.Electric, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    //Main.PlayerDrawDust.Add(dust);
                }
                r *= 0.1f;
                g *= 0.2f;
                b *= 0.7f;
                fullBright = true;
            }
        }
    }

    public class MyGlobalNPC : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        public bool electShock = false;

        public override void ResetEffects(NPC npc)
        {
            electShock = false;
            base.ResetEffects(npc);
        }

        public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
            base.UpdateLifeRegen(npc, ref damage);

            if (electShock)
            {
                if (npc.lifeRegen > 0)
                {
                    npc.lifeRegen = 0;
                }
                npc.lifeRegen -= 30;
                if (damage < 2)
                {
                    damage = 2;
                }
            }
        }

        public override void DrawEffects(NPC npc, ref Color drawColor)
        {
            if (electShock)
            {
                if (Main.rand.Next(4) < 3)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, DustID.Electric, npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 100, default(Color), 0.8f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.8f;
                    Main.dust[dust].velocity.Y -= 0.5f;
                    if (Main.rand.NextBool(4))
                    {
                        Main.dust[dust].noGravity = false;
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
                Lighting.AddLight(npc.position, 0.1f, 0.2f, 0.7f);
            }
        }

    }
}