using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jinhe.Migrant.App.Models
{
    /// <summary>
    /// 
    ///</summary>
    public class SDistrict
    {

        ///<summary> 
        /// 
        ///</summary> 
        public int Dwid { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public String Gbbh { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public String Parent { set; get; }

        ///<summary> 
        /// 网格属性0农村1城市 ye
        ///</summary> 
        public Int16 Lx { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public String CenterY { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public String Kzmj { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public String Dwlx { set; get; }

        ///<summary> 
        /// 上级id
        ///</summary> 
        public long Sjid { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public String Place { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public String CenterX { set; get; }

        ///<summary> 
        /// 社区图片
        ///</summary> 
        public String Mappic { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public Int16 Lvl { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public String Dwmc { set; get; }

        ///<summary> 
        /// 
        ///</summary> 
        public String Phone { set; get; }



        public int EventCount { set; get; }

    }
}
