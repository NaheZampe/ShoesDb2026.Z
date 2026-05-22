using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.DependencyInjection;
using ShoesDb2026.Entities;
using ShoesDb2026.IoC;
using ShoesDb2026.Services.DTOs.Brand;
using ShoesDb2026.Services.DTOs.Genre;
using ShoesDb2026.Services.DTOs.Shoe;
using ShoesDb2026.Services.DTOs.Sport;
using ShoesDb2026.Services.Interfaces;

internal class Program
{
    static IServiceProvider provider = DependencyInyectionContainer.Configure();

    private static void Main(string[] args)
    {
        do
        {
            Console.Clear();
            Console.WriteLine("=== Shoes Manager ===");
            Console.WriteLine("1. Brands");
            Console.WriteLine("2. Genres");
            Console.WriteLine("3. Sizes");
            Console.WriteLine("4. Sports");
            Console.WriteLine("5. Sport Shoes");
            Console.WriteLine("0. Exit");
            Console.Write("Select an option: ");

            var option = Console.ReadLine();

            switch (option)
            {
                case "1": BrandsMenu(); break;
                case "2": GenresMenu(); break;
                case "3": SizesMenu(); break;
                case "4": SportsMenu(); break;
                case "5": SportShoesMenu(); break;
                case "0": return;
                default:
                    Console.WriteLine("Invalid option. Press any key...");
                    Console.ReadKey();
                    break;
            }
        } while (true);
    }

    // ─── BRANDS ───────────────────────────────────────────
    private static void BrandsMenu()
    {
        using (var scoped = provider.CreateScope())
        {
            var services = scoped.ServiceProvider.GetRequiredService<IBrandService>();
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Brands ===");
                Console.WriteLine("1. List all");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Edit");
                Console.WriteLine("4. Delete");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1": ShowAllBrands(services); break;
                    case "2": AddBrand(services); break;
                    case "3": EditBrand(services); break;
                    case "4": DeleteBrand(services); break;
                    case "0": back = true; break;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            } 
        }
    }

    private static void ShowAllBrands(IBrandService services) 
    {
        Console.Clear();
        Console.WriteLine("======== List of Brands ========");
        ShowBrands(services);
        Console.ReadKey();
    }

    private static void ShowBrands(IBrandService services)
    {
        var resultBrand = services.GetAll();
        if (resultBrand.IsFailure)
        {
            ShowErrors(resultBrand.Errors);
            return;
        }
        var brands = resultBrand.Value;
        foreach (var brand in brands!)
        {
            Console.WriteLine($"Id: {brand.BrandId,2} || Name: {brand.Name,-10} || Active: {(brand.Active ? "Yes" : "No")}");
            Console.WriteLine("==============================================================");
        }
    }

    private static void AddBrand(IBrandService services) 
    { 
        Console.WriteLine("======== Brand Creation ========");
        var brandDto = new BrandCreateDto();
        Console.Write("Name: ");
        brandDto.Name = Console.ReadLine()!;
        brandDto.Active = true;
        var result = services.Add(brandDto);
        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Brand added successfully!");
        }
        Console.ReadKey(); 
    }
    private static void EditBrand(IBrandService services)
    {
        Console.Clear();
        Console.WriteLine("======== Update a Brand ========");
        ShowBrands(services);
        Console.Write("Select an ID to update: ");

        var brandId = int.Parse(Console.ReadLine()!);

        var brandResult = services.GetForUpdate(brandId);
        if (brandResult.IsFailure)
        {
            ShowErrors(brandResult.Errors);
            return;
        }

        var brandToUpdate = brandResult.Value;
        Console.WriteLine($"Brand to Update: {brandToUpdate!.Name} | Active: {brandToUpdate.Active}");

        // Name
        Console.Write("New Name (ENTER to keep the same): ");
        var inputName = Console.ReadLine();
        var newName = !string.IsNullOrWhiteSpace(inputName)
            ? inputName
            : brandToUpdate.Name;

        // Active
        Console.Write("Active (y/n, ENTER to keep the same): ");
        var inputActive = Console.ReadLine();
        var newActive = !string.IsNullOrWhiteSpace(inputActive)
            ? inputActive.ToLower() == "y"
            : brandToUpdate.Active;

        // Confirm
        Console.Write("Confirm the changes? (y/n): ");
        var response = Console.ReadLine();

        if (response!.ToLower() == "y")
        {
            brandToUpdate.Name = newName;
            brandToUpdate.Active = newActive;

            var result = services.Update(brandToUpdate);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Brand successfully updated!");
            }
        }
        else
        {
            Console.WriteLine("Cancelled by user.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    private static void DeleteBrand(IBrandService services) 
    {
        Console.Clear();
        Console.WriteLine("======== Delete a Brand ========");
        Console.WriteLine("List of Available Brands");
        ShowBrands(services);
        Console.Write("Select an ID to delete:");
        var brandId = int.Parse(Console.ReadLine()!);

        var brandResult = services.GetById(brandId);
        if (brandResult.IsFailure)
        {
            ShowErrors(brandResult.Errors);
            return;
        }
        var brandToDelete = brandResult.Value;
        Console.Write($"Are you sure to delete {brandToDelete!.Name} (y/n)?");
        var response = Console.ReadLine();
        if (response!.ToLower() == "y")
        {
            var result = services.Delete(brandToDelete.BrandId);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Brand successfully deleted!!!");

            }

        }
        else
        {
            Console.WriteLine("Cancelled by user!!!");
        }
        Console.WriteLine("Key to continue");
        Console.ReadKey();
    }

    // ─── GENRES ───────────────────────────────────────────
    private static void GenresMenu()
    {
        using (var scoped=provider.CreateScope())
        {
            var service = scoped.ServiceProvider.GetRequiredService<IGenreService>();
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Genres ===");
                Console.WriteLine("1. List all");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Edit");
                Console.WriteLine("4. Delete");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1": ShowAllGenres(service); break;
                    case "2": AddGenre(service); break;
                    case "3": EditGenre(service); break;
                    case "4": DeleteGenre(service); break;
                    case "0": back = true; break;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            } 
        }
    }

    private static void ShowAllGenres(IGenreService service) 
    {
        Console.Clear();
        var resultGenre = service.GetAll();
        if (resultGenre.IsFailure)
        {
            ShowErrors(resultGenre.Errors);
            return;
        }
        Console.WriteLine("======= List of Genres =======");
        ShowGenres(service);

        Console.ReadKey();
    }

    private static void ShowGenres(IGenreService service)
    {
        var genres = service.GetAll().Value;
        foreach (var genre in genres!)
        {
            Console.WriteLine($"Id: {genre.GenreId,2} || Name: {genre.GenreName,-10} || Active: {(genre.Active ? "Yes" : "No")}");

            Console.WriteLine("==============================================================");
        }
    }

    private static void AddGenre(IGenreService service)
    {
        Console.WriteLine("======== Genre Creation ========");
        var genreDto = new GenreCreateDto();
        Console.Write("Name: ");
        genreDto.GenreName = Console.ReadLine()!;
        genreDto.Active = true;
        var result = service.Add(genreDto);
        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Genre added successfully!");
        }
        Console.ReadKey();
    }
    private static void EditGenre(IGenreService service) 
    {
        Console.Clear();
        Console.WriteLine("--------Update a Genre--------");
        ShowGenres(service);
        Console.Write("Select an ID to update: ");

        var genreId = int.Parse(Console.ReadLine()!);

        var genreResult = service.GetForUpdate(genreId);
        if (genreResult.IsFailure)
        {
            ShowErrors(genreResult.Errors);
            return;
        }

        var genreToUpdate = genreResult.Value;
        Console.WriteLine($"Genre to Update: {genreToUpdate!.GenreName} | Active: {genreToUpdate.Active}");

        // Name
        Console.Write("New Name (ENTER to keep the same): ");
        var inputName = Console.ReadLine();
        var newName = !string.IsNullOrWhiteSpace(inputName)
            ? inputName
            : genreToUpdate.GenreName;

        // Active
        Console.Write("Active (y/n, ENTER to keep the same): ");
        var inputActive = Console.ReadLine();
        var newActive = !string.IsNullOrWhiteSpace(inputActive)
            ? inputActive.ToLower() == "y"
            : genreToUpdate.Active;

        // Confirm
        Console.Write("Confirm the changes? (y/n): ");
        var response = Console.ReadLine();

        if (response!.ToLower() == "y")
        {
            genreToUpdate.GenreName = newName;
            genreToUpdate.Active = newActive;

            var result = service.Update(genreToUpdate);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Genre successfully updated!");
            }
        }
        else
        {
            Console.WriteLine("Cancelled by user.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    private static void DeleteGenre(IGenreService service) 
    {
        Console.Clear();
        Console.WriteLine("======== Delete a Genre ========");
        Console.WriteLine("List of Available Genres");
        ShowGenres(service);
        Console.Write("Select an ID to delete:");
        var genreId = int.Parse(Console.ReadLine()!);

        var genreResult = service.GetById(genreId);
        if (genreResult.IsFailure)
        {
            ShowErrors(genreResult.Errors);
            return;
        }
        var genreToDelete = genreResult.Value;
        Console.Write($"Are you sure to delete {genreToDelete!.GenreName} (y/n)?");
        var response = Console.ReadLine();
        if (response!.ToLower() == "y")
        {
            var result = service.Delete(genreToDelete.GenreId);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Genre successfully deleted!!!");

            }

        }
        else
        {
            Console.WriteLine("Cancelled by user!!!");
        }
        Console.WriteLine("Key to continue");

        Console.ReadKey();
    }

    // ─── SIZES ────────────────────────────────────────────
    private static void SizesMenu()
    {
        using (var scoped = provider.CreateScope())
        {
            var service=scoped.ServiceProvider.GetRequiredService<ISizeService>();
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Sizes ===");
                Console.WriteLine("1. List all");
                Console.WriteLine("2. Edit");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1": 
                        ShowAllSizes(service);
                        break;
                    case "2": 
                        EditSize(service); 
                        break;
                    case "0": back = true; break;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            } 
        }
    }

    private static void ShowAllSizes(ISizeService service) 
    {
        Console.Clear();
        Console.WriteLine("======== List of Sizes ========");
        ShowSizes(service);

        Console.ReadKey(); 
    }

    private static void ShowSizes(ISizeService service)
    {
        var resultSize = service.GetAllSizes();
        if (resultSize.IsFailure)
        {
            ShowErrors(resultSize.Errors);
            return;
        }
        var sizes = resultSize.Value;
        foreach (var size in sizes!)
        {
            Console.WriteLine($"{size.SizeId,2} || {size.Number,-10}");

            Console.WriteLine("==============================================================");
        }
    }

    private static void ShowErrors(List<string> errors)
    {
        foreach (var error in errors)
        {
            Console.WriteLine(error);
        }
        Console.ReadKey();
    }

    private static void EditSize(ISizeService service) 
    {
        Console.Clear();
        Console.WriteLine("======== Size Update ========");
        Console.WriteLine("List of Available Sizes");
        ShowSizes(service);
        Console.Write("Select an ID to update: ");
        var sizeId=int.Parse(Console.ReadLine()!);

        var sizeResult = service.GetForUpdate(sizeId);
        if (sizeResult.IsFailure)
        {
            ShowErrors(sizeResult.Errors);
            return;
        }
        var sizeToUpdate = sizeResult.Value;
        Console.Write("Number (current: {0}): ", sizeToUpdate!.Number);
        var input = Console.ReadLine();
        if (decimal.TryParse(input, out decimal size))
        {
            sizeToUpdate.Number = size;
        }
        var result = service.Update(sizeToUpdate);
        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Size Updated successfully!!!");
        }

        Console.ReadKey();
    }

    // ─── SPORTS ───────────────────────────────────────────
    private static void SportsMenu()
    {
        using (var scoped=provider.CreateScope())
        {
            var service = scoped.ServiceProvider.GetRequiredService<ISportService>();
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Sports ===");
                Console.WriteLine("1. List all");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Edit");
                Console.WriteLine("4. Delete");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1": ShowAllSports(service); break;
                    case "2": AddSport(service); break;
                    case "3": EditSport(service); break;
                    case "4": DeleteSport(service); break;
                    case "0": back = true; break;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            } 
        }
    }
    private static void ShowAllSports(ISportService service)
    {
        Console.Clear();
        Console.WriteLine("======= List of Sports =======");
        ShowSports(service);
        Console.ReadKey();
    }
    private static void ShowSports(ISportService service)
    {
        var result = service.GetAll();
        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }
        var sports = service.GetAll().Value;
        foreach (var sport in sports!)
        {
            Console.WriteLine($"Id: {sport.SportId,2} || Name: {sport.SportName,-10} || Active: {(sport.Active ? "Yes" : "No")}");
            Console.WriteLine("==============================================================");
        }
    }
    private static void AddSport(ISportService service) 
    {
        Console.WriteLine("======== Sport Creation ========");
        var sportDto = new SportCreateDto();
        Console.Write("Name: ");
        sportDto.SportName = Console.ReadLine()!;
        sportDto.Active = true;
        var result = service.Add(sportDto);
        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Sport added successfully!");
        }
        Console.ReadKey();
    }
    private static void EditSport(ISportService service)
    {
        Console.Clear();
        Console.WriteLine("======= Update a Sport =======");
        ShowSports(service);
        Console.Write("Select an ID to update: ");

        var sportId = int.Parse(Console.ReadLine()!);

        var sportResult = service.GetForUpdate(sportId);
        if (sportResult.IsFailure)
        {
            ShowErrors(sportResult.Errors);
            return;
        }

        var sportToUpdate = sportResult.Value;
        Console.WriteLine($"Genre to Update: {sportToUpdate!.SportName} | Active: {sportToUpdate.Active}");

        // Name
        Console.Write("New Name (ENTER to keep the same): ");
        var inputName = Console.ReadLine();
        var newName = !string.IsNullOrWhiteSpace(inputName)
            ? inputName
            : sportToUpdate.SportName;

        // Active
        Console.Write("Active (y/n, ENTER to keep the same): ");
        var inputActive = Console.ReadLine();
        var newActive = !string.IsNullOrWhiteSpace(inputActive)
            ? inputActive.ToLower() == "y"
            : sportToUpdate.Active;

        // Confirm
        Console.Write("Confirm the changes? (y/n): ");
        var response = Console.ReadLine();

        if (response!.ToLower() == "y")
        {
            sportToUpdate.SportName = newName;
            sportToUpdate.Active = newActive;

            var result = service.Update(sportToUpdate);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Sport successfully updated!");
            }
        }
        else
        {
            Console.WriteLine("Cancelled by user.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
    private static void DeleteSport(ISportService service)
    {
        Console.Clear();
        Console.WriteLine("======== Delete a Sport ========");
        Console.WriteLine("List of Available Sports");
        ShowSports(service);
        Console.Write("Select an ID to delete:");
        var sportId = int.Parse(Console.ReadLine()!);

        var sportResult = service.GetById(sportId);
        if (sportResult.IsFailure)
        {
            ShowErrors(sportResult.Errors);
            return;
        }
        var sportToDelete = sportResult.Value;
        Console.Write($"Are you sure to delete {sportToDelete!.SportName} (y/n)?");
        var response = Console.ReadLine();
        if (response!.ToLower() == "y")
        {
            var result = service.Delete(sportToDelete.SportId);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Sport successfully deleted!!!");

            }

        }
        else
        {
            Console.WriteLine("Cancelled by user!!!");
        }
        Console.WriteLine("Key to continue");

        Console.ReadKey();
    }

    // ─── SPORT SHOES ──────────────────────────────────────
    private static void SportShoesMenu()
    {
        using (var scoped=provider.CreateScope())
        {
            var serviceShoe = scoped.ServiceProvider.GetRequiredService<ISportShoeService>();
            var serviceBrand = scoped.ServiceProvider.GetRequiredService<IBrandService>();
            var serviceGenre = scoped.ServiceProvider.GetRequiredService<IGenreService>();
            var serviceSport = scoped.ServiceProvider.GetRequiredService<ISportService>();
            var serviceSize = scoped.ServiceProvider.GetRequiredService<ISizeService>();
            
            bool back = false;
            while (!back)
            {
                Console.Clear();
                Console.WriteLine("=== Sport Shoes ===");
                Console.WriteLine("1. List all");
                Console.WriteLine("2. Add");
                Console.WriteLine("3. Edit");
                Console.WriteLine("4. Delete");
                Console.WriteLine("0. Back");
                Console.Write("Select an option: ");

                switch (Console.ReadLine())
                {
                    case "1": ShowAllSportShoes(serviceShoe); break;
                    case "2": AddSportShoe(serviceShoe, serviceBrand, serviceGenre, serviceSport, serviceSize); break;
                    case "3": EditSportShoe(serviceShoe, serviceBrand, serviceGenre, serviceSport, serviceSize); break;
                    case "4": DeleteSportShoe(serviceShoe); break;
                    case "0": back = true; break;
                    default:
                        Console.WriteLine("Invalid option. Press any key...");
                        Console.ReadKey();
                        break;
                }
            } 
        }
    }

    private static void ShowAllSportShoes(ISportShoeService serviceShoe) 
    {
        Console.Clear();
        Console.WriteLine("======= List of Shoes =======");
        ShowShoes(serviceShoe);
        Console.ReadKey();
    }

    private static void ShowShoes(ISportShoeService serviceShoe)
    {
        var result = serviceShoe.GetAll();
        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
            return;
        }
        var shoes = serviceShoe.GetAll().Value;
        foreach (var shoe in shoes!)
        {
            Console.WriteLine($"Id: {shoe.ShoeId,2} || Model: {shoe.Model,-10} || Price: {shoe.Price,10:C} || Active: {(shoe.Active ? "Yes" : "No")} || Brand: {shoe.BrandName,-5}");
            Console.WriteLine("=============================================================================");
        }
    }

    private static void AddSportShoe(ISportShoeService serviceShoe, IBrandService serviceBrand, IGenreService serviceGenre, ISportService serviceSport, ISizeService serviceSize) 
    {
        Console.Clear();
        Console.WriteLine("======= Add New Shoe =======");

        var dto = new ShoesCreateDto();

        Console.Write("Model: ");
        dto.Model = Console.ReadLine() ?? "";

        // 🔥 Mostrar Genres
        Console.WriteLine("\nAvailable Genres:");
        ShowGenres(serviceGenre);

        Console.Write("Select Genre ID: ");
        if (!int.TryParse(Console.ReadLine(), out int genreId))
        {
            Console.WriteLine("Invalid Genre ID");
            Console.ReadLine();
            return;
        }
        dto.GenreId = genreId;

        // 🔥 Mostrar Sport
        Console.WriteLine("\nAvailable Sports:");
        ShowSports(serviceSport);

        Console.Write("Select Sport ID: ");
        if (!int.TryParse(Console.ReadLine(), out int sportId))
        {
            Console.WriteLine("Invalid Sport ID");
            Console.ReadLine();
            return;
        }
        dto.SportId = sportId;

        // 🔥 Mostrar Brands
        Console.WriteLine("\nAvailable Brands:");
        ShowBrands(serviceBrand);

        Console.Write("Select Brand ID: ");
        if (!int.TryParse(Console.ReadLine(), out int brandId))
        {
            Console.WriteLine("Invalid Brand ID");
            Console.ReadLine();
            return;
        }
        dto.BrandId = brandId;

        // 🔥 Mostrar Sizes
        Console.WriteLine("\nAvailable Sizes:");
        ShowSizes(serviceSize);

        Console.Write("Select Size ID: ");
        if (!int.TryParse(Console.ReadLine(), out int sizeId))
        {
            Console.WriteLine("Invalid Size ID");
            Console.ReadLine();
            return;
        }
        dto.SizeId = sizeId;

        Console.Write("Price: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal price))
        {
            dto.Price = price;
        }

        Console.Write("Description: ");
        var description = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(description))
        {
            dto.Description = description;
        }
        dto.Active = true;

        // 🔥 LLAMADA AL SERVICE
        var result = serviceShoe.Add(dto);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Shoe added successfully!!!");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
    private static void EditSportShoe(ISportShoeService serviceShoe, IBrandService serviceBrand, IGenreService serviceGenre, ISportService serviceSport, ISizeService serviceSize) 
    {
        Console.Clear();
        Console.WriteLine("======= Update Shoe =======");

        ShowShoes(serviceShoe);

        Console.Write("Select Shoe ID: ");
        if (!int.TryParse(Console.ReadLine(), out int shoeId))
        {
            Console.WriteLine("Invalid ID");
            Console.ReadLine();
            return;
        }

        var shoeResult = serviceShoe.GetForUpdate(shoeId);

        if (shoeResult.IsFailure)
        {
            ShowErrors(shoeResult.Errors);
            return;
        }
        var shoe = shoeResult.Value;
        // 🔹 Model
        Console.Write($"Model ({shoe!.Model}): ");
        var input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
            shoe.Model = input;

        // 🔥 Brand
        Console.WriteLine("\nBrands:");
        ShowBrands(serviceBrand);

        Console.Write($"BrandId ({shoe.BrandId}): ");
        input = Console.ReadLine();
        if (int.TryParse(input, out int brandId))
            shoe.BrandId = brandId;

        // 🔥 Genre
        Console.WriteLine("\nGenres:");
        ShowGenres(serviceGenre);

        Console.Write($"GenreId ({shoe.GenreId}): ");
        input = Console.ReadLine();
        if (int.TryParse(input, out int genreId))
            shoe.GenreId = genreId;

        // 🔹 Sport
        Console.WriteLine("\nSports:");
        ShowSports(serviceSport);

        Console.Write($"SportId ({shoe.SportId}): ");
        input = Console.ReadLine();
        if (int.TryParse(input, out int sportId))
            shoe.SportId = sportId;

        // 🔹 Size
        Console.WriteLine("\nSizes:");
        ShowSizes(serviceSize);

        Console.Write($"SizeId ({shoe.SizeId}): ");
        input = Console.ReadLine();
        if (int.TryParse(input, out int sizeId))
            shoe.SizeId = sizeId;


        // 🔹 Precio
        Console.Write($"Price ({shoe.Price}): ");
        input = Console.ReadLine();
        if (decimal.TryParse(input, out decimal price))
            shoe.Price = price;

        // 🔹 Description
        Console.Write($"Description ({shoe.Description}): ");
        input = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(input))
            shoe.Description = input;

        // 🔹 Activo
        Console.Write($"Is Active ({shoe.Active}): ");
        input = Console.ReadLine();
        if (bool.TryParse(input, out bool isActive))
            shoe.Active = isActive;

        // 🔥 Llamada al service
        var result = serviceShoe.Update(shoe);

        if (result.IsFailure)
        {
            ShowErrors(result.Errors);
        }
        else
        {
            Console.WriteLine("Shoe updated successfully!");
        }

    }
    private static void DeleteSportShoe(ISportShoeService serviceShoe) 
    {
        Console.Clear();
        Console.WriteLine("======== Delete a Shoe ========");
        Console.WriteLine("List of Available Shoes");
        ShowShoes(serviceShoe);
        Console.Write("Select an ID to delete:");
        var shoeId = int.Parse(Console.ReadLine()!);

        var shoeResult = serviceShoe.GetById(shoeId);
        if (shoeResult.IsFailure)
        {
            ShowErrors(shoeResult.Errors);
            return;
        }
        var shoeToDelete = shoeResult.Value;
        Console.Write($"Are you sure to delete {shoeToDelete!.Model} (y/n)?");
        var response = Console.ReadLine();
        if (response!.ToLower() == "y")
        {
            var result = serviceShoe.Delete(shoeToDelete.ShoeId);
            if (result.IsFailure)
            {
                ShowErrors(result.Errors);
            }
            else
            {
                Console.WriteLine("Shoe successfully deleted!!!");
            }
        }
        else
        {
            Console.WriteLine("Cancelled by user!!!");
        }
        Console.WriteLine("Key to continue");

        Console.ReadKey();
    }
}