namespace Documentify.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcludeAttribute : Attribute
    {
        public bool Exclude { get; set; }

        public ExcludeAttribute()
        {
            Exclude = true;
        }

        public ExcludeAttribute(bool exclude)
        {
            Exclude = exclude;
        }
    }
}
