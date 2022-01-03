﻿using System;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyTestMod.NPCs.Town
{
    [AutoloadHead]
    public class Ancestor : ModNPC
    {
        public override string Texture => "MyTestMod/NPCs/Town/Ancestor";

        public override string HeadTexture => "MyTestMod/NPCs/Town/AncestorHead";

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true;
            NPC.width = 24;
            NPC.height = 46;
            NPC.aiStyle = 7;
            NPC.damage = 20;
            NPC.defense = 15;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.DyeTrader;
            NPCID.Sets.HatOffsetY[NPC.type] = 10;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ancestor");
            Main.npcFrameCount[NPC.type] = 3;
            NPCID.Sets.ExtraFramesCount[NPC.type] = 1;
            NPCID.Sets.AttackFrameCount[NPC.type] = 1;
            NPCID.Sets.DangerDetectRange[NPC.type] = 550;
            NPCID.Sets.AttackType[NPC.type] = 3;
            NPCID.Sets.AttackTime[NPC.type] = 30;
            NPCID.Sets.AttackAverageChance[NPC.type] = 30;
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.Add(new SpawnConditionBestiaryInfoElement(Language.GetTextValue(MyTestMod.TransPath + "Biomes.Surface"), 0, "Images/MapBG1"));
            bestiaryEntry.Info.Add(new FlavorTextBestiaryInfoElement(Language.GetTextValue(MyTestMod.TransPath + "AncestorDesc")));
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(0, 2))
            {
                case 0:
                    return "Oleg";
                case 1:
                    return "Arthur";
                default:
                    return "Robert";
            }
        }

        public override string GetChat()
        {
            
            string Ancestor1 = Language.GetTextValue(MyTestMod.TransPath + "Ancestor1");
            string Ancestor2 = Language.GetTextValue(MyTestMod.TransPath + "Ancestor2");
            string Ancestor3 = Language.GetTextValue(MyTestMod.TransPath + "Ancestor3");
            var guide = NPC.GetFirstNPCNameOrNull(NPCID.Guide);
            //string Ancestor3 = "This guy, " + guide + ", looks familiar... I guess I saw him someday, but I can't remember where and when exactly.";
            if (guide != null && Main.rand.Next(4) == 0)
            {
                return Ancestor3;
            }
            switch (Main.rand.Next(2))
            {
                case 0:
                    return Ancestor1;
                default:
                    return Ancestor2;
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
        }

        public override void OnChatButtonClicked(bool firstButton, ref bool shop)
        {
            if (firstButton)
            {
                shop = true;
            }
        }

        private bool CheckByteShard()
        {
            foreach (Player player in Main.player)
            {
                if (player.HasItem(ModContent.ItemType<Items.ByteShard>()))
                {
                    return true;
                }
            }

            return false;
        }

        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return (Main.hardMode && CheckByteShard());
        }

        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = 20;
            knockback = 2f;
        }

        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 32;
            randExtraCooldown = 33;
        }

        public override void TownNPCAttackSwing(ref int itemWidth, ref int itemHeight)
        {
            itemWidth = 32;
            itemHeight = 32;
        }

        public override void DrawTownAttackSwing(ref Texture2D item, ref int itemSize, ref float scale, ref Vector2 offset)
        {
            item = ModContent.Request<Texture2D>("MyTestMod/Items/Weapons/BitSword").Value;
            itemSize = 32;
        }

        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.BitShard>());
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ModContent.ItemType<Items.ByteShard>());
            nextSlot++;
        }
    }
}