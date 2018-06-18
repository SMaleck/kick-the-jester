using Assets.Source.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using UniRx;

namespace Assets.Source.App.Persistence.Models
{    
    public abstract class AbstractPersistentModel
    {
        [JsonIgnore]
        public NotifyEventHandler OnAnyPropertyChanged = delegate { };

        
        public AbstractPersistentModel()
        {
            SetupOnAnyPropertyChanged();
        }

        
        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            SetupOnAnyPropertyChanged();
        }


        // TODO: Make this generic enough for all properties of type ReactiveProperty<T>
        private void SetupOnAnyPropertyChanged()
        {
            // Get All Reactive Properties in this class
            IEnumerable<FieldInfo> reactiveInfos = this.GetType()
                                                       .GetFields()
                                                       .Where(e => e.FieldType.Equals(typeof(IntReactiveProperty)));

            // Fire event when any property changes
            foreach (var ri in reactiveInfos)
            {
                var react = ri.GetValue(this) as IntReactiveProperty;
                react.Subscribe(_ => OnAnyPropertyChanged());
            }
        }
    }
}
