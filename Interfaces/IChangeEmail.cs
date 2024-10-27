using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainActivity
{
    interface IChangeEmail
    {
        public string NewEmail { get; set; }
        public string Username { get; set; }
        void EmailChanged();
    }
}
