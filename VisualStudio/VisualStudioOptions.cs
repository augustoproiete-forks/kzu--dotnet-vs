﻿using System;
using System.Linq;
using System.Collections.Immutable;
using Mono.Options;

namespace VisualStudio
{
    /// <summary>
    /// Provides visual studio selection options
    /// </summary>
    public class VisualStudioOptions : OptionSet
    {
        string[] channelShortcuts = new[] { "pre", "preview", "int", "internal", "master" };
        string[] skuShortcuts = new[] { "e", "ent", "enterprise", "p", "pro", "professional", "c", "com", "community" };

        public VisualStudioOptions(bool showChannel = true, bool showSku = true, bool showNickname = true)
        {
            if (showChannel)
            {
                // Channel
                Add("pre|preview", "Install preview version", _ => Channel = VisualStudio.Channel.Preview);
                Add("int|internal", "Install internal (aka 'dogfood') version", _ => Channel = VisualStudio.Channel.IntPreview);
                Add("master", "Install master version", _ => Channel = VisualStudio.Channel.Master, hidden: true);
            }

            if (showSku)
            {
                // Sku
                Add("sku:", "Edition, one of [e|ent|enterprise], [p|pro|professional] or [c|com|community]", s => Sku = SkuOption.Parse(s));
            }

            if (showNickname)
            {
                // Nickname
                Add("nick|nickname:", "Optional nickname to assign to the installation", n => Nickname = n);
            }
        }

        protected override bool Parse(string argument, OptionContext c)
        {
            if (channelShortcuts.Contains(argument.ToLowerInvariant()))
                argument = "--" + argument;

            if (skuShortcuts.Contains(argument.ToLowerInvariant()))
                argument = "--sku=" + argument;

            return base.Parse(argument, c);
        }

        public Channel? Channel { get; private set; }

        public Sku? Sku { get; private set; }

        public string Nickname { get; private set; }
    }
}