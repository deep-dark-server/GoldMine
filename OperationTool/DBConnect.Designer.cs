﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GoldMine.OperationTool {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "14.0.0.0")]
    internal sealed partial class DBConnect : global::System.Configuration.ApplicationSettingsBase {
        
        private static DBConnect defaultInstance = ((DBConnect)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new DBConnect())));
        
        public static DBConnect Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool UseLocalDB {
            get {
                return ((bool)(this["UseLocalDB"]));
            }
            set {
                this["UseLocalDB"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string LocalDBAddress {
            get {
                return ((string)(this["LocalDBAddress"]));
            }
            set {
                this["LocalDBAddress"] = value;
            }
        }
    }
}
