using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleConnectionToDatabase.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Price { get; set; }

        public string Discount { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime EditedOn { get; set; }
    }
}
