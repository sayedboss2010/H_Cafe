using Cafe.Repository.Repositories.MenuCafe;
using Cafe.VM.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Cafe.VM.ViewModels.MenuCafeVM;

namespace Cafe.Services.MenuCafe
{
    public class MenuCafeServes
    {
        private readonly MenuCafeRepo _MenuCafeRepo;
        public IList<Category> List()
        {
            try
            {
                return _MenuCafeRepo.list();
            }
            catch (Exception)
            {
                return new List<Category>();
            }
        }
    }
}
