using DapperSampleNorthWind.Models;
using DapperSampleNorthWind.Models.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DapperSampleNorthWind.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _Productctx;
        private readonly ICategoryRepository _Categoryctx;
        private readonly ISupplierRepository _Supplierctx;

        public ProductController (IProductRepository Productctx , ICategoryRepository Categoryctx, ISupplierRepository Supplierctx)
        {
            this._Productctx = Productctx;
            this._Categoryctx = Categoryctx;
            this._Supplierctx = Supplierctx;

        }

        public async Task<IActionResult> Index()
        {
            var result = await _Productctx.GetAllAsync();
            return View( result );

        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _Categoryctx.GetCategoryForComboAsync();
            ViewBag.Suppliers = await _Supplierctx.GetSupplierForComboAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel  model)
        {
            //implement with QueryString
            //await _Productctx.InsertAsync(model);

            //implement with StoreProcedure
            //await _Productctx.InsertWithSPAsync(model);

            //implement with StoreProcedure With Returns Id
            await _Productctx.InsertWithSPReturnsValue(model);

            return RedirectToAction(nameof(Index));

        }


        public async Task<IActionResult> Edit(int id)
        {

            ViewBag.Categories = await _Categoryctx.GetCategoryForComboAsync();
            ViewBag.Suppliers = await _Supplierctx.GetSupplierForComboAsync();
            var product = await _Productctx.GetByIdAsync(id);
            return View(product);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            await _Productctx.UpdateAsync(model);
            return RedirectToAction(nameof(Index));
        }




    }
}
