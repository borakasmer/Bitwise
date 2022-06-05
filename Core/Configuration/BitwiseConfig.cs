using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Configuration
{
    public  class BitwiseConfig
    {
        #region Props      
        public string RedisEndPoint { get; set; }
        public string RedisPort { get; set; }
        public string RedisPassword { get; set; }              
        #endregion
    }
}
