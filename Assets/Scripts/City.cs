using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class City : MonoBehaviour
{
    public TextAsset cityDataJson;
    public String cityName;
    public int city_funds = 1000;
    public CitySerializable citySerializable;

    // Start is called before the first frame update
    void Start()
    {
        // Get text mesh pro component CityName
        TextMeshPro cityNameText = gameObject.GetComponentInChildren<TextMeshPro>();
        cityNameText.text = cityName;

        citySerializable = ReadJsonCity();
        Debug.Log("Price Iron: "+ citySerializable.GetPrice("iron"));
    }

    public void SellItem(string item, int quantity, Boat boat)
    {
        if (boat.money < citySerializable.GetPrice(item) * quantity)
        {
            Debug.Log("Not enough money to sell " + item);
            return;
        }
        if (citySerializable.GetQuantity(item) >= quantity)
        {
            citySerializable.ChangeQuantity(item, -quantity);
            boat.inventory[item] += quantity;
            boat.money -= citySerializable.GetPrice(item) * quantity;
            city_funds += citySerializable.GetPrice(item) * quantity;
        }
        else
        {
            Debug.Log("Not enough " + item + " in inventory");
        }
    }

    public void BuyItem(string item, int quantity, Boat boat)
    {
        if (city_funds < citySerializable.GetPrice(item) * quantity)
        {
            Debug.Log("Not enough money to buy " + item);
            return;
        }
        if (boat.inventory[item] >= quantity)
        {
            citySerializable.ChangeQuantity(item, quantity);
            boat.inventory[item] -= quantity;
            city_funds -= citySerializable.GetPrice(item) * quantity;
            boat.money += citySerializable.GetPrice(item) * quantity;
        }
        else
        {
            Debug.Log("Not enough " + item + " in inventory");
        }
    }


    CitySerializable ReadJsonCity()
    {
        CitiesSerializable citiesInJson = JsonUtility.FromJson<CitiesSerializable>(cityDataJson.text);
 
        foreach (CitySerializable city in citiesInJson.cities)
        {
            Debug.Log("Found city: " + city.cityName);
            Debug.Log(city.ToString());
            if (city.cityName == cityName)
            {
                return city;
            }
        }
        return null;
    }
}
