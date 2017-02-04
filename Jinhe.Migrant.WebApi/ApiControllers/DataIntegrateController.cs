using Jinhe.Data;
using Jinhe.Mjordomo.Models;
using Jinhe.Mjordomo.Services;
using Jinhe.Migrant.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Jinhe.Migrant.WebApi.ApiControllers
{

    /// <summary>
    /// 需整合的数据 
    /// </summary>
    [RoutePrefix("api/DataIntegrate")]
    public class DataIntegrateController : ApiController
    {
        /// <summary>
        /// 获取当前月份每日的案卷统计情况
        /// </summary>
        /// <returns></returns>
        public IEnumerable<MonthlyHistogram> Get(string gridId)
        {
            Services.DataIntegrate service = new Services.DataIntegrate();
            return service.GetMonthlyHistogramData(gridId);
        }

        /// <summary>
        /// 根据日期类型获取网格中的案卷数（AcceptNum当日受理，RegisterNum当日立案，CloseNum当日结案，CaseSum总案卷）
        /// </summary>
        /// <param name="mapID">网格号</param>
        /// <param name="dateType">日期类型（1为当天）</param>
        /// <returns></returns>
        [Route("GetGridCaseNum")]
        public dynamic GetGridCaseNum(string mapID, int dateType)
        {
            Mjordomo.Services.DataIntegrate service = new Services.DataIntegrate();
            return service.GetCaseNum(mapID, dateType);
        }
    }
}
