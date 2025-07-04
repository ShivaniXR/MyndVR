using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using REST_API_HANDLER;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.IO;


public class KalaAi : MonoBehaviour
{
    // References to UI elements in the Unity Editor
	public GameObject loadingpanel;
	public TMP_InputField inputText;
	public TMP_Text resultText;
	public List<GameObject> previewObjs;

	// API endpoint for image generation
	private string IMAGE_GENERTION_API_URL = "https://api.openai.com/v1/images/generations";

	// This method is called when the "Search" button is clicked in the Unity UI
	public void SearchButtonClicked()
    {
        // Clear previous results and show loading panel
		resultText.text = "";
		resultText.enabled = false;
		loadingpanel.SetActive(true);

        // Clear textures from preview objects
		for (int i = 0; i < previewObjs.Count; i++)
		{
			previewObjs[i].GetComponent<Renderer>().material.mainTexture = null;
		}

        // Get user input from the TextMeshPro input field
		string description = inputText.text;
       // if (description.Length <= 160)
        //{
          //  description = description.Substring(0, 160);
        //}
		string resolution = "256x256"; // Possible Resolution: 256x256, 512x512, or 1024x1024.

        // Call the method to generate and load images based on user input
		GenerateImage(description, resolution, () => {
			loadingpanel.SetActive(false); // Hide loading panel when complete
		});
		
	}

    // This method sends a request to the OpenAI API to generate images
	public void GenerateImage(string description, string resolution, Action completionAction)
	{
        // Create a request model with input parameters
		GenerateImageRequestModel reqModel = new GenerateImageRequestModel(description, 1, resolution);

        // Make a POST request to the OpenAI API
		ApiCall.instance.PostRequest<GenerateImageResponseModel>(IMAGE_GENERTION_API_URL, reqModel.ToCustomHeader(), null, reqModel.ToBody(), (result =>
		{
			loadTexture(result.data, completionAction); // Load generated images
			resultText.enabled = true; // Show response text
		}), (error =>
		{
			ErrorResponseModel entity = JsonUtility.FromJson<ErrorResponseModel>(error);
			completionAction.Invoke(); // Invoke completion action
			resultText.enabled = true; // Show error response text
			resultText.text = entity.error.message;
		}));
	}

    // This method loads the generated textures and displays them on preview objects
	async void loadTexture(List<UrlClass> urls, Action completionAction)
    {
		for (int i = 0; i < urls.Count; i++)
        {
			Texture2D _texture = await GetRemoteTexture(urls[i].url); // Get remote texture
			previewObjs[i].GetComponent<Renderer>().material.mainTexture = _texture; // Apply texture to object
			Utility.WriteImageOnDisk(_texture, System.DateTime.Now.Millisecond + "_createImg_" + i + "_.jpg"); // Write image to disk
		}

		completionAction.Invoke(); // Invoke completion action
	}

    // This method fetches a remote texture using Unity's UnityWebRequestTexture
	public static async Task<Texture2D> GetRemoteTexture(string url)
	{
		using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
		{
			var asyncOp = www.SendWebRequest();

			while (asyncOp.isDone == false)
				await Task.Delay(1000 / 30); // Wait for texture to load

			// Read results:
			if (www.isNetworkError || www.isHttpError)
			{
				return null; // Return null for errors
			}
			else
			{
				return DownloadHandlerTexture.GetContent(www); // Return downloaded texture
			}
		}
	}

    // This method writes a texture to disk as a PNG file
	private void WriteImageOnDisk(Texture2D texture, string fileName)
	{
		byte[] textureBytes = texture.EncodeToPNG(); // Encode texture as PNG bytes
		string path = Application.persistentDataPath + fileName; // Set file path
		File.WriteAllBytes(path, textureBytes); // Write bytes to file
		Debug.Log("File Written On Disk! "  + path ); // Log file write success
	}
}
