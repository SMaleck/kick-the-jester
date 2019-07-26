using Assets.Source.Services.Localization;
using Assets.Source.Util;
using NUnit.Framework;
using System.Linq;

namespace Assets.Editor.Test.Localization
{
    public class LocalizationTest
    {
        [Test]
        public void ShouldContainAllKeys()
        {
            EnumHelper<TextKey>.Iterator
                .ToList()
                .ForEach(key =>
                {
                    Assert.DoesNotThrow(
                        () => { TextRepo.GetText(key); },
                        $"Failed for TextKey {key}");
                });
        }
    }
}
