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
            Game.Create(Guid.NewGuid(),"Sensible Soccer","Amiga 500 zamanlarında harika bir futbol oyunu", ["Jon Hare", "Chris Yates"], 9.95M, string.Empty, [Genre.Sport]),
            Game.Create(Guid.NewGuid(),"The Secret of Monkey Island","Guybrush Threepwood ile macera dolu bir hikaye", ["Ron Gilbert", "Tim Schafer"], 19.99M, string.Empty, [Genre.Adventure]),
            Game.Create(Guid.NewGuid(),"Doom","FPS türünün efsanelerinden", ["John Carmack", "John Romero"], 29.99M, string.Empty, [Genre.Fps]),
            Game.Create(Guid.NewGuid(),"Super Mario World","Nintendo'nun ikonik platform oyunu", ["Shigeru Miyamoto"], 39.99M, string.Empty, [Genre.Platformer]),
            Game.Create(Guid.NewGuid(),"Street Fighter II","Dövüş oyunları tarihini değiştiren yapım", ["Yoshiki Okamoto"], 24.99M, string.Empty, [Genre.Fighting]),
            Game.Create(Guid.NewGuid(),"Tetris","Basit ama bağımlılık yapan puzzle oyunu", ["Alexey Pajitnov"], 9.99M, string.Empty, [Genre.Puzzle]),
            Game.Create(Guid.NewGuid(),"Final Fantasy VII","RPG dünyasında devrim yaratan oyun", ["Hironobu Sakaguchi", "Yoshinori Kitase"], 49.99M, string.Empty, [Genre.Rpg]),
            Game.Create(Guid.NewGuid(),"Command & Conquer","RTS türünün öncülerinden", ["Brett Sperry", "Joseph Bostic"], 29.95M, string.Empty, [Genre.Strategy]),
            Game.Create(Guid.NewGuid(),"Half-Life","FPS türünde bir dönüm noktası", ["Gabe Newell", "Marc Laidlaw"], 39.99M, string.Empty, [Genre.Fps]),
            Game.Create(Guid.NewGuid(),"Tomb Raider","Lara Croft ile aksiyon dolu macera", ["Toby Gard"], 34.99M, string.Empty, [Genre.Adventure, Genre.Action])
        };


        await catalogDbContext.Games.AddRangeAsync(games);
        await catalogDbContext.SaveChangesAsync();
    }
}