using Core.Results.Bases;

namespace Core.Results
{
    /// <summary>
    /// TR: Genelde servis sınıflarında çeşitli methodlardan başarılı olarak dönecek IsSuccessful 
    /// özelliği false olacak Result soyut sınıfından miras alan işlem sonucu sınıfı.
    /// EN: Generally in service classes to return an error result from several methods, inherited 
    /// abstract Result class will have the value of IsSuccessful property false.
    /// </summary>
    public class ErrorResult : Result
    {
        public ErrorResult(string message) : base(false, message)
        {
        }

        public ErrorResult() : base(false, "")
        {
        }
    }
}
