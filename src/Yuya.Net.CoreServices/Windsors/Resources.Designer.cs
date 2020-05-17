﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Yuya.Net.CoreServices.Windsors {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("TurkuazGO.Windsors.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create instance of type &apos;{0}&apos; because it is either abstract or an interface..
        /// </summary>
        internal static string Error_CannotActivateAbstractOrInterface {
            get {
                return ResourceManager.GetString("Error_CannotActivateAbstractOrInterface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to convert &apos;{0}&apos; to type &apos;{1}&apos;..
        /// </summary>
        internal static string Error_FailedBinding {
            get {
                return ResourceManager.GetString("Error_FailedBinding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to create instance of type &apos;{0}&apos;..
        /// </summary>
        internal static string Error_FailedToActivate {
            get {
                return ResourceManager.GetString("Error_FailedToActivate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot create instance of type &apos;{0}&apos; because it is missing a public parameterless constructor..
        /// </summary>
        internal static string Error_MissingParameterlessConstructor {
            get {
                return ResourceManager.GetString("Error_MissingParameterlessConstructor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No IConfigureOptions&lt;&gt; or IPostConfigureOptions&lt;&gt; implementations were found..
        /// </summary>
        internal static string Error_NoIConfigureOptions {
            get {
                return ResourceManager.GetString("Error_NoIConfigureOptions", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No IConfigureOptions&lt;&gt; or IPostConfigureOptions&lt;&gt; implementations were found, did you mean to call Configure&lt;&gt; or PostConfigure&lt;&gt;?.
        /// </summary>
        internal static string Error_NoIConfigureOptionsAndAction {
            get {
                return ResourceManager.GetString("Error_NoIConfigureOptionsAndAction", resourceCulture);
            }
        }
    }
}
