using Core.Results.Bases;

namespace Core.Results
{
    /// <summary>
    /// TR: Genelde servis sınıflarında çeşitli methodlardan başarılı olarak dönecek IsSuccessful 
    /// özelliği true olacak Result soyut sınıfından miras alan işlem sonucu sınıfı.
    /// EN: Generally in service classes to return a successful result from several methods, inherited 
    /// abstract Result class will have the value of IsSuccessful property true.
    /// </summary>
    public class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {
        }

        public SuccessResult() : base(true, "")
        {
        }
    }
}
