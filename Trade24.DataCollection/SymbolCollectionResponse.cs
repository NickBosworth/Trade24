namespace Trade24.DataCollection
{
    public class SymbolCollectionResponse
    {
        public bool Success { get; set; }
        public List<string> Symbols { get; set; } = new List<string>();
        public string ErrorMessage { get; set; }
    }
}
