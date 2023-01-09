public interface IItemListCollection
{
    public string Tag { get; }
    public ListItem[] ListItems { get; }
    public void ApplyList();
}