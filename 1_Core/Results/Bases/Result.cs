namespace Core.Results.Bases
{
    /// <summary>
    /// TR: Genelde servis sınıflarında çeşitli methodlardan sonuç olarak dönmek ve başarılı ile başarısız 
    /// işlem sonuçları için kullanılacak soyut sınıf.
    /// EN: Generally in service classes the results of several methods will be returned as successful or error
    /// results through the classes inherited from this abstract class.
    /// </summary>
    public abstract class Result
    {
        public bool IsSuccessful { get; }

        public string? Message { get; set; }

        public Result(bool isSuccessful, string message)
        {
            IsSuccessful = isSuccessful;
            Message = message;
        }
    }
}
