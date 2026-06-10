using MongoDB.Driver;

public class MongoDBConexion
{
    public static IMongoDatabase GetDB()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        return client.GetDatabase("ecocervantes");
    }
}