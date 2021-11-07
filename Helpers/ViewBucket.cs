using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DaysGoneModManager.Views;

namespace DaysGoneModManager.Helpers
{
    public class ViewBucket
    {
        public ViewBucket(IBaseView baseView) => ViewRef = baseView;

        public IBaseView ViewRef { get; set; }
    }
}
