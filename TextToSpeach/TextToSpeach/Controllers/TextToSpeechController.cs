using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using TextToSpeach.Models;

namespace TextToSpeach.Controllers
{
    public class TextToSpeechController : Controller
    {
        private readonly string subscriptionKey;
        private readonly string region;

        public TextToSpeechController(IConfiguration configuration)
        {
            subscriptionKey = configuration["AzureSpeech:SubscriptionKey"];
            region = configuration["AzureSpeech:Region"];
        }

        public IActionResult Index()
        {
            var model = new TTSModel();
            return View(model);
        }

        [Route("TextToSpeech/GenerateSpeech")]
        [HttpPost]
        public async Task<IActionResult> GenerateSpeech(TTSModel model)
        {
            if (string.IsNullOrEmpty(model.Text) || string.IsNullOrEmpty(model.SelectedVoice))
            {
                ModelState.AddModelError("", "Текст і вибір голосу обов'язкові");
                return View("Index", model);
            }

            try
            {
                var config = SpeechConfig.FromSubscription(subscriptionKey, region);
                config.SpeechSynthesisVoiceName = model.SelectedVoice;

                using (var synthesizer = new SpeechSynthesizer(config, null))
                {
                    var result = await synthesizer.SpeakTextAsync(model.Text);

                    if (result.Reason == ResultReason.SynthesizingAudioCompleted)
                    {
                        var audioStream = new MemoryStream(result.AudioData);
                        audioStream.Position = 0;

                        return new FileStreamResult(audioStream, "audio/mpeg");
                    }
                    else
                    {
                        var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                        string errorDetails = cancellation.ErrorDetails;
                        string reason = cancellation.Reason.ToString();

                        ModelState.AddModelError("", "Помилка під час генерації мови");
                        return View("Index", model);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Виникла помилка: {ex.Message}");
                return View("Index", model);
            }
        }
    }
}
