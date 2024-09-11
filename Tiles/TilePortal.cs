using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using Terraria.Localization;

namespace BinaryTechnologies.Tiles
{
    public class TEPortal : ModTileEntity
    {
        public bool stateChanged = false;
        private int _portalState = 0;

        public int PortalState
        {
            get => _portalState;
            set => _portalState = value < 2 && value >= 0 ? value : 0;
        }

        public override bool IsTileValidForEntity(int x, int y)
        {
            Tile tile = Main.tile[x, y];
            return tile.HasTile && tile.TileType == ModContent.TileType<Tiles.TilePortal>() && tile.TileFrameX == 0 && tile.TileFrameY == 0;
        }

        public override void SaveData(TagCompound tag)
        {
            tag.Add("PortalState", _portalState);
        }

        public override void LoadData(TagCompound tag)
        {
            try
            {
                _portalState = tag.Get<int>("PortalState");
            }
            catch (System.Exception)
            {
                Main.NewText("PortalState not found");
            }

        }

        public override void Update()
        {
            if (stateChanged)
            {
                Main.NewText("stateChanged");
                NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, ID, Position.X, Position.Y);
                stateChanged = false;
            }

        }

        public override void OnNetPlace()
        {
            if (Main.netMode == NetmodeID.Server)
            {
                NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, ID, Position.X, Position.Y);
            }
        }


        public override void NetSend(BinaryWriter writer)
        {
            writer.Write(_portalState);
        }

        public override void NetReceive(BinaryReader reader)
        {
            _portalState = reader.ReadInt32();
        }

        public void SyncTE()
        {
            //if (Main.netMode == NetmodeID.Server)
            //{
            //    return;
            //}
            Main.NewText("sync");
            NetMessage.SendData(MessageID.TileEntitySharing, -1, -1, null, ID, Position.X, Position.Y);
        }

        public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                NetMessage.SendTileSquare(Main.myPlayer, i, j, 6, 10);
                
                //NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type, 0f, 0, 0, 0);
                NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i, j, Type);
                return -1;
            }
            return Place(i, j);
        }
    }

    public class TilePortal : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileSolidTop[Type] = false;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileTable[Type] = false;
            Main.tileLavaDeath[Type] = false;
            Main.tileLighted[Type] = true;
            Main.tileBlockLight[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style6x3);
            TileObjectData.newTile.Height = 10;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16, 16, 16, 16, 16, 16, 16, 16, 16 };
            TileObjectData.newTile.Origin = new Point16(0, 5);
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<TEPortal>().Hook_AfterPlacement, -1, 0, true);
            TileObjectData.addTile(Type);
            AnimationFrameHeight = 180;
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(200, 200, 200), name);
            DustType = DustID.Stone;
        }

        public int _portalState = 0;

        public override void NearbyEffects(int i, int j, bool closer)
        {
            //Main.NewText(i + " " + j);

            //TEPortal entity = GetPortalEntity(i, j);
            if (TileUtils.TryGetTileEntityAs(i, j, out TEPortal entity))
            {
                _portalState = entity.PortalState;
                Main.LocalPlayer.GetModPlayer<BinaryTechnologiesPlayer>().standingNearPortalState = entity.PortalState != 0;
            }


        }

        private TEPortal GetPortalEntity(int i, int j)
        {
            Tile tile = Main.tile[i, j];
            int left = i - tile.TileFrameX / 18;
            int top = j - tile.TileFrameY % AnimationFrameHeight / 18;
            int index = ModContent.GetInstance<TEPortal>().Find(left, top);
            if (index == -1)
            {
                Main.NewText("Binary Technologies Error: Portal Entity not found", Color.Red);
                //throw new System.Exception("Portal Entity not found");
                return null;
            }
            return (TEPortal)ModTileEntity.ByID[index];
        }

        //public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        //{
        //    Tile tile = Main.tile[i, j];
        //    if (tile.frameX / AnimationFrameHeight > 0)
        //    {

        //        r = 1f;
        //        g = 1f;
        //        b = 1f;
        //    }
        //}

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {

            if (_portalState == 0)
            {
                frame = 0;
                return;
            }

            if (_portalState == 1)
            {
                if (frame < 1 || frame > 3) frame = 1;

                if (++frameCounter >= 18)
                {
                    frameCounter = 0;

                    if (++frame > 3) frame = 1;
                }
            }

        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.LocalPlayer;

            Main.mouseRightRelease = false;

            //TEPortal entity = GetPortalEntity(i, j);
            int byteShard = ModContent.ItemType<Items.ByteShard>();

            if (TileUtils.TryGetTileEntityAs(i, j, out TEPortal entity))
            {
                if (entity.PortalState == 0 && player.ConsumeItem(byteShard))
                {
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.MaxMana, new Vector2(i * 16, j * 16));
                    for (int k = 0; k < 50; k++)
                    {
                        Tile tile = Main.tile[i, j];
                        int left = i - tile.TileFrameX / 18;
                        int top = j - tile.TileFrameY % AnimationFrameHeight / 18;
                        int dustIndex = Dust.NewDust(new Vector2(left * 16, top * 16), 96, 160, 298, 0f, 0f, 255, default(Color), 1f);
                        Main.dust[dustIndex].velocity *= 1.4f;
                    }

                    entity.PortalState = 1;
                    entity.stateChanged = true;
                    entity.SyncTE();
                    //Main.NewText("Portal was activated by Byte Shard!");
                }
            }
            return true;
        }

        //public override void MouseOver(int i, int j)
        //{
        //    Player player = Main.LocalPlayer;
        //    player.cursorItemIconID = ModContent.ItemType<Items.ByteShard>();
        //    player.cursorItemIconText = "";
        //}

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            //TEPortal entity = GetPortalEntity(i, j);
            Point16 origin = TileUtils.GetTileOrigin(i, j);
            ModContent.GetInstance<TEPortal>().Kill(origin.X, origin.Y);
            //ModContent.GetInstance<TEPortal>().Kill(i, j);
            _portalState = 0;
        }
    }
}
