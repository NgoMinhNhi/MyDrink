﻿// <auto-generated />
using System;
using System.Net.Http;
using System.Collections.Generic;
using Refit;
using System.Text;
using System.Threading.Tasks;

/* ******** Hey You! *********
 *
 * This is a generated file, and gets rewritten every time you build the
 * project. If you want to edit it, you need to edit the mustache template
 * in the Refit package */

#pragma warning disable
namespace MyDrink.RefitInternalGenerated
{
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate)]
    sealed class PreserveAttribute : Attribute
    {

        //
        // Fields
        //
        public bool AllMembers;

        public bool Conditional;
    }
}
#pragma warning restore

namespace MyDrink.Interfaces
{
    using MyDrink.RefitInternalGenerated;

    /// <inheritdoc />
    [global::System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [global::System.Diagnostics.DebuggerNonUserCode]
    [Preserve]
    [global::System.Reflection.Obfuscation(Exclude=true)]
    partial class AutoGeneratedIMyAPI : IMyAPI
    {
        /// <inheritdoc />
        public HttpClient Client { get; protected set; }
        readonly IRequestBuilder requestBuilder;

        /// <inheritdoc />
        public AutoGeneratedIMyAPI(HttpClient client, IRequestBuilder requestBuilder)
        {
            Client = client;
            this.requestBuilder = requestBuilder;
        }

        /// <inheritdoc />
        Task<string> IMyAPI.RegisterUser(Dictionary<string, object> data)
        {
            var arguments = new object[] { data };
            var func = requestBuilder.BuildRestResultFuncForMethod("RegisterUser", new Type[] { typeof(Dictionary<string, object>) });
            return (Task<string>)func(Client, arguments);
        }

        /// <inheritdoc />
        Task<string> IMyAPI.LoginUser(Dictionary<string, object> data)
        {
            var arguments = new object[] { data };
            var func = requestBuilder.BuildRestResultFuncForMethod("LoginUser", new Type[] { typeof(Dictionary<string, object>) });
            return (Task<string>)func(Client, arguments);
        }
    }
}
