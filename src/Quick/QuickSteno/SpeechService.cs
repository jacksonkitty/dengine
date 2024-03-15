namespace QuickSteno;

using System;
using System.Threading.Tasks;
using Microsoft.CognitiveServices.Speech;

// Leans on https://github.com/Azure-Samples/cognitive-services-speech-sdk/blob/master/samples/csharp/sharedcontent/console/speech_recognition_samples.cs
internal class SpeechService
{
    internal static async Task<string?> CaptureSingleUtterance(string? key, string? region, CancellationToken token)
    {
        var spoken = await RecognitionWithMicrophoneAsync(key, region, token);
        return spoken?.Text;
    }

    // Speech recognition from microphone.
    private static async Task<SpeechRecognitionResult?> RecognitionWithMicrophoneAsync(string? key, string? region, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            return null;
        }

        ArgumentNullException.ThrowIfNull(nameof(key));
        ArgumentNullException.ThrowIfNull(nameof(region));

        // <recognitionWithMicrophone>
        // Creates an instance of a speech config with specified subscription key and service region.
        // Replace with your own subscription key and service region (e.g., "westus").
        // The default language is "en-us".
        var config = SpeechConfig.FromSubscription(key, region);

        // Creates a speech recognizer using microphone as audio input.
        using (var recognizer = new SpeechRecognizer(config))
        {
            // Starts recognizing.
            Console.WriteLine("Say something...");

            // Starts speech recognition, and returns after a single utterance is recognized. The end of a
            // single utterance is determined by listening for silence at the end or until a maximum of 15
            // seconds of audio is processed.  The task returns the recognition text as result.
            // Note: Since RecognizeOnceAsync() returns only a single utterance, it is suitable only for single
            // shot recognition like command or query.
            // For long-running multi-utterance recognition, use StartContinuousRecognitionAsync() instead.
            var result = await recognizer.RecognizeOnceAsync().ConfigureAwait(false);

            // Checks result.
            if (result.Reason == ResultReason.RecognizedSpeech)
            {
                Console.WriteLine($"RECOGNIZED: Text={result.Text}");
                return result;
            }
            else if (result.Reason == ResultReason.NoMatch)
            {
                Console.WriteLine($"NOMATCH: Speech could not be recognized.");
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = CancellationDetails.FromResult(result);
                Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");

                if (cancellation.Reason == CancellationReason.Error)
                {
                    Console.WriteLine($"CANCELED: ErrorCode={cancellation.ErrorCode}");
                    Console.WriteLine($"CANCELED: ErrorDetails={cancellation.ErrorDetails}");
                    Console.WriteLine($"CANCELED: Did you update the subscription info?");
                }
            }

            return null;
        }
        // </recognitionWithMicrophone>
    }
}
