using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Options;
using LFC.Model;
using DBM = LFC.DataProvider.Model;

namespace LFC.Business.Server
{
    public class MenuServer: IMenuServer,IDisposable
    {
        private readonly IOptions<AppSettings> _settings;
        public MenuServer(IOptions<AppSettings> settings)
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
            if(dbConn!=null)
            {
                dbConn.Dispose();
                dbConn.Close();
            }
        }

        public Menu GetMenus(int menuType)
        {
            List<DBM.Menu> workMenus =dbConn.Query<DBM.Menu>("select * from menu where MenuType=@MenuType",new { MenuType = menuType }).ToList();
            DBM.Menu masterMenu = workMenus.Where(i => !i.PID.HasValue).FirstOrDefault();
            return SetMenu(workMenus, null, masterMenu.ID, masterMenu.Name, masterMenu.MenuID);
        }
        private Menu SetMenu(List<DBM.Menu> workMenus, Menu menu,int masterMenuID,string name,string menuID)
        {
            if(menu==null)
            {
                menu =new Menu();
                menu.ID = menuID;
                menu.Name = name;
            }
            foreach (var item in workMenus.Where(i=>i.PID== masterMenuID))
            {
                Menu cMenu = new Menu();
                cMenu.ID = item.MenuID;
                cMenu.Name = item.Name;
                cMenu.Target = item.Target;
                cMenu.Url = item.Url;
                SetMenu(workMenus, cMenu, item.ID,item.Name, item.MenuID);
                if(menu.Children==null)
                {
                    menu.Children = new List<Menu>();
                }
                menu.Children.Add(cMenu);
            }
            return menu;
        }
    }
}
