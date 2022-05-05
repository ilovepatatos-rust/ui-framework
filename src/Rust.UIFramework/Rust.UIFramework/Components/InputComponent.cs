﻿using Oxide.Ext.UiFramework.Json;
using UnityEngine.UI;

namespace Oxide.Ext.UiFramework.Components
{
    public class InputComponent : BaseTextComponent
    {
        private const string Type = "UnityEngine.UI.InputField";

        public int CharsLimit;
        public string Command;
        public bool IsPassword;
        public bool IsReadyOnly;
        public bool NeedsKeyboard = true;
        public InputField.LineType LineType;

        public override void WriteComponent(JsonFrameworkWriter writer)
        {
            writer.WriteStartObject();
            JsonCreator.AddFieldRaw(writer, JsonDefaults.Common.ComponentTypeName, Type);
            JsonCreator.AddField(writer, JsonDefaults.Input.CharacterLimitName, CharsLimit, JsonDefaults.Input.CharacterLimitValue);
            JsonCreator.AddField(writer, JsonDefaults.Common.CommandName, Command, JsonDefaults.Common.NullValue);
            JsonCreator.AddField(writer, JsonDefaults.Input.LineTypeName, LineType);

            if (IsPassword)
            {
                JsonCreator.AddKeyField(writer, JsonDefaults.Input.PasswordName);
            }

            if (IsReadyOnly)
            {
                JsonCreator.AddFieldRaw(writer, JsonDefaults.Input.ReadOnlyName, true);
            }

            if (NeedsKeyboard)
            {
                JsonCreator.AddKeyField(writer, JsonDefaults.Input.InputNeedsKeyboardName);
            }
            
            base.WriteComponent(writer);
            writer.WriteEndObject();
        }

        protected override void EnterPool()
        {
            base.EnterPool();
            CharsLimit = 0;
            Command = null;
            NeedsKeyboard = true;
            IsPassword = false;
            LineType = default(InputField.LineType);
        }
    }
}