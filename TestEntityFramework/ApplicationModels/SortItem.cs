using System.Collections;
using System.Linq.Expressions;

namespace TestEntityFramework.ApplicationModels;

public class SortItem
{
    public string PropertyName { get; set; }
    public SortDirection Direction { get; set; }
}