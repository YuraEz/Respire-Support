using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Newtonsoft.Json.Linq;
using TMPro;

public class Pollen : MonoBehaviour
{
    // UI ��������
    public Button treeButton; // ������ ��� ������ Tree
    public Button weedsButton; // ������ ��� ������ Weeds
    public Button grassButton; // ������ ��� ������ Grass
    public TMP_Dropdown pollenTypeDropdown; // ���������� ������ ��� ������ ���� ������
    public TMP_InputField pollenLocationInput; // ���� ����� ��� ������
    public TextMeshProUGUI resultText; // ���� ������ �����������

    public List<GameObject> objects;

    // API URL � ����
    private string locationApiUrl = "http://dataservice.accuweather.com/locations/v1/topcities/150";
    private string forecastApiUrl = "http://dataservice.accuweather.com/forecasts/v1/daily/1day/";
    private string apiKey = "d8GyNitatXKGn17GR0wvxHRWI8GcrSZg";

    // �������� �������� ���������� ���� ������
    private string selectedPollenType;

    // ����� ������� �������
    public void GetPollenData()
    {
        resultText.text = "Loading...";
        StartCoroutine(GetLocationKey(pollenLocationInput.text));
    }

    // 1. ��������� Location Key ��� ������
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

        // ������� ������ ��� ��������� locationKey
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

        // ������� � ���������� ����
        StartCoroutine(GetPollenForecast(locationKey));
    }

    // 2. ��������� �������� ������
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

        // ������� ������ � ������
        JObject forecast = JObject.Parse(request.downloadHandler.text);
        JArray airAndPollen = (JArray)forecast["DailyForecasts"][0]["AirAndPollen"];

        // ���������� ������ ��� ������ � ����������� ������
        Dictionary<string, int> pollenData = new Dictionary<string, int>();
        foreach (var pollen in airAndPollen)
        {
            string name = pollen["Name"].ToString();
            if (name == "Tree" || name == "Grass" || name == "Weeds")
            {
                pollenData[name] = (int)pollen["CategoryValue"];
            }
        }

        // ��������� ������������ ��� ������
        treeButton.onClick.RemoveAllListeners();
        weedsButton.onClick.RemoveAllListeners();
        grassButton.onClick.RemoveAllListeners();

        treeButton.onClick.AddListener(() => ShowPollenData("Tree", pollenData));
        weedsButton.onClick.AddListener(() => ShowPollenData("Weeds", pollenData));
        grassButton.onClick.AddListener(() => ShowPollenData("Grass", pollenData));

        // ���������� ����������� ������
        // UpdateDropdownOptions();

        // �������� ������� � Dropdown
        //pollenTypeDropdown.onValueChanged.RemoveAllListeners();
        // pollenTypeDropdown.onValueChanged.AddListener(delegate { ShowPollenFromDropdown(pollenData); });


      //  ShowPollenData("tree", pollenData);
        //resultText.text = "�������� ��� ������ ����� ������ ��� ���������� ������.";
    }

    // 3. ����������� ������ � ������ �� ������
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


        selectedPollenType = pollenType; // ���������� ���������� ����

        // ��������� �������� ������
        int categoryValue = pollenData.ContainsKey(pollenType) ? pollenData[pollenType] : 2;

        // ��������� � �������� ������


        foreach (GameObject obj in objects)
        {
            obj.SetActive(false);
        }

        objects[categoryValue].SetActive(true);

        string description = GetPollenDescription(pollenType, categoryValue);
        resultText.text = description;
    }

    // 4. ���������� Dropdown
    //private void UpdateDropdownOptions()
    //{
    //    pollenTypeDropdown.ClearOptions();
    //    List<string> options = new List<string> { "Tree", "Grass", "Ragweed" };
    //    pollenTypeDropdown.AddOptions(options);
    //}

    // 5. ��������� ������ ����� Dropdown
    // private void ShowPollenFromDropdown(Dictionary<string, int> pollenData)
    // {
    //     string selectedOption = pollenTypeDropdown.options[pollenTypeDropdown.value].text;
    //     ShowPollenData(selectedOption, pollenData);
    // }

    // ��������������� ����� ��� �������������� �������� ������ � ��������
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
