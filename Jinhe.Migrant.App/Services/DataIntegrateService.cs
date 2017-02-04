/*************************************************************************************
 * CLR 版本：4.0.30319.42000
 * 类 名 称：WorkCaseService
 * 命名空间：Jinhe.Mjordomo.App.Services
 * 文 件 名：WorkCaseService
 * 创建时间：2016/12/14 16:31:45
 * 作    者：hzk
 * 说    明：
 * 修改时间：
 * 修 改 人：
*************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jinhe.Migrant.App.Models;
using Jinhe.Migrant.App.Dal;
using Jinhe.Migrant.App.ViewModels;
using Jinhe.Data;
using System.Transactions;

namespace Jinhe.Migrant.App.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class DataIntegrate
    {
        private readonly DataIntegrateDal dataIntegrateDal = new DataIntegrateDal();

        public IEnumerable<MonthlyHistogram> GetMonthlyHistogramData(string gridId)
        {
            DateTime firstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1); //月首
            DateTime lastDay = firstDay.AddMonths(1).AddDays(-1);//月末
            IEnumerable<MonthlyHistogram> list = dataIntegrateDal.CountByDate(gridId);//当月的案卷统计情况
            List<MonthlyHistogram> result = new List<MonthlyHistogram>();
            while (firstDay <= lastDay)
            {
                var key = firstDay.ToString("yyyy-MM-dd");
                MonthlyHistogram entity = null;
                var query = list.Where(item => item.Dat == key);
                if (query.Any())
                {
                    entity = query.First();
                }
                else
                {
                    entity = new MonthlyHistogram()
                    {
                        Dat = key
                    };
                }
                firstDay = firstDay.AddDays(1);
                result.Add(entity);
            }
            return result;
        }

        /// <summary>
        /// 获取事件数
        /// </summary>
        /// <param name="mapID">网格编码</param>
        /// <param name="dateType">日期类型：（1：当天、2:当月、3:当年、4：全部）</param>
        /// <returns></returns>
        public dynamic GetCaseNum(string mapID, int dateType)
        {
            dynamic GridCaseCount = null;
            switch (dateType)
            {
                case 1:
                    DateTime today = DateTime.Now.Date;
                    DateTime enday = today.AddDays(1);
                    GridCase acceptCase = dataIntegrateDal.AcceptCaseCount(mapID, today, enday);
                    GridCase registerCase = dataIntegrateDal.RegisterCaseCount(mapID, today, enday);
                    GridCase closedCase = dataIntegrateDal.ClosedCaseCount(mapID, today, enday);
                    GridCase caseSum = dataIntegrateDal.CaseSum(mapID, today, enday);
                    GridCaseCount = new
                    {
                        AcceptNum = acceptCase.Num,
                        RegisterNum = registerCase.Num,
                        CloseNum = registerCase.Num,
                        CaseSum = caseSum.Num
                    };


                    break;

            }

            return GridCaseCount;



        }

    }


}
