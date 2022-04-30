﻿using System.Collections.Generic;
using Network;
using Oxide.Core.Plugins;
using Oxide.Ext.UiFramework.Builder;
using Oxide.Ext.UiFramework.Pooling;

namespace Oxide.Ext.UiFramework.Plugin
{
    public partial class UiFramework
    {
        #region JSON Sending
        public void DestroyUi(BasePlayer player, string name)
        {
            UiBuilder.DestroyUi(player, name);
        }

        public void DestroyUi(List<Connection> connections, string name)
        {
            UiBuilder.DestroyUi(connections, name);
        }

        private void DestroyUiAll(string name)
        {
            UiBuilder.DestroyUi(name);
        }
        #endregion

        #region Unloading
        public override void HandleRemovedFromManager(PluginManager manager)
        {
            UiFrameworkPool.OnUnload();
            base.HandleRemovedFromManager(manager);
        }
        #endregion
    }
}