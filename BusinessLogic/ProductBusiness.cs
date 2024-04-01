using POS_API.DatabaseLogic;
using POS_API.Models;
using POS_API.POS_Database;
using System;
using System.Collections.Generic;

namespace POS_API.BusinessLogic
{
    public class ProductBusiness
    {
        private readonly ProductDAT _productDAT;

        public ProductBusiness(ProductDAT productDAT)
        {
            _productDAT = productDAT ?? throw new ArgumentNullException(nameof(productDAT));
        }

        public ResponseViewModel AddProduct(ProductViewModel productRequest)
        {
            var product = new Product
            {
                ProductType = (string)productRequest.ProductType, // Cast the property to string
                ProductCode = $"{productRequest.Code} {productRequest.BarcodeSymbology}", // Correct property names
                ProductName = productRequest.ProductName,
                Price = productRequest.Price,
                Category = productRequest.Category,
                BrandName = productRequest.BrandName,
                Cost = productRequest.Cost,
                Quantity = productRequest.Quantity,
                TaxMethod = productRequest.TaxMethod // Correct property name
            };

            return _productDAT.AddProduct(product);
        }

        public List<ProductViewModel> GetProductList()
        {
            var productList = _productDAT.GetProductList();
            var productViewModelList = new List<ProductViewModel>();

            foreach (var product in productList)
            {
                var productCodeParts = product.ProductCode.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (productCodeParts.Length >= 2)
                {
                    productViewModelList.Add(new ProductViewModel
                    {
                        ProductType = product.ProductType,
                        ProductCode = product.ProductCode,
                        ProductName = product.ProductName,
                        Price = product.Price,
                        Category = product.Category,
                        BrandName = product.BrandName,
                        Cost = product.Cost,
                        Quantity = product.Quantity,
                        TaxMethod = product.TaxMethod,
                        Code = productCodeParts[0],
                        BarcodeSymbology = productCodeParts[1]
                    });
                }
                else
                {
                    // Handle cases where ProductCode does not contain a space
                }
            }

            return productViewModelList;
        }


        public ProductViewModel GetProductById(int productId)
        {
            var product = _productDAT.GetProductById(productId);

            if (product != null)
            {
                return new ProductViewModel
                {
                    ProductType = (string)product.ProductType, // Cast the property to string
                    Code = product.ProductCode.Split(' ')[0], // Correct property name
                    BarcodeSymbology = product.ProductCode.Split(' ')[1], // Correct property name
                    ProductName = product.ProductName,
                    Price = product.Price,
                    Category = product.Category,
                    BrandName = product.BrandName,
                    Cost = product.Cost,
                    Quantity = product.Quantity,
                    TaxMethod = product.TaxMethod // Correct property name
                };
            }
            else
            {
                return null;
            }
        }

        public ResponseViewModel UpdateProduct(ProductViewModel updatedProduct)
        {
            var product = new Product
            {
                productId = updatedProduct.ProductId,
                ProductType = (string)updatedProduct.ProductType, // Cast the property to string
                ProductCode = $"{updatedProduct.Code}   {updatedProduct.BarcodeSymbology}", // Correct property names
                ProductName = updatedProduct.ProductName,
                Price = updatedProduct.Price,
                Category = updatedProduct.Category,
                BrandName = updatedProduct.BrandName,
                Cost = updatedProduct.Cost,
                Quantity = updatedProduct.Quantity,
                TaxMethod = updatedProduct.TaxMethod // Correct property name
            };

            return _productDAT.UpdateProduct(product);
        }


        public ResponseViewModel DeleteProduct(int productId)
        {
            return _productDAT.DeleteProduct(productId);
        }
    }
}
