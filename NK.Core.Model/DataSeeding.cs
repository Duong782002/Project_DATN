using Microsoft.Extensions.Configuration;
using NK.Core.Business.Utilities;
using NK.Core.Model.Entities;
using NK.Core.Model.Enums;
namespace NK.Core.Model
{
    public partial class DataSeeding
    {

        public static void DevelopmentSeed(AppDbContext dbContext, IConfiguration config)
        {
            if (!dbContext.Materials.Any())
            {
                dbContext.Materials.AddRange(GenerateMaterial());
                dbContext.SaveChanges();
            }

            if (!dbContext.Brands.Any())
            {
                dbContext.Brands.AddRange(GenerateBrand());
                dbContext.SaveChanges();
            }

            if (!dbContext.Soles.Any())
            {
                dbContext.Soles.AddRange(GenerateSole());
                dbContext.SaveChanges();
            }

            if (!dbContext.Categories.Any())
            {
                dbContext.Categories.AddRange(GenerateCategory());
                dbContext.SaveChanges();
            }

            if (!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(GenerateProduct());
                dbContext.SaveChanges();
            }

            if (!dbContext.Sizes.Any())
            {
                dbContext.Sizes.AddRange(GenerateSize());
                dbContext.SaveChanges();
            }

            if (!dbContext.Stocks.Any())
            {
                dbContext.Stocks.AddRange(GenerateStock(dbContext));
                dbContext.SaveChanges();
            }

            if (!dbContext.AppUsers.Any())
            {
                dbContext.AppUsers.AddRange(GenerateAppUser());
                dbContext.SaveChanges();
            }

            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.AddRange(GenerateOrder(dbContext));
                dbContext.SaveChanges();
            }
        }

        private static List<Product> products = new();
        private static List<Size> sizes = new();
        private static List<AppUser> users = new();
        private static List<Order> orders = new();
        private static List<Stock> stocks = new();

        private static List<Material> GenerateMaterial()
        {
            return new List<Material>()
            {
                new Material()
                {
                    Id = "1",
                    Name = "Da",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Material()
                {
                    Id = "2",
                    Name = "Da lộn",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Material()
                {
                    Id = "3",
                    Name = "Da bóng",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Material()
                {
                    Id = "4",
                    Name = "Lưới",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Material()
                {
                    Id = "5",
                    Name = "Cao su",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                }
            };
        }

        private static List<Brand> GenerateBrand()
        {
            return new List<Brand>()
            {
                new Brand()
                {
                    Id = "1",
                    Name = "Sneaker Sportswear",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Brand()
                {
                    Id = "2",
                    Name = "Jordan",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Brand()
                {
                    Id = "3",
                    Name = "Sneaker By You",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Brand()
                {
                    Id = "4",
                    Name = "Sneaker Lab",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Brand()
                {
                    Id = "5",
                    Name = "ACG",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Brand()
                {
                    Id = "6",
                    Name = "Sneaker Pro",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                }
            };
        }

        private static List<Sole> GenerateSole()
        {
            return new List<Sole>()
            {
                new Sole()
                {
                    Id = "1",
                    Name = "Giày cho nam",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Sole()
                {
                    Id = "2",
                    Name = "Giày cho nữ",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                },
                new Sole()
                {
                    Id = "3",
                    Name = "Giày cho trẻ em",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                }
            };
        }

        private static List<Category> GenerateCategory()
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = "1",
                    Name = "Giày thể thao",
                    Description = "Được thiết kế để mang lại sự thoải mái và hỗ trợ cho các hoạt động thể chất. Chúng có đế mềm, đàn hồi và thường được làm từ chất liệu nhẹ, thoáng khí.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Category()
                {
                    Id = "2",
                    Name = "Giày da",
                    Description = "Thường được làm từ da thật hoặc da tổng hợp, mang lại vẻ ngoài sang trọng và lịch lãm. Chúng phù hợp với các dịp trang trọng và công sở.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Category()
                {
                    Id = "3",
                    Name = "Giày boot",
                    Description = "Thường có cổ cao và được thiết kế để bảo vệ chân, đặc biệt là trong các điều kiện thời tiết xấu hoặc môi trường khắc nghiệt. Chúng có nhiều kiểu dáng từ cổ ngắn đến cổ cao.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Category()
                {
                    Id = "4",
                    Name = "Giày dép mùa hè",
                    Description = "Thường có thiết kế thoáng mát, giúp chân thoải mái trong những ngày nóng bức. Chúng thường có quai dây hoặc đế bằng.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
                new Category()
                {
                    Id = "5",
                    Name = "Giày công sở",
                    Description = "Thường có thiết kế trang nhã, phù hợp với môi trường làm việc. Chúng mang lại sự thoải mái cho người mang trong suốt cả ngày dài.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now
                },
            };
        }

        private static List<Product> GenerateProduct()
        {
            var imagePathsMan = Directory.GetFiles("G:\\doantotnghiep2024\\doantotnghiep2024\\NikeWorld\\product\\man", "*.jpg")
                          .Concat(Directory.GetFiles("G:\\doantotnghiep2024\\doantotnghiep2024\\NikeWorld\\product\\man", "*.png"))
                          .ToArray();
            var imagePathsWoman = Directory.GetFiles("G:\\doantotnghiep2024\\doantotnghiep2024\\NikeWorld\\product\\woman", "*.jpg")
                          .Concat(Directory.GetFiles("G:\\doantotnghiep2024\\doantotnghiep2024\\NikeWorld\\product\\woman", "*.png"))
                          .ToArray();
            var imagePathsKid = Directory.GetFiles("G:\\doantotnghiep2024\\doantotnghiep2024\\NikeWorld\\product\\kid", "*.jpg")
                          .Concat(Directory.GetFiles("G:\\doantotnghiep2024\\doantotnghiep2024\\NikeWorld\\product\\kid", "*.png"))
                          .ToArray();

            List<string> brandIds = new List<string>() { "1", "2", "3", "4", "5", "6"};
            List<string> materialIds = new List<string>() { "1", "2", "3", "4", "5"};

            Random random = new Random();

            List<ProductInfo> productInfos = new List<ProductInfo>()
            {
                new ProductInfo()
                {
                    Id = "61e86b7b-be56-4619-911a-953ffa6ff30f",
                    Name = "Sneaker Air Force 1 '07",
                    Description = "Maecenas leo odio, condimentum id, luctus nec, molestie sed, justo. Pellentesque viverra pede ac diam. Cras pellentesque volutpat dui.\n\nMaecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti.\n\nNullam porttitor lacus at turpis. Donec posuere metus vitae ipsum. Aliquam non mauris.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[0]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "a206b3ae-4494-4c5f-9974-4933942d0859",
                    Name = "Sneaker Air Force 1 '07 EasyOn",
                    Description = "Suspendisse potenti. In eleifend quam a odio. In hac habitasse platea dictumst.\n\nMaecenas ut massa quis augue luctus tincidunt. Nulla mollis molestie lorem. Quisque ut erat.\n\nCurabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[0]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "0bcb98a6-f541-4635-9a97-7140187e9993",
                    Name = "Sneaker Air Max 1",
                    Description = "Mauris enim leo, rhoncus sed, vestibulum sit amet, cursus id, turpis. Integer aliquet, massa id lobortis convallis, tortor risus dapibus augue, vel accumsan tellus nisi eu orci. Mauris lacinia sapien quis libero.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[0]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "ce11a301-1b83-48e4-a585-beda12fe16aa",
                    Name = "Sneaker Metcon ",
                    Description = "In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.\n\nMaecenas leo odio, condimentum id, luctus nec, molestie sed, justo. Pellentesque viverra pede ac diam. Cras pellentesque volutpat dui.\n\nMaecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[1]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "29702f38-18c1-4b78-811f-f4b114402dc2",
                    Name = "Sneaker InfinityRN 4",
                    Description = "Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[1]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "a9db51d9-a2b1-4c9b-b7dc-5778c4e94e57",
                    Name = "Sneaker InfinityRN 4 SE",
                    Description = "Fusce consequat. Nulla nisl. Nunc nisl.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[1]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "02cf1997-137d-400b-b176-6954e45f8221",
                    Name = "Sneaker Air Max 1 '86 OG G",
                    Description = "Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[2]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "0a072e67-0f5c-48a0-a9a1-41d0245b8882",
                    Name = "Sneaker Air Max 1 '86 OG G",
                    Description = "Etiam vel augue. Vestibulum rutrum rutrum neque. Aenean auctor gravida sem.\n\nPraesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[2]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "3916fe77-c829-4788-8cf2-8d4bdf93a2d1",
                    Name = "Sneaker GT Cut 2 EP",
                    Description =  "Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[2]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "537f8fa9-ed99-47bd-a5cc-e31bddd707c3",
                    Name = "Sneaker Metcon 9 AMP",
                    Description =  "Praesent blandit. Nam nulla. Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede.\n\nMorbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[3]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "803ae6ce-f240-4980-ae20-25f090eb2895",
                    Name = "Sneaker Metcon 9 EasyOn",
                    Description =  "Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[3]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "d7ff9638-2e6c-4e90-badc-c9c5eea72437",
                    Name = "Sneaker Metcon 9 EasyOn",
                    Description =  "Fusce consequat. Nulla nisl. Nunc nisl.\n\nDuis bibendum, felis sed interdum venenatis, turpis enim blandit mi, in porttitor pede justo eu massa. Donec dapibus. Duis at velit eu est congue elementum.\n\nIn hac habitasse platea dictumst. Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[3]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "870cf581-bca0-492c-90f1-c49d935a3a34",
                    Name = "Sneaker Air Max Plus III",
                    Description =  "In hac habitasse platea dictumst. Etiam faucibus cursus urna. Ut tellus.\n\nNulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.\n\nCras non velit nec nisi vulputate nonummy. Maecenas tincidunt lacus at velit. Vivamus vel nulla eget eros elementum pellentesque.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[4]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "4087015d-d1b5-47b0-b87f-0f59bfa29d44",
                    Name = "Air Jordan 1 Low",
                    Description =  "Quisque porta volutpat erat. Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla. Nunc purus.\n\nPhasellus in felis. Donec semper sapien a libero. Nam dui.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[4]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "e68608be-041a-42fe-a67d-bc4685fadf82",
                    Name = "Sneaker Dunk Low Retro1",
                    Description =  "Nullam sit amet turpis elementum ligula vehicula consequat. Morbi a ipsum. Integer a nibh.\n\nIn quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[4]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "f463ca0a-8083-4ed1-9bab-cdd00ca87405",
                    Name = "Air Jordan 1 Low FlyEase",
                    Description =  "Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.\n\nProin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.\n\nDuis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[5]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "a9704b23-28b1-4f19-b7eb-4794c99ddbbf",
                    Name = "Giannis Immortality 3 EP",
                    Description =  "Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.\n\nProin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.\n\nDuis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[5]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "fbf3deef-1b79-468a-9d33-4ec3652f52ad",
                    Name = "Jordan One Take 4 PF",
                    Description =  "Proin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[5]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "9da2ea86-9035-4168-b3dd-b3a480362111",
                    Name = "Sneaker Air More Uptempo '96",
                    Description =  "Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.\n\nProin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.\n\nDuis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[6]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "c4f6be9b-b178-4a6e-852a-500f8dc0c68a",
                    Name = "Sneaker Rival Fly 3",
                    Description =  "Maecenas tristique, est et tempus semper, est quam pharetra magna, ac consequat metus sapien ut nunc. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Mauris viverra diam vitae quam. Suspendisse potenti.\n\nNullam porttitor lacus at turpis. Donec posuere metus vitae ipsum. Aliquam non mauris.\n\nMorbi non lectus. Aliquam sit amet diam in magna bibendum imperdiet. Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[6]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "9fe52c30-5772-4bf6-afde-a1bbc55748bb",
                    Name = "Sneaker Dunk Low By You",
                    Description =  "Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio.\n\nCras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim. Lorem ipsum dolor sit amet, consectetuer adipiscing elit.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[6]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "3d078ad6-8f39-4a00-8717-99e6bee10a1c",
                    Name = "Sneaker Go FlyEase",
                    Description =  "Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio.\n\nCras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim. Lorem ipsum dolor sit amet, consectetuer adipiscing elit.\n\nProin interdum mauris non ligula pellentesque ultrices. Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[7]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "97141803-b9bb-4572-9d12-33ff66c86d7b",
                    Name = "Sneaker Downshifter 12",
                    Description =  "Vestibulum quam sapien, varius ut, blandit non, interdum in, ante. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Duis faucibus accumsan odio. Curabitur convallis.\n\nDuis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.",
                    CreatedDate = DateTime.Now,
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[7]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "2c065b04-5b02-40b9-993d-deef4e8e4191",
                    Name = "Sneaker Dunk Low Retro Premium",
                    Description =  "Praesent blandit. Nam nulla. Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede.\n\nMorbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[7]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "bdd45608-d835-4135-a75c-85454b47d1d2",
                    Name = "Sneaker Air Max 90",
                    Description =  "Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.\n\nProin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.\n\nDuis aliquam convallis nunc. Proin at turpis a pede posuere nonummy. Integer non velit.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[8]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "63f1c567-59a7-44e5-adf1-cdb2d5326a4a",
                    Name = "Sneaker Air Max 97",
                    Description =  "Phasellus in felis. Donec semper sapien a libero. Nam dui.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[8]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "18fada3c-0a5f-4cbf-b6e8-6c5b7ebf4218",
                    Name = "Air Jordan 1 Low SE",
                    Description =  "Quisque porta volutpat erat. Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla. Nunc purus.\n\nPhasellus in felis. Donec semper sapien a libero. Nam dui.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[8]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "b052f219-222b-4eb5-8db6-0bd068eacd6e",
                    Name = "Jordan Post",
                    Description =  "Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[9]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "5eb47d8d-0537-45ab-a26e-d42e27fc0c12",
                    Name = "KD Trey 5 X EP",
                    Description =  "Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.\n\nFusce consequat. Nulla nisl. Nunc nisl.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[9]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "33fa3a68-5575-451e-9a69-15c47a74238a",
                    Name = "Sneaker Air Max 901",
                    Description =  "Curabitur in libero ut massa volutpat convallis. Morbi odio odio, elementum eu, interdum eu, tincidunt in, leo. Maecenas pulvinar lobortis est.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[9]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "51541565-41ae-4289-87d9-7246e03176e6",
                    Name = "Sneaker Air Max 90 G NRG",
                    Description =  "Cras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim. Lorem ipsum dolor sit amet, consectetuer adipiscing elit.\n\nProin interdum mauris non ligula pellentesque ultrices. Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl.\n\nAenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[10]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "68385ae7-10de-45c3-a091-ee6f32ba0cf3",
                    Name = "Sneaker Blazer Low '77 SE",
                    Description =  "Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.\n\nPraesent blandit. Nam nulla. Integer pede justo, lacinia eget, tincidunt eget, tempus vel, pede.\n\nMorbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[10]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "45feb8df-9921-4f9b-9d61-e97403b43c9b",
                    Name = "Sneaker Air Force 1 '07 LV8 EMB",
                    Description =  "In sagittis dui vel nisl. Duis ac nibh. Fusce lacus purus, aliquet at, feugiat non, pretium quis, lectus.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[10]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "1b766633-5799-4009-bcfa-d9d973f60c11",
                    Name = "Jordan Stadium 90",
                    Description =  "Curabitur gravida nisi at nibh. In hac habitasse platea dictumst. Aliquam augue quam, sollicitudin vitae, consectetuer eget, rutrum at, lorem.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[11]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "1f9848fe-a184-45c9-8845-cd15cd8f0e10",
                    Name = "Sneaker Air Max LTD 3",
                    Description =  "Sed sagittis. Nam congue, risus semper porta volutpat, quam pede lobortis ligula, sit amet eleifend pede libero quis orci. Nullam molestie nibh in lectus.\n\nPellentesque at nulla. Suspendisse potenti. Cras in purus eu magna vulputate luctus.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[11]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "cec17bc3-9226-4014-ab8e-15b3959943df",
                    Name = "Sneaker Invincible 3",
                    Description =  "Nulla ut erat id mauris vulputate elementum. Nullam varius. Nulla facilisi.\n\nCras non velit nec nisi vulputate nonummy. Maecenas tincidunt lacus at velit. Vivamus vel nulla eget eros elementum pellentesque.\n\nQuisque porta volutpat erat. Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla. Nunc purus.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[11]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "a9a173a1-374e-4c8a-9823-3916ada65586",
                    Name = "Sneaker Air Max 90 GG NRG",
                    Description =  "Duis bibendum. Morbi non quam nec dui luctus rutrum. Nulla tellus.\n\nIn sagittis dui vel nisl. Duis ac nibh. Fusce lacus purus, aliquet at, feugiat non, pretium quis, lectus.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[12]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "f034204f-349e-4286-9536-d71049edcb7d",
                    Name = "Sneaker Air Max 90 G",
                    Description =  "Phasellus sit amet erat. Nulla tempus. Vivamus in felis eu sapien cursus vestibulum.\n\nProin eu mi. Nulla ac enim. In tempor, turpis nec euismod scelerisque, quam turpis adipiscing lorem, vitae mattis nibh ligula nec sem.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[12]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "a16a7fe8-0f45-4eae-b969-4c319f2b1de2",
                    Name = "Sneaker Blazer Low '77 Jumbo",
                    Description =  "Quisque id justo sit amet sapien dignissim vestibulum. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Nulla dapibus dolor vel est. Donec odio justo, sollicitudin ut, suscipit a, feugiat et, eros.\n\nVestibulum ac est lacinia nisi venenatis tristique. Fusce congue, diam id ornare imperdiet, sapien urna pretium nisl, ut volutpat sapien arcu sed augue. Aliquam erat volutpat.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[12]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "bed0f167-29b1-445f-b341-6f57c32274f6",
                    Name = "Sneaker Air Force 1 High By You",
                    Description =  "Quisque porta volutpat erat. Quisque erat eros, viverra eget, congue eget, semper rutrum, nulla. Nunc purus.\n\nPhasellus in felis. Donec semper sapien a libero. Nam dui.\n\nProin leo odio, porttitor id, consequat in, consequat ut, nulla. Sed accumsan felis. Ut at dolor quis odio consequat varius.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[12]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "e76f3310-c1ee-43bd-a0c7-a72349ab96e0",
                    Name = "Sneaker Phantom GX Club",
                    Description =  "In quis justo. Maecenas rhoncus aliquam lacus. Morbi quis tortor id nulla ultrices aliquet.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[12]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "fcee3918-b833-4801-9fe7-cf8d134959cd",
                    Name = "Sneaker Dunk High Retro",
                    Description =  "Sed ante. Vivamus tortor. Duis mattis egestas metus.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[12]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "b6f06d00-4e5a-4a08-a67b-bac05743ede9",
                    Name = "Sneaker Vaporfly ",
                    Description =  "Proin interdum mauris non ligula pellentesque ultrices. Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl.\n\nAenean lectus. Pellentesque eget nunc. Donec quis orci eget orci vehicula condimentum.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[13]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "a732f901-804e-4e9c-8500-d3b8226e783c",
                    Name = "Sneaker Pegasus 40",
                    Description =  "Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio.\n\nCras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim. Lorem ipsum dolor sit amet, consectetuer adipiscing elit.\n\nProin interdum mauris non ligula pellentesque ultrices. Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl.",
                    ModifiedDate = DateTime.Now,
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[13]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "01be9181-ea3b-4d05-8921-481d6c3943ed",
                    Name = "Sneaker Quest 5",
                    Description =  "Praesent id massa id nisl venenatis lacinia. Aenean sit amet justo. Morbi ut odio.\n\nCras mi pede, malesuada in, imperdiet et, commodo vulputate, justo. In blandit ultrices enim. Lorem ipsum dolor sit amet, consectetuer adipiscing elit.\n\nProin interdum mauris non ligula pellentesque ultrices. Phasellus id sapien in sapien iaculis congue. Vivamus metus arcu, adipiscing molestie, hendrerit at, vulputate vitae, nisl.",
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[13]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "d2f090ae-1a9b-4f29-8d7b-a166fa66f20b",
                    Name = "Air Jordan 1 Zoom CMFT 2",
                    Description =  "Sed ante. Vivamus tortor. Duis mattis egestas metus.\n\nAenean fermentum. Donec ut mauris eget massa tempor convallis. Nulla neque libero, convallis eget, eleifend luctus, ultricies eu, nibh.",
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[14]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "206aa56d-e2e1-4368-bcc4-098a54ddd6b5",
                    Name = "Sneaker Air Pegasus '89",
                    Description =  "Integer tincidunt ante vel ipsum. Praesent blandit lacinia erat. Vestibulum sed magna at nunc commodo placerat.",
                    Status = Status.ACTIVE,
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[14]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                },
                new ProductInfo()
                {
                    Id = "11566c27-b2fe-4164-9577-c37b9e055905",
                    Name = "LeBron XX EP",
                    Description =  "Nam ultrices, libero non mattis pulvinar, nulla pede ullamcorper augue, a suscipit nulla elit ac nulla. Sed vel enim sit amet nunc viverra dapibus. Nulla suscipit ligula in lacus.",
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[14]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "5"
                },
                new ProductInfo()
                {
                    Id = "760fb364-e594-4019-b1f2-f7d557f41bbb",
                    Name = "LeBron XX EP",
                    Description =  "Pellentesque at nulla. Suspendisse potenti. Cras in purus eu magna vulputate luctus.\n\nCum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vivamus vestibulum sagittis sapien. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.\n\nEtiam vel augue. Vestibulum rutrum rutrum neque. Aenean auctor gravida sem.",
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[15]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "1"
                },
                new ProductInfo()
                {
                    Id = "b37d2c49-6106-4fd8-a5aa-61fd7d2557c3",
                    Name = "Jordan Why Not .6 PF",
                    Description =  "In hac habitasse platea dictumst. Morbi vestibulum, velit id pretium iaculis, diam erat fermentum justo, nec condimentum neque sapien placerat ante. Nulla justo.\n\nAliquam quis turpis eget elit sodales scelerisque. Mauris sit amet eros. Suspendisse accumsan tortor quis turpis.",
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsWoman[15]),
                    SoleId = "2",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "2"
                },
                new ProductInfo()
                {
                    Id = "9b3dbb06-2ffa-42cc-b06b-81fe70208f54",
                    Name = "Sneaker Pegasus FlyEase SE",
                    Description =  "Duis consequat dui nec nisi volutpat eleifend. Donec ut dolor. Morbi vel lectus in quam fringilla rhoncus.",
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsKid[15]),
                    SoleId = "3",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "3"
                },
                new ProductInfo()
                {
                    Id = "4439dce2-b65d-46ca-92d2-19da51aa5eef",
                    Name = "Zion 2 PF",
                    Description = "Morbi porttitor lorem id ligula. Suspendisse ornare consequat lectus. In est risus, auctor sed, tristique in, tempus sit amet, sem.",
                    RetailPrice = random.Next(3000000, 5000001),
                    TaxRate = random.Next(5, 20),
                    DiscountType = (DiscountType)Enum.GetValues(typeof(DiscountType)).GetValue(random.Next(Enum.GetValues(typeof(DiscountType)).Length)),
                    weather = (Weather)Enum.GetValues(typeof(Weather)).GetValue(random.Next(Enum.GetValues(typeof(Weather)).Length)),
                    ProductImage = ConvertImageToByteArray(imagePathsMan[16]),
                    SoleId = "1",
                    MaterialId = materialIds[random.Next(materialIds.Count)],
                    BrandId = brandIds[random.Next(brandIds.Count)],
                    CategoryId = "4"
                }
            };

            foreach(var item in productInfos)
            {
                Product product = new Product()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Status = item.Status,
                    CreatedDate = GetRandomDate(new DateTime(2024,1,1), DateTime.Now),
                    ModifiedDate = item.ModifiedDate,
                    ProductImage = item.ProductImage,
                    RetailPrice = item.RetailPrice,
                    DiscountType = item.DiscountType,
                    DiscountRate = item.DiscountType == DiscountType.PERCEN ? (decimal)(item.RetailPrice - item.RetailPrice * (item.TaxRate / 100)) : item.RetailPrice,
                    TaxRate = item.DiscountType == DiscountType.PERCEN ?  item.TaxRate : 0,
                    weather = item.weather,
                    SoleId = item.SoleId,
                    MaterialId = item.MaterialId,
                    CategoryId = item.CategoryId,
                    BrandId = item.BrandId
                };

                products.Add(product);
            }

            return products;
        }

        private static List<Size> GenerateSize()
        {
            for(int i = 24; i <= 27; i++)
            {
                sizes.Add(new Size { Id = Guid.NewGuid().ToString(), NumberSize = i });
            }

            for(int i= 35; i<= 45; i++)
            {
                sizes.Add(new Size { Id = Guid.NewGuid().ToString(), NumberSize = i });
            }

            return sizes.OrderBy(s => s.NumberSize).ToList();
        }

        private static List<Stock> GenerateStock(AppDbContext dbContext)
        {
            Random random = new Random();

            products = dbContext.Products.ToList();
            sizes = dbContext.Sizes.ToList();
            Dictionary<string, int> productStockMap = new Dictionary<string, int>();

            foreach (var product in products)
            {
                List<Size> availabelSizes = product.SoleId == "3" ? sizes.Where(p => p.NumberSize >= 24 && p.NumberSize <= 27).ToList() : sizes.Where(p => p.NumberSize >= 35 && p.NumberSize <= 45).ToList();

                int numberOfSizes = random.Next(2, 4);
                List<Size> selectedSizes = availabelSizes.OrderBy(p => random.Next()).Take(numberOfSizes).ToList();

                foreach(var size in selectedSizes)
                {
                    int unitInStock = random.Next(5, 10);
                    stocks.Add(new Stock()
                    {
                        ProductId = product.Id,
                        SizeId = size.Id,
                        UnitInStock = unitInStock
                    });

                    // Cập nhật tổng UnitInStock của sản phẩm
                    if (productStockMap.ContainsKey(product.Id))
                        productStockMap[product.Id] += unitInStock;
                    else
                        productStockMap.Add(product.Id, unitInStock);
                }
            }

            // Gán giá trị FirstQuantity cho mỗi sản phẩm
            foreach (var product in products)
            {
                if (productStockMap.ContainsKey(product.Id))
                    product.FirstQuantity = productStockMap[product.Id];
                else
                    product.FirstQuantity = 0; // Nếu không có UnitInStock nào, gán giá trị mặc định là 0
            }

            // Lưu thay đổi vào database
            dbContext.SaveChanges();

            return stocks;
        }

        private static List<AppUser> GenerateAppUser()
        {
            string password = StringUtil2.CreateMD5("07082002")[..20];
            Random random = new Random();

            for (char c = 'A'; c <= 'O'; c++)
            {
                users.Add(new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = $"van{c.ToString().ToLower()}123",
                    UserName = $"Nguyễn Văn {c}",
                    Password = password,
                    Email = $"van{c}@gmail.com",
                    PhoneNumber = $"09{random.Next(10000000, 100000000)}",
                    Gender = Gender.NAM,
                    IsFirst = random.Next(2) == 0,
                    Status = Status.STOP,
                    Roles = Role.Customer,
                    ModifiedDate = GetRandomDate(new DateTime(2023,12,1), new DateTime(2024,5, 1))
                });
            }

            for (char c = 'A'; c <= 'I'; c++)
            {
                string phonenumber = $"09{random.Next(10000000, 100000000)}";
                users.Add(new AppUser()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = phonenumber,
                    UserName = $"Nguyễn Văn {c}",
                    Password = password,
                    Email = $"nguyenvan{c}@gmail.com",
                    PhoneNumber = phonenumber,
                    IsFirst = false,
                    Gender = Gender.NAM,
                    Status = Status.ACTIVE,
                    Roles = Role.Employee,
                    ModifiedDate = GetRandomDate(new DateTime(1999, 1, 1), new DateTime(2004, 1, 1))
                });
            }

            users.Add(new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserId = "duong",
                UserName = "Lê Minh Dương",
                Password = password,
                Email = "minhduong@gmail.com",
                PhoneNumber = "0987654321",
                Gender = Gender.NAM,
                Status = Status.ACTIVE,
                Roles = Role.Admin,
                ModifiedDate = new DateTime(2002,10,16)
            });

            return users;
        }

        private static List<Order> GenerateOrder(AppDbContext dbContext)
        {
            Random random = new();

            var users = dbContext.AppUsers.ToList();
            var products = dbContext.Products.ToList();
            var sizes = dbContext.Sizes.ToList();
            var stocks = dbContext.Stocks.ToList();

            foreach(var user in users)
            {
                int numberOfOrders = 0;
                if(user.Roles == Role.Customer)
                {
                    numberOfOrders = random.Next(2, 4);
                }
                else if(user.Roles == Role.Employee)
                {
                    numberOfOrders = random.Next(10, 21);
                }

                for(int i = 0; i < numberOfOrders; i++)
                {
                    string randomChar = ((char)random.Next('A', 'Z' + 1)).ToString();
                    decimal totalAmount = 0;

                    List<OrderItem> orderItems = new List<OrderItem>();
                    int numItems = random.Next(1, 3);
                    for(int j = 0; j < numItems; j++)
                    {
                        Product randomProduct = products[random.Next(products.Count - 1)];
                        string sizeId = sizes[random.Next(sizes.Count)].Id;

                        decimal unitPrice = randomProduct.DiscountRate;
                        decimal quantity = random.Next(1, 2);
                        totalAmount += unitPrice * quantity;

                        orderItems.Add(new OrderItem()
                        {
                            ProductId = randomProduct.Id,
                            SizeId = sizeId,
                            Quantity = quantity,
                            UnitPrice = unitPrice,
                        });
                    }

                    List<OrderStatus> orderStatuses = new List<OrderStatus>();
                    if(user.Roles == Role.Employee)
                    {
                        orderStatuses = new List<OrderStatus>() {
                            new OrderStatus
                            {
                                Id = Guid.NewGuid().ToString(),
                                OrderId = string.Empty,
                                Status = StatusOrder.CONFIRM,
                                Time = DateTime.Now,
                                Note = "Chờ xác nhận"
                            },
                            new OrderStatus
                            {
                                Id = Guid.NewGuid().ToString(),
                                OrderId = string.Empty,
                                Status = StatusOrder.DELIVERIED,
                                Time = DateTime.Now,
                                Note = "Đơn hàng đã giao"
                            }};
                    }
                    else
                    {
                        int maxStatusIndex = random.Next(1,4);
                        DateTime statusTime = DateTime.Now.AddMonths(-1 * maxStatusIndex);
                        for (int k = 1; k <= maxStatusIndex; k++)
                        {
                            StatusOrder status = (StatusOrder)Enum.GetValues(typeof(StatusOrder)).GetValue(k);
                            orderStatuses.Add(new OrderStatus
                            {
                                Id = Guid.NewGuid().ToString(),
                                OrderId = string.Empty,
                                Status = status,
                                Time = statusTime.AddDays(k * 10),
                                Note = GetRandomStatus(status)
                            });
                        }
                    }

                    var order = new Order()
                    {
                        Id = Guid.NewGuid().ToString(),
                        AddressName = "Trường Đại học Công Nghiệp Hà Nội, phường Minh Khai, quận Bắc Từ Liêm, Hà Nội",
                        CustomerName = $"Nguyễn Văn {randomChar}",
                        PhoneNumber = $"09{random.Next(1000000, 10000000)}",
                        Note = "",
                        TotalAmount = totalAmount,
                        Payment = user.Roles == Role.Employee ? PaymentMethod.OTHER : random.Next(0, 2) == 0 ? PaymentMethod.ON : PaymentMethod.OFF,
                        CurrentStatus = user.Roles == Role.Employee ? StatusOrder.DELIVERIED : orderStatuses.Last().Status,
                        UserId = user.Id,
                        DateCreated = user.Roles == Role.Customer ? GetRandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 27)) : GetRandomDate(new DateTime(2024, 1, 1), new DateTime(2024, 5, 27)),
                        OrderItems = orderItems,
                        OrderStatuses = orderStatuses
                    };

                    foreach(var item in orderItems)
                    {
                        item.OrderId = order.Id;
                        item.Order = order;
                    }

                    foreach (var status in orderStatuses)
                    {
                        status.OrderId = order.Id;
                        status.Order = order;
                    }

                    if (order.CurrentStatus == StatusOrder.DELIVERIED)
                    {
                        foreach (var item in orderItems.ToList())
                        {
                            item.OrderId = order.Id;
                            item.Order = order;

                            var stockItem = stocks.FirstOrDefault(p => p.ProductId ==  item.ProductId);
                            if(stockItem != null)
                            {
                                if(stockItem.UnitInStock >= item.Quantity)
                                {
                                    stockItem.UnitInStock -= (int)item.Quantity;
                                }
                                else
                                {
                                    orderItems.Remove(item);
                                }
                            }
                        }

                        totalAmount = orderItems.Sum(x => x.Quantity * x.UnitPrice);
                        order.TotalAmount = totalAmount;
                    }

                    if (order.TotalAmount > 0)
                    {
                        orders.Add(order);
                    }
                }
            }

            return orders;
        }

        class ProductInfo
        {
            public string Id { get; set; } = string.Empty;
            public DateTime? ModifiedDate { get; set; }
            public string? Name { get; set; }
            public Status Status { get; set; } = Status.ACTIVE;
            public DateTime CreatedDate { get; set; } = DateTime.Now;
            public decimal RetailPrice { get; set; }
            public string? Description { get; set; }
            public decimal? TaxRate { get; set; }
            public DiscountType DiscountType { get; set; }
            public Weather weather { get; set; }
            public byte[]? ProductImage { get; set; }
            public string SoleId { get; set; } = string.Empty;
            public string MaterialId { get; set; } = string.Empty;
            public string CategoryId { get; set; } = string.Empty;
            public string BrandId { get; set; } = string.Empty;
        }
        public static byte[]? ConvertImageToByteArray(string imagePath)
        {
            if (File.Exists(imagePath))
            {
                return File.ReadAllBytes(imagePath);
            }
            return null;
        }

        public static DateTime GetRandomDate(DateTime start, DateTime end)
        {
            Random random = new Random();
            int range = (end - start).Days;
            return start.AddDays(random.Next(range + 1));
        }

        public static string GetRandomStatus(StatusOrder status)
        {
            var result = "";
            switch (status)
            {

                case StatusOrder.CONFIRM:
                    result = "Chờ xác nhận";
                    break;
                case StatusOrder.PENDING_SHIP:
                    result = "Đang chuản bị hàng và giao đến kho Bắc Từ Liêm";
                    break;
                case StatusOrder.SHIPPING:
                    result = "Đơn hàng đang được giao đến địa chỉ gốc";
                    break;
                case StatusOrder.DELIVERIED:
                    result = "Đơn hàng được giao thành công!";
                    break;
                case StatusOrder.CANCELED:
                    result = "Đơn hàng đã bị hủy";
                    break;
            }
            return result;
        }
    }
}
