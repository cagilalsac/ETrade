namespace Core.Records.Bases
{
    /// <summary>
    /// TR: İlişki entity'leri dışında tüm entity'lerin ve model'lerin miras alacağı ve veritabanındaki entity'lerin 
    /// karşılığı olan tablolarda sütunları oluşacak özellikleri içeren soyut sınıf.
    /// EN: An abstract class that all entities and models, except for relationship entities, will inherit from, 
    /// and which contains properties to create columns in the corresponding tables for entities in the database.
    /// </summary>
    public abstract class Record
    {
        public int Id { get; set; }
    }
}
