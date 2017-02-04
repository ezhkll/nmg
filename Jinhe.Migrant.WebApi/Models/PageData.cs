using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Jinhe.Migrant.WebApi.Models
{
    public class PageData<T>
    {
        public IEnumerable<T> Items { set; get; }

        private int _total = 0;
        public int Total
        {
            set
            {
                if (this.Items != null && value < Items.Count())
                {
                    throw new ASoftException("未正确设置分页数据的Total");
                }
                _total = value;
            }
            get
            {
                if (this.Items != null && _total < Items.Count())
                {
                    throw new ASoftException("未正确设置分页数据的Total");
                }
                return _total;
            }
        }
    }
}