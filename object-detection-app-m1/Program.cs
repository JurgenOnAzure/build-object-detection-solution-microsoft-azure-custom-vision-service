/*
  This demo application accompanies Pluralsight course 'Build an Object Detection Solution with Microsoft Azure Custom Vision Service', 
  by Jurgen Kevelaers. See https://pluralsight.pxf.io/custom-vision-object-detection.

  MIT License

  Copyright (c) 2021 Jurgen Kevelaers

  Permission is hereby granted, free of charge, to any person obtaining a copy
  of this software and associated documentation files (the "Software"), to deal
  in the Software without restriction, including without limitation the rights
  to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
  copies of the Software, and to permit persons to whom the Software is
  furnished to do so, subject to the following conditions:

  The above copyright notice and this permission notice shall be included in all
  copies or substantial portions of the Software.

  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
  IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
  AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
  LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
  SOFTWARE.
*/

using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace object_detection_app_m1
{
  class Program
  {
    // TODO: set your Custom Vision settings here
    private const string customVisionEndpoint = "https://TODO.api.cognitive.microsoft.com/";
    private static readonly Guid customVisionProjectId = new Guid("TODO");
    private const string customVisionTrainingKey = "TODO";
    private const string customVisionPredictionKey = "TODO";
    private const string customVisionPredictionModelname = "TODO";

    private const string customVisionPearTestImageUrl = "https://raw.githubusercontent.com/JurgenOnAzure/build-object-detection-solution-microsoft-azure-custom-vision-service/main/images-testing/pear-2166102_1280.jpg";
    private const string customVisionBananaTestImageUrl = "https://raw.githubusercontent.com/JurgenOnAzure/build-object-detection-solution-microsoft-azure-custom-vision-service/main/images-testing/banana-316868_1280.jpg";

    private const ConsoleColor predictionConsoleColor = ConsoleColor.Green;
    private const ConsoleColor trainingConsoleColor = ConsoleColor.Yellow;
    private const ConsoleColor errorConsoleColor = ConsoleColor.Red;

    private static readonly ConsoleColor defaultConsoleForegroundColor = Console.ForegroundColor;
    private static readonly object lockObject = new object();

    static async Task Main(string[] args)
    {
      ConsoleWriteLine("*** Press ENTER to start ***");
      Console.ReadLine();

      await DetectImage(
        imageDisplayName: "PEAR",
        imageUrl: customVisionPearTestImageUrl);

      await DetectImage(
        imageDisplayName: "BANANA",
        imageUrl: customVisionBananaTestImageUrl);

      await Train();

      ConsoleWriteLine();
      ConsoleWriteLine("*** Press ENTER to quit ***");
      Console.ReadLine();
    }

    private static async Task DetectImage(string imageDisplayName, string imageUrl)
    {
      try
      {
        ConsoleWriteLine();
        ConsoleWriteLine($"Will start image prediction for {imageDisplayName}...", predictionConsoleColor);

        using var predictionClient = new CustomVisionPredictionClient(
         new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.ApiKeyServiceClientCredentials(customVisionPredictionKey))
        {
          Endpoint = customVisionEndpoint
        };

        var prediction = await predictionClient.DetectImageUrlAsync(
          projectId: customVisionProjectId,
          publishedName: customVisionPredictionModelname,
          imageUrl: new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models.ImageUrl(imageUrl));

        ConsoleWriteLine($"Image prediction results for {imageDisplayName}", predictionConsoleColor);
        ConsoleWriteLine($"{JsonConvert.SerializeObject(prediction, Formatting.Indented)}", predictionConsoleColor);
      }
      catch (Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models.CustomVisionErrorException predictionEx)
      {
        ConsoleWriteLine($"* ERROR * {predictionEx.Response.Content}", errorConsoleColor);
      }
      catch (Exception ex)
      {
        ConsoleWriteLine($"* ERROR * {ex.Message}", errorConsoleColor);
      }
    }

    private static async Task Train()
    {
      try
      {
        ConsoleWriteLine();
        ConsoleWriteLine("Will start new training...", trainingConsoleColor);

        using var trainingClient = new CustomVisionTrainingClient(
         new Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.ApiKeyServiceClientCredentials(customVisionTrainingKey))
        {
          Endpoint = customVisionEndpoint
        };

        var iteration = await trainingClient.TrainProjectAsync(
          projectId: customVisionProjectId,
          trainingType: "Regular",
          forceTrain: true);

        ConsoleWriteLine($"\tTraining started: iteration {iteration.Name}", trainingConsoleColor);
      }
      catch(Microsoft.Azure.CognitiveServices.Vision.CustomVision.Training.Models.CustomVisionErrorException trainingEx)
      {
        ConsoleWriteLine($"* ERROR * {trainingEx.Response.Content}", errorConsoleColor);
      }
      catch (Exception ex)
      {
        ConsoleWriteLine($"* ERROR * {ex.Message}", errorConsoleColor);
      }
    }

    private static void ConsoleWriteLine(string message = null, ConsoleColor? foregroundColor = null)
    {
      lock (lockObject)
      {
        Console.ForegroundColor = foregroundColor ?? defaultConsoleForegroundColor;
        Console.WriteLine(message);
        Console.ForegroundColor = defaultConsoleForegroundColor;
      }
    }

  }
}
