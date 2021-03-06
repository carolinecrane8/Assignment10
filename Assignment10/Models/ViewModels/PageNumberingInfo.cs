using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//calculate
namespace Assignment10.Models.ViewModels
{
    public class PageNumberingInfo
    {
        public int NumItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumItems { get; set; }

        //calculate num of pages

        public int NumPages => 
            (int)(Math.Ceiling((decimal)TotalNumItems / NumItemsPerPage));


    }
}
