using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace TextToSpeach.Models
{
    public class TTSModel
    {
        [Required(ErrorMessage = "Please enter some text")]
        [StringLength(500, ErrorMessage = "Text cannot exceed 500 characters")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Please select a voice")]
        public string SelectedVoice { get; set; }

        public List<SelectListItem> Voices { get; set; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "uk-UA-PolinaNeural", Text = "uk-UA-PolinaNeural (Female)"},
            new SelectListItem { Value = "uk-UA-OstapNeural", Text = "uk-UA-OstapNeural (Male)"}
        };
    }
}
