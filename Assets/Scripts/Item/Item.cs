using UnityEngine;

public class Item
{
    private int id;
    private int quantity;

    public int Id {  get { return id; } }
    public int Quantity { get { return quantity; } }
    public Item(int id)
    {
        this.id = id;
        this.quantity = 1;
    }
    public void AddStack()
    {
        quantity++;
    }
}
