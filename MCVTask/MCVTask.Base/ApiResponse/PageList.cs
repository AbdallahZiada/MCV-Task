using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCVTask.Base.ApiResponse
{
    public class PageList<T>
    {
        public PageList()
        {
            DataList = new List<T>();
        }

        public List<T> DataList { get; private set; }
        public int TotalCount { get; private set; }

        public void SetResult(int totalCount, List<T> dataList)
        {
            TotalCount = totalCount;
            DataList = dataList;
        }
    }
}
