public class Parts
{
    private string itemid;
    private string partnum;
    private string manufacturer;
    private string description;

    public Parts(string itemid, string partnum, string manufacturer, string description)
    {
        this.itemid = itemid;
        this.partnum = partnum;
        this.manufacturer = manufacturer;
        this.description = description;
    }

    public string ItemId
    {
        get { return itemid; }
        set { itemid = value; }
    }

    public string PartNum
    {
        get { return partnum; }
        set { partnum = value; }
    }
    public string Manufacturer
    {
        get { return manufacturer; }
        set { manufacturer = value; }
    }
    public string Description
    {
        get { return description; }
        set { description = value; }
    }

}
