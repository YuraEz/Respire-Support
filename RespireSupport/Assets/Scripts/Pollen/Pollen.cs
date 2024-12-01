using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using TMPro;

public class Pollen : MonoBehaviour
{
    // UI элементы
    public Button treeButton; // Кнопка для выбора Tree
    public Button weedsButton; // Кнопка для выбора Weeds
    public Button grassButton; // Кнопка для выбора Grass
    public TMP_Dropdown pollenTypeDropdown; // Выпадающий список для выбора типа пыльцы
    public TMP_InputField pollenLocationInput; // Поле ввода для города
    public TextMeshProUGUI resultText; // Поле вывода результатов

    public List<GameObject> objects;

    // API URL и ключ
    private string locationApiUrl = "http://dataservice.accuweather.com/locations/v1/topcities/150";
    private string forecastApiUrl = "http://dataservice.accuweather.com/forecasts/v1/daily/1day/";
    private string apiKey = "d8GyNitatXKGn17GR0wvxHRWI8GcrSZg";

    // Хранение текущего выбранного типа пыльцы
    private string selectedPollenType;

    // Метод запуска запроса
    public void GetPollenData()
    {
        resultText.text = "Loading...";
        StartCoroutine(GetLocationKey(pollenLocationInput.text));
    }

    // 1. Получение Location Key для города
    private IEnumerator GetLocationKey(string cityName)
    {
        string requestUrl = $"{locationApiUrl}?apikey={apiKey}";

        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            resultText.text = "City is not found.";
            yield break;
        }

        // Парсинг ответа для получения locationKey
        JArray locations = JArray.Parse(request.downloadHandler.text);
        string locationKey = null;

        foreach (var location in locations)
        {
            if (location["EnglishName"].ToString().ToLower() == cityName.ToLower())
            {
                locationKey = location["Key"].ToString();
                break;
            }
        }

        if (string.IsNullOrEmpty(locationKey))
        {
            resultText.text = "City is not found.";
            yield break;
        }

        // Переход к следующему шагу
        StartCoroutine(GetPollenForecast(locationKey));
    }

    // 2. Получение прогноза пыльцы
    private IEnumerator GetPollenForecast(string locationKey)
    {
        string requestUrl = $"{forecastApiUrl}{locationKey}?apikey={apiKey}&details=true";

        UnityWebRequest request = UnityWebRequest.Get(requestUrl);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            resultText.text = "City is not found.";
            yield break;
        }

        // Парсинг данных о пыльце
        JObject forecast = JObject.Parse(request.downloadHandler.text);
        JArray airAndPollen = (JArray)forecast["DailyForecasts"][0]["AirAndPollen"];

        // Сохранение данных для кнопок и выпадающего списка
        Dictionary<string, int> pollenData = new Dictionary<string, int>();
        foreach (var pollen in airAndPollen)
        {
            string name = pollen["Name"].ToString();
            if (name == "Tree" || name == "Grass" || name == "Weeds")
            {
                pollenData[name] = (int)pollen["CategoryValue"];
            }
        }

        // Установка обработчиков для кнопок
        treeButton.onClick.RemoveAllListeners();
        weedsButton.onClick.RemoveAllListeners();
        grassButton.onClick.RemoveAllListeners();

        treeButton.onClick.AddListener(() => ShowPollenData("Tree", pollenData));
        weedsButton.onClick.AddListener(() => ShowPollenData("Weeds", pollenData));
        grassButton.onClick.AddListener(() => ShowPollenData("Grass", pollenData));

        // Обновление выпадающего списка
        // UpdateDropdownOptions();

        // Привязка события к Dropdown
        //pollenTypeDropdown.onValueChanged.RemoveAllListeners();
        // pollenTypeDropdown.onValueChanged.AddListener(delegate { ShowPollenFromDropdown(pollenData); });


      //  ShowPollenData("tree", pollenData);
        //resultText.text = "Выберите тип пыльцы через кнопки или выпадающий список.";
    }

    // 3. Отображение данных о пыльце по кнопке
    private void ShowPollenData(string pollenType, Dictionary<string, int> pollenData)
    {
        if (pollenType == "Tree")
        {
            treeButton.transform.GetChild(0).gameObject.SetActive(true);
            weedsButton.transform.GetChild(0).gameObject.SetActive(false);
            grassButton.transform.GetChild(0).gameObject.SetActive(false);
        }
        else if (pollenType == "Grass")
        {
            treeButton.transform.GetChild(0).gameObject.SetActive(false);
            weedsButton.transform.GetChild(0).gameObject.SetActive(false);
            grassButton.transform.GetChild(0).gameObject.SetActive(true);
        }
        else
        {
            treeButton.transform.GetChild(0).gameObject.SetActive(false);
            weedsButton.transform.GetChild(0).gameObject.SetActive(true);
            grassButton.transform.GetChild(0).gameObject.SetActive(false);
        }


        selectedPollenType = pollenType; // Сохранение выбранного типа

        // Получение значения пыльцы
        int categoryValue = pollenData.ContainsKey(pollenType) ? pollenData[pollenType] : 2;

        // Сообщение о значении пыльцы


        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }

        objects[categoryValue].SetActive(true);

        string description = GetPollenDescription(pollenType, categoryValue);
        resultText.text = description;
    }

    // 4. Обновление Dropdown
    //private void UpdateDropdownOptions()
    //{
    //    pollenTypeDropdown.ClearOptions();
    //    List<string> options = new List<string> { "Tree", "Grass", "Ragweed" };
    //    pollenTypeDropdown.AddOptions(options);
    //}

    // 5. Обработка выбора через Dropdown
    // private void ShowPollenFromDropdown(Dictionary<string, int> pollenData)
    // {
    //     string selectedOption = pollenTypeDropdown.options[pollenTypeDropdown.value].text;
    //     ShowPollenData(selectedOption, pollenData);
    // }

    // Вспомогательный метод для преобразования значений пыльцы в описание
    private string GetPollenDescription(string pollenType, int categoryValue)
    {
        return categoryValue switch
        {
            0 => $"There's no pollen here. {pollenType}.",
            1 => $"Pollen levels are very low. {pollenType}",
            2 => $"Pollen levels are low. {pollenType}",
            3 => $"Pollen level is moderate. {pollenType}:",
            4 => $"Pollen level is high. {pollenType}",
            5 => $"Pollen levels are very high. {pollenType}",
            _ => "Unknown"
        };
    }
}
