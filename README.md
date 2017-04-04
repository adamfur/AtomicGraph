# AtomicGraph

```csharp
public class Shop
{
	public int Orders { get; set; }
	public decimal TotalSum { get; set; }
	
	public void Checkout(Basket basket)
	{
		++Orders
		TotalSum += basket.Sum();
	}
}
```

## Scenario 1
**A proper Basket item is submitted to Checkout**
Orders is increased by one, and TotalSum is increased by the Sum of the order
*Consistency is preserved*

## Scenarion 2
**A null object is submitted to Checkout**
Order is increased by one, and code throws NullRferenceEXception.
*Shop is now corrupt*
