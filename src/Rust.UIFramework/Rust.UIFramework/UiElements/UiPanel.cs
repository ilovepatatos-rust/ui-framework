﻿using Newtonsoft.Json;
using UI.Framework.Rust.Colors;
using UI.Framework.Rust.Components;
using UI.Framework.Rust.Json;
using UI.Framework.Rust.Positions;
using Pool = Facepunch.Pool;

namespace UI.Framework.Rust.UiElements
{
    public class UiPanel : BaseUiComponent
    {
        public ImageComponent Image;

        public void AddSprite(string sprite)
        {
            Image.Sprite = sprite;
        }

        public void AddMaterial(string material)
        {
            Image.Material = material;
        }

        public static UiPanel Create(UiPosition pos, UiOffset offset, UiColor color)
        {
            UiPanel panel = CreateBase<UiPanel>(pos, offset);
            panel.Image.Color = color;
            return panel;
        }

        public static UiPanel Create(Position pos, Offset? offset, UiColor color)
        {
            UiPanel panel = CreateBase<UiPanel>(pos, offset);
            panel.Image.Color = color;
            return panel;
        }

        public override void WriteComponents(JsonTextWriter writer)
        {
            JsonCreator.Add(writer, Image);
            base.WriteComponents(writer);
        }

        public override void EnterPool()
        {
            base.EnterPool();
            Pool.Free(ref Image);
        }

        public override void LeavePool()
        {
            base.LeavePool();
            Image = Pool.Get<ImageComponent>();
        }

        public override void SetFadeIn(float duration)
        {
            Image.FadeIn = duration;
        }
    }
}