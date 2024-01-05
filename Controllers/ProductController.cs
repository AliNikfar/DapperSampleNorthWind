using DapperSampleNorthWind.Models;
using DapperSampleNorthWind.Models.Contracts;
using ExcelDataReader;
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

        public async Task<IActionResult> Delete(int id)
        {
            await _Productctx.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        [HttpPost]
        public async Task<IActionResult> BulkInsert(IFormFile excelFile)
        {
            // First Add Package  'dotnet add package ExcelDataReader' 
            // then using MemoryStream to read Excel data

            var formFile = HttpContext.Request.Form.Files[0];
            var aa = HttpContext;
            List<ProductViewModel> products = new List<ProductViewModel>();

            using (var ms = new MemoryStream())
            {
                excelFile.CopyTo(ms);
                bool firstRow = true;

                using (var reader = ExcelReaderFactory.CreateReader(ms))
                {
                    do
                    {
                        while (reader.Read())
                        {
                            if (firstRow)// Breakes First Row
                            {
                                firstRow = false;
                                continue;

                            }
                            var product = new ProductViewModel()
                            {
                                ProductName = reader[0].ToString(),
                                CategoryID = Convert.ToInt32(reader[1]),
                                SupplierID = Convert.ToInt32(reader[2]),
                                UnitPrice = Convert.ToDouble(reader[3]),
                            };
                            products.Add(product);
                        }

                    }
                    while (reader.NextResult());
                }

            }
            return View(nameof(Index));
        }

    }





}