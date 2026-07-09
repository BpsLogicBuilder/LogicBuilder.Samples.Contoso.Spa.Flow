using Contoso.Spa.Flow.ScreenSettings.Json;
using LogicBuilder.App.Spa.Forms.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Contoso.Spa.Flow.ScreenSettings.Views
{
    [JsonConverter(typeof(ScreenSettingsConverter))]
    abstract public class ScreenSettingsBase
    {
        abstract public ViewType ViewType { get; }
        public IEnumerable<CommandButtonDescriptor>? CommandButtons { get; set; }
        public IEnumerable<ValidationResult>? Errors { get; set; }
        public string TypeString => this.GetType().AssemblyQualifiedName ?? throw new ArgumentException($"{this.GetType().Name}: {{75FE4EA4-09BF-40C2-A750-50E46A801147}}");
    }
}
