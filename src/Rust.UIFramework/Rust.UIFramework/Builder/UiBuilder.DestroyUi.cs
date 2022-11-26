﻿using System;
using System.Collections.Generic;
using Network;
using Oxide.Ext.UiFramework.UiElements;

namespace Oxide.Ext.UiFramework.Builder
{
    public partial class UiBuilder
    {
        public void DestroyUi(BasePlayer player)
        {
            if (!player) throw new ArgumentNullException(nameof(player));
            DestroyUi(player, _rootName);
        }

        public void DestroyUi(Connection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            DestroyUi(new SendInfo(connection), _rootName);
        }

        public void DestroyUi(List<Connection> connections)
        {
            if (connections == null) throw new ArgumentNullException(nameof(connections));
            DestroyUi(new SendInfo(connections), _rootName);
        }

        public void DestroyUi()
        {
            DestroyUi(_rootName);
        }

        public void DestroyUiImages(BasePlayer player)
        {
            if (!player) throw new ArgumentNullException(nameof(player));
            DestroyUiImages(player.Connection);
        }

        public void DestroyUiImages()
        {
            DestroyUiImages(Net.sv.connections);
        }

        public void DestroyUiImages(Connection connection)
        {
            if (connection == null) throw new ArgumentNullException(nameof(connection));
            for (int index = _components.Count - 1; index >= 0; index--)
            {
                BaseUiComponent component = _components[index];
                if (component is UiRawImage)
                {
                    DestroyUi(new SendInfo(connection), component.Name);
                }
            }
        }

        public void DestroyUiImages(List<Connection> connections)
        {
            if (connections == null) throw new ArgumentNullException(nameof(connections));
            for (int index = _components.Count - 1; index >= 0; index--)
            {
                BaseUiComponent component = _components[index];
                if (component is UiRawImage)
                {
                    DestroyUi(new SendInfo(connections), component.Name);
                }
            }
        }

        public static void DestroyUi(BasePlayer player, string name)
        {
            if (player == null) throw new ArgumentNullException(nameof(player));
            DestroyUi(new SendInfo(player.Connection), name);
        }

        public static void DestroyUi(string name)
        {
            DestroyUi(new SendInfo(Net.sv.connections), name);
        }

        public static void DestroyUi(SendInfo send, string name)
        {
            CommunityEntity.ServerInstance.ClientRPCEx(send, null, UiConstants.RpcFunctions.DestroyUiFunc, name);
        }
    }
}