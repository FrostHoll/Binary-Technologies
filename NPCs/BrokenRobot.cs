using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.ItemDropRules;
using Terraria.Localization;
using Microsoft.Xna.Framework;

namespace BinaryTechnologies.NPCs
{
    public class BrokenRobot : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Broken Robot");
            Main.npcFrameCount[NPC.type] = 3;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers(0)
            {
                Velocity = 0.5f,
                Direction = -1
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);

        }

        public override void SetDefaults()
        {
            NPC.width = 34;
            NPC.height = 45;
            NPC.damage = 30;
            NPC.defense = 12;
            NPC.lifeMax = 220;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.value = Item.buyPrice(silver: 1);
            NPC.knockBackResist = 0.5f;
            NPC.aiStyle = 3;
            AIType = NPCID.Zombie;
            AnimationType = NPCID.Zombie;
            //Banner = Item.NPCtoBanner(NPCID.Zombie);
            //BannerItem = Item.BannerToItem(Banner);
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Times.NightTime,
                new FlavorTextBestiaryInfoElement(Language.GetTextValue(BinaryTechnologies.TransPath + "Bestiary.BrokenRobot"))
            });
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            return SpawnCondition.OverworldNightMonster.Chance * 0.2f;
        }

        public override void HitEffect(int hitDirection, double damage)
        {
            for (int i = 0; i < 10; i++)
            {
                int dustType = Main.rand.Next(139, 143);
                int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType);
                Dust dust = Main.dust[dustIndex];
                dust.velocity.X = dust.velocity.X + Main.rand.Next(-50, 51) * 0.01f;
                dust.velocity.Y = dust.velocity.Y + Main.rand.Next(-50, 51) * 0.01f;
                dust.scale *= 1f + Main.rand.Next(-30, 31) * 0.01f;
            }
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            if (!target.HasBuff(BuffID.Electrified))
            {
                if (Main.expertMode || Main.masterMode)
                {
                    target.AddBuff(BuffID.Electrified, 60);
                }
                else
                {
                    if (Main.rand.NextBool(3)) target.AddBuff(ModContent.BuffType<Buffs.ElectShock>(), 60);
                }
            }
            base.OnHitPlayer(target, damage, crit);
        }

        private const int AI_LASER_TIMER = 0;

        public override void AI()
        {
            if (Main.rand.NextBool(5)) Dust.NewDust(NPC.Center, NPC.Hitbox.Width, NPC.Hitbox.Height, 169, 0f, 0f, 100);

            if (!NPC.HasPlayerTarget)
            {
                base.AI();
                return;
            }

            Player player = Main.player[NPC.target];

            if (NPC.localAI[AI_LASER_TIMER] <= 0f)
            {
                Vector2 projDirection = Vector2.Normalize(player.position - NPC.Top) * 8f;
                int proj = Projectile.NewProjectile(NPC.GetProjectileSpawnSource(), new Vector2(NPC.Top.X, NPC.Top.Y + 15f), projDirection, ProjectileID.DeathLaser, NPC.damage / 2, 0f, Main.myPlayer);
                NPC.localAI[AI_LASER_TIMER] = 120f;
                NPC.netUpdate = true;
            }
            else
            {
                NPC.localAI[AI_LASER_TIMER]--;
            }

            base.AI();
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.ElectMaterial>(), 3, 1, 1));
        }
    }
}