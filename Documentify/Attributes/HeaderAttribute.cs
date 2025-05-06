namespace Documentify.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class HeaderAttribute : Attribute
    {
        public string Header { get; set; }

        public HeaderAttribute(string header)
        {
            Header = header;
        }
    }
}
