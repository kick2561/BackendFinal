﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StoreBuy {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "15.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("StoreBuy.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to dd-MMMM-yyyy HH:mm.
        /// </summary>
        internal static string DateTimeFormat {
            get {
                return ResourceManager.GetString("DateTimeFormat", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email Verification.
        /// </summary>
        internal static string EmailVerificationSubject {
            get {
                return ResourceManager.GetString("EmailVerificationSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to siddichandu03@gmail.com.
        /// </summary>
        internal static string FromMailAddress {
            get {
                return ResourceManager.GetString("FromMailAddress", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 10.
        /// </summary>
        internal static string ItemsInCategoryLimit {
            get {
                return ResourceManager.GetString("ItemsInCategoryLimit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 1.
        /// </summary>
        internal static string LimitPerSlot {
            get {
                return ResourceManager.GetString("LimitPerSlot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to smtp.gmail.com.
        /// </summary>
        internal static string MailHost {
            get {
                return ResourceManager.GetString("MailHost", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to chandu@12345.
        /// </summary>
        internal static string MailPassword {
            get {
                return ResourceManager.GetString("MailPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to -1.
        /// </summary>
        internal static string NotAPrice {
            get {
                return ResourceManager.GetString("NotAPrice", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Order Details.
        /// </summary>
        internal static string OrderSubject {
            get {
                return ResourceManager.GetString("OrderSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Your OTP is .
        /// </summary>
        internal static string OTPBody {
            get {
                return ResourceManager.GetString("OTPBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Forgot Password.
        /// </summary>
        internal static string OTPSubject {
            get {
                return ResourceManager.GetString("OTPSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 15.
        /// </summary>
        internal static string OTPTimeSpan {
            get {
                return ResourceManager.GetString("OTPTimeSpan", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 587.
        /// </summary>
        internal static string SMTPPort {
            get {
                return ResourceManager.GetString("SMTPPort", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 10.
        /// </summary>
        internal static string StoreLimit {
            get {
                return ResourceManager.GetString("StoreLimit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to siddichandu02@gmail.com.
        /// </summary>
        internal static string ToMailAddress {
            get {
                return ResourceManager.GetString("ToMailAddress", resourceCulture);
            }
        }
    }
}
