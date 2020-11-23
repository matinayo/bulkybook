using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.Models.ViewModel
{
    public class ProductVM
    {
        // display all product details
        public Product Product { get; set; }

        // dropdown for Category and CoverType
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        // SelectListItem so you can use directly in dropdown
    
        public IEnumerable<SelectListItem> CoverTypeList { get; set; }
    }
}
