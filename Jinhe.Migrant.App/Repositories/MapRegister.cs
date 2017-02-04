/*************************************************************************************
 * CLR 版本：4.0.30319.42000
 * 类 名 称：MapRegister
 * 命名空间：Jinhe.Mjordomo.Repositories
 * 文 件 名：MapRegister
 * 创建时间：2016/12/15 13:12:59
 * 作    者：hzk
 * 说    明：
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

using Jinhe.Data;

namespace Jinhe.Migrant.App.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class MapRegister : EntityMapRegister
    {
        public override void RegistTo(IEntityMapContainer container)
        {
            base.RegistTo(container);
            this.AddMapping<SDistrictMap>();
            this.AddMapping<BEventinfoUpMap>();
            this.AddMapping<BHandleUpMap>();
            this.AddMapping<BEventPicUpMap>();
            this.AddMapping<MonthlyHistogramMap>();
            this.AddMapping<UUsersMap>();
            this.AddMapping<EnventallUpMap>();
            this.AddMapping<SSwlxUpMap>();
            this.AddMapping<GridCaseMap>();
            this.AddMapping<PartyUpMap>();
            this.AddMapping<ZNewsMap>();
        }
    }
}
