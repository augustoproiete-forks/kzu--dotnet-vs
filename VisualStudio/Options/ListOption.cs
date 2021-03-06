﻿using System;
using System.Collections.Generic;
using Mono.Options;

namespace VisualStudio
{
    class ListOption : OptionSet<bool>
    {
        public ListOption() => Add("list", "Shows result as a list", x => Value = x != null);

        protected override bool Parse(string argument, OptionContext c)
        {
            if ("list".Equals(argument, StringComparison.OrdinalIgnoreCase))
                argument = "--list";

            return base.Parse(argument, c);
        }
    }
}
