using UnityEngine;

public class Item
{
    private int id;
    private int stack;

    public int Id {  get { return id; } }
    public int Stack { get { return stack; } }
    public Item(int id)
    {
        this.id = id;
        this.stack = 1;
    }
    public void AddStack()
    {
        stack++;
    }
}
