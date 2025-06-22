using Cafe.Repository.Repositories.Generic;
using Cafe.Repository.Repositories.MenuCafe;
using Cafe.VM.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cafe.VM.ViewModels.MenuCafeCategoryVM;

namespace Cafe.Services.MenuCafe
{
    public class MenuCafeService
    {
        private readonly MenuCafeRepo _MenuCafeRepo;
        public MenuCafeService(MenuCafeRepo MenuCafeRepo)
        {
            _MenuCafeRepo = MenuCafeRepo;
        }
        public IList<MenuCafeCategoryVM> List()
        {
            try
            {
                var list = _MenuCafeRepo.list();
                return list;
            }
            catch (Exception)
            {
                return new List<MenuCafeCategoryVM>();
            }
        }
    }
}
