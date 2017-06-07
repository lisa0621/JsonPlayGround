using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace JsonPlayGround.Models
{
    public class ShulinParkingLotVM
    {
        [Display(Name = "區別")]
        public string zone { get; set; }

        [Display(Name = "設置地點")]
        public string Address { get; set; }

        [Display(Name = "數量")]
        public string Number { get; set; }

        [Display(Name = "車種")]
        public string Vehicle_classification { get; set; }

        [Display(Name = "是否收費")]
        public string Charged { get; set; }

        [Display(Name = "是否有身障標誌")]
        public string Disabled_parking_sign { get; set; }

        [Display(Name = "是否有身障標線")]
        public string Disabled_parking_lot { get; set; }
    }
}