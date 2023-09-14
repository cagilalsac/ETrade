namespace MvcWebUI.Settings
{
    public static class AppEnvironment // web uygulama ortamı static class'ı
    {
        public static bool IsDevelopment { get; set; } // web uygulamasının development (geliştirme) veya
                                                       // production (canlı) ortamda çalışma bilgisini tutar,
                                                       // MvcWebUI -> Program.cs altında değeri atanır
    }
}
