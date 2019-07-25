using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UniRx;

namespace Assets.Source.Services.Savegame.StorageModels
{
    public abstract class AbstractPersistentModel
    {
        [JsonIgnore]
        public ReactiveCommand OnAnyPropertyChanged = new ReactiveCommand();


        protected AbstractPersistentModel()
        {
            SetupOnAnyPropertyChanged();
        }


        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            SetupOnAnyPropertyChanged();
        }


        // ToDo [SAVEGAMES] Make this generic enough for all properties of type ReactiveProperty<T>
        private void SetupOnAnyPropertyChanged()
        {
            // Get All INT Reactive Properties in this class
            IEnumerable<FieldInfo> intReactiveInfos = this.GetType()
                .GetFields()
                .Where(e => e.FieldType == typeof(IntReactiveProperty));

            // Fire event when any property changes
            foreach (var ri in intReactiveInfos)
            {
                var react = ri.GetValue(this) as IntReactiveProperty;
                react.Subscribe(_ => OnAnyPropertyChanged.Execute());
            }

            // Get All FLOAT Reactive Properties in this class
            IEnumerable<FieldInfo> floatReactiveInfos = this.GetType()
                .GetFields()
                .Where(e => e.FieldType == typeof(FloatReactiveProperty));

            // Fire event when any property changes
            foreach (var ri in floatReactiveInfos)
            {
                var react = ri.GetValue(this) as FloatReactiveProperty;
                react.Subscribe(_ => OnAnyPropertyChanged.Execute());
            }

            // Get All BOOL Reactive Properties in this class
            IEnumerable<FieldInfo> boolReactiveInfos = this.GetType()
                .GetFields()
                .Where(e => e.FieldType == typeof(BoolReactiveProperty));

            // Fire event when any property changes
            foreach (var ri in boolReactiveInfos)
            {
                var react = ri.GetValue(this) as BoolReactiveProperty;
                react.Subscribe(_ => OnAnyPropertyChanged.Execute());
            }
        }        
    }
}
