using System;
using UnityEngine;

[System.Serializable]
public class InventorySerializable
{
    public int iron;
    public int cotton;
    public int silk;
    public int wood;
    public int grain;

    public override string ToString()
    {
        string message = "Inventory: \n";
        // Get properties of this object
        foreach (var field in GetType().GetFields())
        {
            message += string.Format("{0}: {1}\n", field.Name, field.GetValue(this));
        }
        return message;
    }
}


[System.Serializable]
public class BasePricesSerializable
{
    public int iron;
    public int cotton;
    public int silk;
    public int wood;
    public int grain;
}



[System.Serializable]
public class CitySerializable
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public string cityName;
    public InventorySerializable inventory;
    public BasePricesSerializable basePrice;
    public BasePricesSerializable prices;

    public override string ToString()
    {
        String message = "City: " + cityName + "\n";

        message += inventory.ToString();
        return message;
    }

    public int GetPrice(string item)
    {
        prices = basePrice;
        return (int)prices.GetType().GetField(item).GetValue(prices);
    }

    public int GetQuantity(string item)
    {
        return (int)inventory.GetType().GetField(item).GetValue(inventory);
    }

    public void ChangeQuantity(string item, int quantity)
    {
        int current_quantity = (int)inventory.GetType().GetField(item).GetValue(inventory);
        current_quantity += quantity;
        inventory.GetType().GetField(item).SetValue(inventory, current_quantity);
    }
}

[System.Serializable]
public class CitiesSerializable
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public CitySerializable[] cities;
}
