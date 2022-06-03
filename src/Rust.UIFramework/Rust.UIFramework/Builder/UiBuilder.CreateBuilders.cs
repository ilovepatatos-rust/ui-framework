﻿using Oxide.Ext.UiFramework.Cache;
using Oxide.Ext.UiFramework.Colors;
using Oxide.Ext.UiFramework.Enums;
using Oxide.Ext.UiFramework.Offsets;
using Oxide.Ext.UiFramework.Pooling;
using Oxide.Ext.UiFramework.Positions;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder
    {
        public static UiBuilder Create(UiPosition pos, string name, string parent) => Create(pos, default(UiOffset), name, parent);
        public static UiBuilder Create(UiPosition pos, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default(UiOffset), name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(UiPosition pos, UiOffset offset, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(UiPosition pos, UiOffset offset, string name, string parent) => Create(UiSection.Create(pos, offset), name, parent);
        public static UiBuilder Create(UiPosition pos, UiColor color, string name, string parent) => Create(pos, default(UiOffset), color, name, parent);
        public static UiBuilder Create(UiPosition pos, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, default(UiOffset), color, name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(UiPosition pos, UiOffset offset, UiColor color, string name, UiLayer parent = UiLayer.Overlay) => Create(pos, offset, color, name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(UiPosition pos, UiOffset offset, UiColor color, string name, string parent) => Create(UiPanel.Create(pos, offset, color), name, parent);
        public static UiBuilder Create(BaseUiComponent root, string name, UiLayer parent = UiLayer.Overlay) => Create(root, name, UiLayerCache.GetLayer(parent));
        public static UiBuilder Create(BaseUiComponent root, string name, string parent)
        {
            UiBuilder builder = Create();
            builder.SetRoot(root, name, parent);
            return builder;
        }

        public static UiBuilder Create()
        {
            return UiFrameworkPool.Get<UiBuilder>();
        }
        
        /// <summary>
        /// Creates a UiBuilder that is designed to be a popup modal
        /// </summary>
        /// <param name="offset">Dimensions of the modal</param>
        /// <param name="modalColor">Modal form color</param>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <returns></returns>
        public static UiBuilder CreateModal(string name, UiColor modalColor, UiOffset offset, UiLayer layer = UiLayer.Overlay)
        {
            return CreateModal(name, modalColor, offset, new UiColor(0, 0, 0, 0.5f), UiConstants.Materials.InGameBlur, layer);
        }

        /// <summary>
        /// Creates a UiBuilder that is designed to be a popup modal
        /// </summary>
        /// <param name="offset">Dimensions of the modal</param>
        /// <param name="modalColor">Modal form color</param>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <param name="modalBackgroundColor">Color of the fullscreen background</param>
        /// <param name="backgroundMaterial">Material of the full screen background</param>
        /// <returns></returns>
        public static UiBuilder CreateModal(string name, UiColor modalColor, UiOffset offset, UiColor modalBackgroundColor, string backgroundMaterial = null, UiLayer layer = UiLayer.Overlay)
        {
            UiPanel backgroundBlur = UiPanel.Create(UiPosition.Full, default(UiOffset), modalBackgroundColor);
            backgroundBlur.SetMaterial(backgroundMaterial);
            
            UiBuilder builder = Create(backgroundBlur, name, layer);
            
            UiPanel modal = UiPanel.Create(UiPosition.MiddleMiddle, offset, modalColor);
            builder.AddComponent(modal, backgroundBlur);
            builder.OverrideRoot(modal);
            return builder;
        }
        
        /// <summary>
        /// Creates a UI builder that when created before your main UI will run a command if the user click outside of the UI window
        /// </summary>
        /// <param name="command">Command to run when the button is clicked</param>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <returns></returns>
        public static UiBuilder CreateOutsideClose(string command, string name, UiLayer layer = UiLayer.Overlay)
        {
            UiBuilder builder = Create(UiButton.CreateCommand(UiPosition.Full, default(UiOffset), UiColor.Clear, command), name, layer);
            builder.NeedsMouse();
            return builder;
        }
        
        /// <summary>
        /// Creates a UI builder that will hold mouse input so the mouse doesn't reset position when updating other UI's
        /// </summary>
        /// <param name="name">Name of the UI</param>
        /// <param name="layer">Layer the UI is on</param>
        /// <returns></returns>
        public static UiBuilder CreateMouseLock(string name, UiLayer layer = UiLayer.Overlay)
        {
            UiBuilder builder = Create(UiPosition.None, UiColor.Clear, name, UiLayerCache.GetLayer(layer));
            builder.NeedsMouse();
            return builder;
        }
    }
}