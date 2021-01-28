using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleConnectionToDatabase.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime CreatedOn { get; set; }

        public int Active { get; set; }
    }
}