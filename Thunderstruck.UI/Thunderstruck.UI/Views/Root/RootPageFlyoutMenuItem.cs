using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thunderstruck.UI.Views.Root
{
    public class RootPageFlyoutMenuItem
    {
        public RootPageFlyoutMenuItem()
        {
            TargetType = typeof(RootPageFlyoutMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}