/*************************************************************************************
 * CLR 版本：4.0.30319.42000
 * 类 名 称：Class1
 * 命名空间：Jinhe.Mjordomo.Repositories
 * 文 件 名：Class1
 * 创建时间：2016/12/16 8:40:01
 * 作    者：hzk
 * 说    明：
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

using Jinhe.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jinhe.Migrant.App.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class MonthlyHistogramMap : EntityMap<Jinhe.Migrant.App.Models.MonthlyHistogram>
    {
        public override void Mapping()
        {
            this.ToTable("b_eventinfo_up");
            this.Property(x => x.Dat).HasColumnName("dat");
            this.Property(x => x.Num).HasColumnName("num");
        }
    }
}
