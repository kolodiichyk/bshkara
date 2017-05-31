using System;
using PropertyChanged;

namespace Bshkara.Mobile.Helpers
{
    [ImplementPropertyChanged]
    public class MenuItem
    {
        public int Index { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public Action Action { get; set; }
    }
}