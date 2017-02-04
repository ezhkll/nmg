/*************************************************************************************
 * CLR 版本：4.0.30319.42000
 * 类 名 称：BEventinfoUpDal
 * 命名空间：Jinhe.Mjordomo.Dal
 * 文 件 名：BEventinfoUpDal
 * 创建时间：2016/12/14 16:48:15
 * 作    者：hzk
 * 说    明：
 * 修改时间：
 * 修 改 人：
*************************************************************************************/

using Jinhe.Migrant.App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jinhe.Data;

namespace Jinhe.Migrant.App.Dal
{
    /// <summary>
    /// 需要数据整合的数据层
    /// </summary>
    public class DataIntegrateDal
    {
        private IDataGateway _dataGateway = DataGateway.Instance;
        /// <summary>
        /// 统计当月的案卷并返回结果
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MonthlyHistogram> CountByDate(string gridId)
        {
            return _dataGateway
                 .Query<MonthlyHistogram>("select count(1) num,a.dat from(select eventid,to_char(reporttime,'yyyy-mm-dd') as dat from b_eventinfo_up where BASICGRID like '%'||{0}||'%') a where to_char(sysdate, 'yyyy-mm') = substr(a.dat, 1, 7) group by a.dat", gridId);
        }

        /// <summary>
        /// 立案
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public GridCase RegisterCaseCount(string mapId, DateTime start, DateTime end)
        {
            var entity = _dataGateway.Query<GridCase>("select count(1) as count,'立案' as name from b_eventinfo_up where exists(select * from b_handle_up  where b_handle_up.stepid>2 and b_handle_up.stepid<6 and b_handle_up.endtime>={1} and b_handle_up.endtime<{2}  and b_eventinfo_up.eventid=b_handle_up.eventid) and b_eventinfo_up.basicgrid like {0}||'%'", mapId, start, end).First();
            return entity;
        }

        /// <summary>
        /// 受理
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public GridCase AcceptCaseCount(string mapId, DateTime start, DateTime end)
        {

            var entity = _dataGateway.Query<GridCase>("select count(1) as count,'受理' as name from b_eventinfo_up where exists(select * from b_handle_up  where b_handle_up.stepid<=2 and b_handle_up.endtime>={1} and b_handle_up.endtime<{2}  and b_eventinfo_up.eventid=b_handle_up.eventid) and b_eventinfo_up.basicgrid like {0}||'%'", mapId, start, end).First();
            return entity;

        }

        /// <summary>
        /// 结案
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public GridCase ClosedCaseCount(string mapId, DateTime start, DateTime end)
        {
            var entity = _dataGateway.Query<GridCase>("select count(1) as count,'结案' as name from b_eventinfo_up where exists(select * from b_handle_up  where b_handle_up.stepid=6 and b_handle_up.endtime>={1} and b_handle_up.endtime<{2} and b_eventinfo_up.eventid=b_handle_up.eventid) and b_eventinfo_up.basicgrid like {0}||'%'", mapId, start, end).First();
            return entity;
        }

        /// <summary>
        /// 总数
        /// </summary>
        /// <param name="mapId"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public GridCase CaseSum(string mapId, DateTime start, DateTime end)
        {
            var entity = _dataGateway.Query<GridCase>("select count(1) as count,'总数' as name from b_eventinfo_up where exists(select * from b_handle_up  where b_handle_up.stepid<=2 and  b_eventinfo_up.eventid=b_handle_up.eventid) and b_eventinfo_up.basicgrid like {0}||'%'", mapId, start, end).First();
            return entity;
        }
    }


}
