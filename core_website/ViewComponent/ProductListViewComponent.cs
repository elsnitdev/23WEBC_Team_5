using core_website.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
public class ProductListViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(IEnumerable<SanPham> products) { 
    // Receive the products from the controller and pass to the view
    return View("~/Views/Shared/Component/SanPhamCardList.cshtml", products);
  }
}