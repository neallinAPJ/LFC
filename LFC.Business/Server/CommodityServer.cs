using LFC.DataProvider.Model;
using LFC.Model;
using LFC.Model.Commodity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;


namespace LFC.Business.Server
{
    public class CommodityServer: ICommodityServer,IDisposable
    {
        private readonly IOptions<AppSettings> _settings;
        public CommodityServer(IOptions<AppSettings> settings)
        {
            _settings = settings;
        }
        internal IDbConnection dbConn
        {
            get
            {
                return new SqlConnection(_settings.Value.MySqlConnectionStrings);
            }
        }

        public void Dispose()
        {
            if (dbConn != null)
            {
                dbConn.Dispose();
                dbConn.Close();
            }
        }

        public void AddCommodity(CommodityModel data)
        {
            Commodity commodityData = new Commodity();
            commodityData.Category = data.Category;
            commodityData.Image = data.ImageData;
            commodityData.Name = data.Name;
            commodityData.Url = data.Url;
            dbConn.Insert(commodityData);
        }
        public CommodityModel GetCommodity(int id)
        {
            var data = dbConn.QueryFirstOrDefault<Commodity>("select * from Commodity where ID=@ID",new { ID= id });
            CommodityModel commodityData = new CommodityModel();
            commodityData.ID = id;
            commodityData.Category = data.Category;
            commodityData.ImageData = data.Image;
            commodityData.Name = data.Name;
            commodityData.Url = data.Url;
            return commodityData;
        }
        public List<CommodityModel> GetCommoditys()
        {
            var datas=dbConn.Query<Commodity>("select * from Commodity");
            List<CommodityModel> commoditys = new List<CommodityModel>();
            foreach (var item in datas)
            {
                CommodityModel commodity = new CommodityModel();
                commodity.ID = item.ID;
                commodity.Category = item.Category;
                commodity.ImageData = item.Image;
                commodity.Name = item.Name;
                commodity.Url = item.Url;
                commoditys.Add(commodity);
            }
            return commoditys;
        }

    }
}
