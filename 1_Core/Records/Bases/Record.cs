namespace Core.Records.Bases
{
    /// <summary>
    /// TR: İlişki entity'leri dışında tüm entity'lerin ve model'lerin miras alacağı ve veritabanındaki entity'lerin 
    /// karşılığı olan tablolarda sütunları oluşacak özellikleri içeren soyut sınıf.
    /// Ayrıca repository ve service sınıflarında sadece kendisinden miras alan tipler kullanılabilecek.
    /// EN: An abstract class that all entities and models, except for relationship entities, will inherit from, 
    /// and which contains properties to create columns in the corresponding tables for entities in the database.
    /// Morever, only types inherited from this class can be used in repository and service classes.
    /// </summary>
    public abstract class Record
    {
        public int Id { get; set; }
    }
}
