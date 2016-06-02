using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JPK_WEB_MVC.ViewModels
{
    public class LayerInfoVM
    {
        public IEnumerable<View_Company_CounterAll> getLayerInfo
        { get; set; }
        public IEnumerable<View_Company_CounterAll> getCompanyInfo
        { get; set; }
    }
}