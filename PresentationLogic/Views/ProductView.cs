using ForApplication.BusinecLogic.Services;
using ForApplication.Data;
using ForApplication.DataAccess;
using ForApplication.Models;
using ForApplication.Models.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace ForApplication.PresentationLogic.Views;

public class ProductView
{
    private readonly AppDbContext _context;
    private readonly IProductServic _product;
    public ProductView(IProductServic product, AppDbContext context)
    {
        this._product = product;
        this._context = context;
    }


    public async Task Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("1. Yangi mahsulot qo'shish");
            Console.WriteLine("2. Mahsulotlarni ko'rish");
            Console.WriteLine("3. Mahsulotni yangilash");
            Console.WriteLine("4. Mahsulotni o'chirish");
            Console.WriteLine("5. LINQ so'rovlarini bajarish");
            Console.WriteLine("0. Chiqish");
            Console.WriteLine();
            Console.Write("Tanlovingiz: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await AddProduct();
                    break;
                case "2":
                    await ReadProduct();
                    break;
                case "3":
                    await UpdateProduct();
                    break;
                case "4":
                    await DeleteProduct();
                    break;
                case "5":
                    LINQQueries();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Noto'g'ri tanlov.");
                    break;
            }
        }
    }


    async Task AddProduct()
    {
        Console.Clear();
        Console.WriteLine("Yangi mahsulot qo'shish");


        Console.Write("Mahsulot nomi: ");
        string? product = Console.ReadLine();

        Console.Write("Mahsulot narxi: ");
        decimal price = decimal.Parse(Console.ReadLine());

        Console.Write("Mahsulot kategoriyasi: ");
        string? category = Console.ReadLine();

        Console.Write("Ombordagi miqdori: ");
        int stockQuantity = int.Parse(Console.ReadLine());

        Console.Write("Yetkazib beruvchining Idsi: ");
        int supplierId = int.Parse(Console.ReadLine());

        var result = new Product()
        {
            Name = product,
            Price = price,
            Category = category,
            StockQuantity = stockQuantity,
            SupplierId = supplierId
        };

        try
        {
            bool isCreated = await this._product.AddProductAsync(result);

            if (isCreated)
            {
                Console.Clear();
                Console.WriteLine("Mahsulot muvaffaqiyatli qo'shildi.");
                Console.ReadKey();
            }
        }
        catch (AlreadyExistException ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }

    }


    async Task ReadProduct()
    {
        Console.Clear();

        var result = await _product.GetAllProducts().ToListAsync();

        Console.WriteLine("Ma'lumotlar bazasidagi barcha mahsulotlar!");
        Console.WriteLine();

        result.ForEach(p =>
            Console.WriteLine($"ID: {p.Id},  Nomi: {p.Name}, Narxi: {p.Price}, " +
                              $"Kategoriyasi: {p.Category}, Zaxirasi: {p.StockQuantity},  Yetkazib beruvchi ID: {p.SupplierId}"));

        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine("Davom etish uchun biror tugmani bosing...");
        Console.ReadKey();
    }


    async Task UpdateProduct()
    {
        Console.Clear();
        await ReadProduct();

        Console.Write("Yangilamoqchi bo'lgan mahsulot ID sini kiriting: ");
        int id = int.Parse(Console.ReadLine());
        Console.Clear();

        Console.Write("Mahsulot nomi (agar o'zgarishsiz qolishini xohlasangiz '~' ): ");
        string? newName = Console.ReadLine();
        if (newName == "~") newName = null;

        Console.Write("Mahsulot narxi (agar o'zgarishsiz qolishini xohlasangiz '~' ): ");
        string? newPrice = Console.ReadLine();
        decimal? price = null;
        if (newPrice == "~")
            price = null;
        else
            price = decimal.Parse(newPrice);

        Console.Write("Mahsulot kategoriyasi (agar o'zgarishsiz qolishini xohlasangiz '~' ): ");
        string? newCategory = Console.ReadLine();
        if (newCategory == "~") newCategory = null;

        Console.Write("Mahsulot zaxirasi (agar o'zgarishsiz qolishini xohlasangiz '~' ): ");
        string? newSQ = Console.ReadLine();
        int? sq = null;
        if (newSQ == "~")
            sq = null;
        else
            sq = int.Parse(newSQ);


        var result = new Product()
        {
            Id = id,
            Name = newName,
            Price = price,
            Category = newCategory,
            StockQuantity = sq
        };

        try
        {
            bool isUpdate = await this._product.UpdateProductAsync(result);

            if (isUpdate)
            {
                Console.WriteLine();
                Console.WriteLine("Mahsulot muvaffaqiyatli yangilandi.  Davom etish uchun biror tugmani bosing...");
                Console.ReadKey();
            }
        }
        catch (NotFoundException ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }

    }


    async Task DeleteProduct()
    {
        Console.Clear();

        Console.Write("O'chirmoqchi bo'lgan mahsulot ID sini kiriting (orqaga qaytish uchun '0' ni bosing): ");
        string? idInput = Console.ReadLine();

        if (idInput == "0") return;

        if (!int.TryParse(idInput, out int id))
        {
            Console.WriteLine("ID noto'g'ri formatda kiritildi. Dastur to'xtatildi.");
            Console.ReadKey();
            return;
        }

        try
        {
            bool isDelete = await this._product.DeleteProductAsync(id: id);

            if (isDelete)
            {
                Console.WriteLine();
                Console.WriteLine("Mahsulot muvaffaqiyatli o'chirildi.  Davom etish uchun biror tugmani bosing...");
                Console.ReadKey();
            }
        }
        catch (NotFoundException ex)
        {
            await Console.Out.WriteLineAsync(ex.Message);
        }
    }


    public void LINQQueries()
    {
        Console.Clear();
        Console.WriteLine("LINQ");
        Console.WriteLine();
        Console.WriteLine("1. Filtrlash");
        Console.WriteLine("2. Tartiblash");
        Console.WriteLine("3. Guruhlash");
        Console.WriteLine("4. Proeksiya");
        Console.WriteLine("5. Agregatsiya");
        Console.WriteLine("6. Birlashma");
        Console.WriteLine("7. Guruhlash va Agregatsiya");
        Console.WriteLine("0. Orqaga qaytish");
        Console.WriteLine();
        Console.Write("Tanlovingiz: ");

        string? option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Filtering();
                break;
            case "2":
                Arrange();
                break;
            case "3":
                Grouping();
                break;
            case "4":
                Projection();
                break;
            case "5":
                Aggregation();
                break;
            case "6":
                Unification();
                break;
            case "7":
                GroupingAndAggregation();
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Noto'g'ri tanlov.");
                break;
        }
        Console.ReadKey();
    }


    private void Filtering()
    {
        Console.Clear();

        var expensiveProducts = _context.Products.Where(p => p.Price > 10000).ToList();

        Console.WriteLine("Narxi 10000 dan katta bo'lgan mahsulotlar:");
        foreach (var product in expensiveProducts)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
        }
    }


    private void Arrange()
    {
        Console.Clear();

        var sortedByPrice = _context.Products.OrderBy(p => p.Price).ToList();
        Console.WriteLine("Narx bo'yicha tartiblangan mahsulotlar:");
        foreach (var product in sortedByPrice)
        {
            Console.WriteLine($"Name: {product.Name}, Price: {product.Price}");
        }

        Console.WriteLine();
        
        var sortedByCategoryAndName = _context.Products.OrderBy(p => p.Category).ThenBy(p => p.Name).ToList();
        Console.WriteLine("Kategoriyasi va Nomi bo'yicha tartiblangan mahsulotlar:");
        foreach(var product in sortedByCategoryAndName)
        {
            Console.WriteLine($"Category: {product.Category}, Name: {product.Name}");
        }
    }


    private void Grouping()
    {
        Console.Clear();

        var groupedByCategory = _context.Products.GroupBy(p => p.Category).
            Select(g => new {Category = g.Key, Count = g.Count()}).ToList();
        Console.WriteLine("Kategoriya bo'yicha guruhlangan mahsulotlar:");
        foreach (var group in groupedByCategory)
        {
            Console.WriteLine($"Category: {group.Category}, Count: {group.Count}");
        }

        Console.WriteLine();
        
        var groupedByPriceRange = _context.Products.GroupBy(p => p.Price <= 50 ? "0-50" : p.Price <= 100 ? "51-100" : "100+")
                                                  .Select(g => new { PriceRange = g.Key, Count = g.Count() }).ToList();
        Console.WriteLine("Mahsulotlarni narx diapazoni bo'yicha guruhlang mahsulotlar:");
        foreach (var group in groupedByPriceRange)
        {
            Console.WriteLine($"Price: {group.PriceRange}");
        }
    }


    private void Projection()
    {
        Console.Clear();

        var productNamesAndPrices = _context.Products.Select(p => new { p.Name, p.Price }).ToList();
        Console.WriteLine("\nMahsulot nomlari va narxlari:");
        foreach (var item in productNamesAndPrices)
        {
            Console.WriteLine($"Name: {item.Name}, Price: {item.Price}");
        }

        Console.WriteLine();

        var productsWithStockStatus = _context.Products.Select(p => new {p.Name, StockStatus = p.StockQuantity > 0 ? 
                                                             "Omborda mavjud" : "Zahirada qolmagan"}).ToList();
        productsWithStockStatus.ForEach(product => Console.WriteLine($"Name: {product.Name}, Status: {product.StockStatus}"));
    }


    private void Aggregation()
    {
        Console.Clear();

        var averagePrice = _context.Products.Average(p => p.Price);
        var maxPrice = _context.Products.Max(p => p.Price);
        var minPrice = _context.Products.Min(p => p.Price);
        var totalStock = _context.Products.Sum(p => p.StockQuantity);

        Console.WriteLine($"Mahsulotlarning o'rtacha narxi: {averagePrice}");
        Console.WriteLine($"Mahsulotlar orasidagi maksimal narx: {maxPrice}");
        Console.WriteLine($"Mahsulotlar orasidagi minimal narx: {minPrice}");
        Console.WriteLine($"Ombordagi mahsulotlarning umumiy miqdori: {totalStock}");
    }


    private void Unification()
    {
        Console.Clear();

        var productsWithSuppliers = _context.Products.Join(_context.Suppliers, p => p.SupplierId, s => s.Id, (
                                                    p, s) => new { p.Name, SupplierName = s.Name }).ToList();

        Console.WriteLine("Mahsulotlar va ularning yetkazib beruvchilari:");
        foreach (var item in productsWithSuppliers)
        {
            Console.WriteLine($"Product: {item.Name}, Supplier: {item.SupplierName}");
        }
    }


    private void GroupingAndAggregation()
    {
        Console.Clear();

        Console.WriteLine("Kategoriya bo'yicha guruhlangan va o'rtacha narxlar:");

        _context.Products.GroupBy(p => p.Category)
            .Select(g => new { Category = g.Key, AveragePrice = g.Average(p => p.Price) }).ToList()
            .ForEach(group => Console.WriteLine($"Category: {group.Category}, Average Price: {group.AveragePrice}"));
    }
}