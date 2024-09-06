namespace RentAGame.Catalog.Data;

public class CatalogDataSeeder(CatalogDbContext catalogDbContext)
    : IDataSeed
{
    // Örnek verileri yükleme işini üstlenen fonksiyon
    public async Task SeedAllAsync()
    {
        // Eğer Context zaten oyun bilgileri içeriyorsa çık
        if (await catalogDbContext.Games.AnyAsync())
            return;

        var games = new List<Game>
        {
            Game.Create(Guid.Parse("2ef0c7fe-1536-47a2-a37c-6dd94b9ef625"),"Sensible Soccer","Amiga 500 zamanlarında harika bir futbol oyunu", ["Jon Hare", "Chris Yates"], 9.95M, string.Empty, [Genre.Sport]),
            Game.Create(Guid.Parse("d14f366d-961e-47e2-bea5-610984127868"),"The Secret of Monkey Island","Guybrush Threepwood ile macera dolu bir hikaye", ["Ron Gilbert", "Tim Schafer"], 19.99M, string.Empty, [Genre.Adventure]),
            Game.Create(Guid.Parse("e15af250-8c0e-45ea-892d-393d5f155c21"),"Doom","FPS türünün efsanelerinden", ["John Carmack", "John Romero"], 29.99M, string.Empty, [Genre.Fps]),
            Game.Create(Guid.Parse("2f181ec3-d6d0-4ba5-86c4-3ee21fb512f2"),"Super Mario World","Nintendo'nun ikonik platform oyunu", ["Shigeru Miyamoto"], 39.99M, string.Empty, [Genre.Platformer]),
            Game.Create(Guid.Parse("3a2cea6c-b096-492f-8583-e32a7c03c3d3"),"Street Fighter II","Dövüş oyunları tarihini değiştiren yapım", ["Yoshiki Okamoto"], 24.99M, string.Empty, [Genre.Fighting]),
            Game.Create(Guid.Parse("80982277-2ce1-4a1e-87f0-789d422fa627"),"Tetris","Basit ama bağımlılık yapan puzzle oyunu", ["Alexey Pajitnov"], 9.99M, string.Empty, [Genre.Puzzle]),
            Game.Create(Guid.Parse("e18a4db7-dc38-4c88-9870-9cc1bba41fa0"),"Final Fantasy VII","RPG dünyasında devrim yaratan oyun", ["Hironobu Sakaguchi", "Yoshinori Kitase"], 49.99M, string.Empty, [Genre.Rpg]),
            Game.Create(Guid.Parse("db5449c1-3af5-43c1-9fa0-ffba6d07ca8f"),"Command & Conquer","RTS türünün öncülerinden", ["Brett Sperry", "Joseph Bostic"], 29.95M, string.Empty, [Genre.Strategy]),
            Game.Create(Guid.Parse("f138239a-5fd3-4dc7-a70c-88008ae89ac3"),"Half-Life","FPS türünde bir dönüm noktası", ["Gabe Newell", "Marc Laidlaw"], 39.99M, string.Empty, [Genre.Fps]),
            Game.Create(Guid.Parse("318ebc70-a936-4c1e-8657-3f87c954e38d"),"Tomb Raider","Lara Croft ile aksiyon dolu macera", ["Toby Gard"], 34.99M, string.Empty, [Genre.Adventure, Genre.Action])
        };


        await catalogDbContext.Games.AddRangeAsync(games);
        await catalogDbContext.SaveChangesAsync();
    }
}