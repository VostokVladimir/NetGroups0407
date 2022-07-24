using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;

public class CatalogManager : MonoBehaviour
{
    private Dictionary<string, CatalogItem> _catalog = new Dictionary<string, CatalogItem>();

    public void GetCatalog()
    {
        var request_catalog = new GetCatalogItemsRequest();
        PlayFabClientAPI.GetCatalogItems(request_catalog, Sucsess, Failure);

            
    }

    private void Sucsess(GetCatalogItemsResult result)
    {
        foreach(var item in result.Catalog)
        {
            _catalog.Add(item.ItemId, item);
            Debug.Log($"ID позиции {item.ItemId}");
            Debug.Log($"Имя позиции {item.DisplayName}");

        }

    }

    private void Failure(PlayFabError error)
        
    {
        Debug.LogError(error.ErrorMessage);
    }

}
