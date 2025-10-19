namespace UnApIFood.Utils

{
    public static class ConfigUtil
    {
        public static string? ConnectionString  { get; set; } = null;
        public static string? ApiKey { get; set; } = "provea un ApiKey >:C";
        public static string? JWTAudience { get; set; }  = "provea un JWT >:C";
        public static string? JWTIssuer { get; set; } = "provea un JWT >:C";
        public static string? JWTKey { get; set; } = "provea un JWT >:C";
    }
    
    
}