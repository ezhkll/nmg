using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Jinhe.Migrant.App.ViewModels
{

    public class BacklogInfo
    {
        /// <summary>
        /// 审核人id
        /// </summary>
        public String Dwid { set; get; }
        /// <summary>
        /// 网格号
        /// </summary>
        public string MapId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Stepid { get; set; }

        public int deptId { get; set; }

    }
}
