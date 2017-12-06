using LFC.Model.Commodity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LFC.Business.Server
{
    public interface ICommodityServer
    {
        void AddCommodity(CommodityModel data);
        List<CommodityModel> GetCommoditys();
        CommodityModel GetCommodity(int id);
    }
}
