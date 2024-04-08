using System.Collections.Generic;
using OrderCloud.SDK;

namespace Customer.OrderCloud.Common.Models;

public class MyProduct : Product<MyProductXp>
{
    public MyProduct()
    {
        xp = new MyProductXp();
    }
}

public class MyProductXp
{
    public string Status { get; set; }
    public string Brand { get; set; }
    public string UnitOfMeasure { get; set; }
    public string CCID { get; set; }
    public List<Image> Images { get; set; }
    public string Currency { get; set; }
    public string ProductType { get; set; }
    public string ProductUrl { get; set; }
}

public class Image
{
    public string ThumbnailUrl { get; set; }
    public string Url { get; set; }
}