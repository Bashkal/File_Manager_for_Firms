using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ViewComponents
{
    public class Departments : ViewComponent
    {
        private readonly DataBaseContext _dataBase;

        public Departments(DataBaseContext dataBase)
        {
            _dataBase = dataBase;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
             
            return View(await _dataBase.Departments.ToListAsync());


        }
    }
}
