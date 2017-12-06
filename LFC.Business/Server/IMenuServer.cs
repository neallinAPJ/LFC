using LFC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LFC.Business.Server
{
    public interface IMenuServer
    {
        Menu GetMenus(int menuType);
    }
}
