﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FolderJpgCreator.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("FolderJpgCreator.Properties.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Error: Assumed JPEG cover image not present ({0})!.
        /// </summary>
        internal static string ErrorPictureNotSupported {
            get {
                return ResourceManager.GetString("ErrorPictureNotSupported", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Processing directory....
        /// </summary>
        internal static string ProcessBegin {
            get {
                return ResourceManager.GetString("ProcessBegin", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Completed. Processed {0} files, skipped {1} and wrote {2} folder.jpgs..
        /// </summary>
        internal static string ProcessFinished {
            get {
                return ResourceManager.GetString("ProcessFinished", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Scanning and processing files with {0} extension....
        /// </summary>
        internal static string ProcessingWildcardNow {
            get {
                return ResourceManager.GetString("ProcessingWildcardNow", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please type path to directory to recursively create folder.jpgs for:.
        /// </summary>
        internal static string ProcessRequestInputPath {
            get {
                return ResourceManager.GetString("ProcessRequestInputPath", resourceCulture);
            }
        }
    }
}
