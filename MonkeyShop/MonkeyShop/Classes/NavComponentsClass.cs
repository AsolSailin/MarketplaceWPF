using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MonkeyShop.Classes
{
    public class NavComponentsClass
    {
        public Page Page { get; set; }
        public NavComponentsClass(Page page)
        {
            Page = page;
        }
    }
}
